using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using movieCharacterAPI.Context;
using movieCharacterAPI.Dto;
using movieCharacterAPI.Models;
using System.Net;
using System.Net.Mime;
using static movieCharacterAPI.Dto.CharacterDtos;
using static movieCharacterAPI.Dto.MovieDtos;

namespace movieCharacterAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [ApiConventionType(typeof(DefaultApiConventions))]
    [Produces(MediaTypeNames.Application.Json)]
    [Consumes(MediaTypeNames.Application.Json)]
    public class CharacterEntityController : ControllerBase
    {
        private readonly MovieContext _context;
        private readonly IMapper _mapper;
        public CharacterEntityController(MovieContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        /// <summary>
        /// Get All character information, all movies and characters
        /// </summary>
        /// <returns>A list with all details</returns>
        [HttpGet]
        public async Task<IActionResult> GetAllCharacterDetails()
        {
            List<CharacterDto> characters = null;
            try
            {
                characters = await _context.Characters.Include(e => e.Movies).Select(e => new CharacterDto(
                    e.CharacterId,
                    e.Name,
                    e.Alias,
                    e.Gender,
                    e.Picture,
                    e.Movies.Select(e => new MovieDto(e.MovieId,
                    e.Title,
                    e.Genre,
                    e.ReleaseYear,
                    e.Director,
                    e.Picture,
                    e.Trailer,
                    e.Character.Select(e => new CharacterTitle(e.CharacterId, e.Name)).ToList(),
                    e.Franchise.Name)).ToList())).ToListAsync();

                if (characters == null) { return NotFound(); }
            } catch (Exception ex) {return BadRequest(ex.Message);}
            return Ok(characters);
        }

     

        /// <summary>
        /// Returns all names of the Characters
        /// </summary>
        /// <returns>A list of Character names</returns>
        [HttpGet("Titles")]
        public async Task<IActionResult> GetAllCharacterTitles()
        {
            List<CharacterTitle> characters = null;
            try
            {
                characters = await _context.Characters.Select(e => new CharacterTitle(
                e.CharacterId, e.Name)).ToListAsync();

                if (characters == null) { return NotFound(); }
            } catch (Exception ex){return BadRequest(ex.Message);}
            return Ok(characters);
        }




    
        /// <summary>
        /// Get Character details by Unique identifier 
        /// </summary>
        /// <param name="id">Unique identifier</param>
        /// <returns>Character details</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCharacterById(int id)
        {
            CharacterDto? result = null;
            try
            {
                result = await _context.Characters.Where(e => e.CharacterId == id).Include(e => e.Movies).Select(e => new CharacterDto(
                    e.CharacterId,
                    e.Name,
                    e.Alias,
                    e.Gender,
                    e.Picture,
                   e.Movies.Select(e => new MovieDto(e.MovieId,
                    e.Title,
                    e.Genre,
                    e.ReleaseYear,
                    e.Director,
                    e.Picture,
                    e.Trailer,
                    e.Character.Select(e => new CharacterTitle(e.CharacterId, e.Name)).ToList(),
                    e.Franchise.Name)).ToList())).FirstOrDefaultAsync();

                if (result == null) { return NotFound(); }
            } catch (Exception ex){return BadRequest(ex);}
            return Ok(result);
        }
      

        /// <summary>
        /// Post a Character
        /// </summary>
        /// <param name="characterDto">Character creation requestBody</param>
        /// <returns>ObjectResult  Character Id and status code 201(Created) </returns>
        [HttpPost]
        public async Task<IActionResult> AddCharacter(PostCharacterDto characterDto)
        {
            if (characterDto is null) { return NoContent(); }

            CharacterEntity character = null;

            try {
                character = new CharacterEntity
                {
                    Name = characterDto.FullName,
                    Alias = characterDto.Alias,
                    Gender = characterDto.Gender,
                    Picture = characterDto.Picture
                };
                await _context.Characters.AddAsync(character);
                await _context.SaveChangesAsync();

            }catch (Exception ex) {return BadRequest(ex);}
            return new ObjectResult(character.CharacterId) { StatusCode = (int)HttpStatusCode.Created};
        }
        



        /// <summary>
        /// Edit a Existing Character with the details in RequestBody
        /// </summary>
        /// <param name="command">Identifier and requestBody details</param>
        /// <returns>ObjectResult Character Id and status code 202(Accepted)</returns>
        [HttpPut]
        public async Task<IActionResult> UpdateCharacter([FromBody] UpdateCharacterCommand command)
        {
            if (command is null)
            {
                return BadRequest();
            }
            var character = await _context.Characters.FirstOrDefaultAsync(e => e.CharacterId == command.Id);
            try
            {
                character.Name = command.CharacterDto.FullName;
                character.Alias = command.CharacterDto.Alias;
                character.Gender = command.CharacterDto.Gender;
                character.Picture = command.CharacterDto.Picture;
                character.Movies = new List<MovieEntity>();

                await _context.SaveChangesAsync();
            }
            catch (Exception _)
            {
                return BadRequest("An error accoured during request processing");
            }
            return new ObjectResult(character.CharacterId) { StatusCode = (int)HttpStatusCode.Accepted };
        }



        /// <summary>
        /// Delete a Character with the given identifier
        /// </summary>
        /// <param name="id">Character Identifier</param>
        /// <returns>Status Ok 200</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCharacterById(int id)
        {
            try
            {
                var character = await _context.Characters.FindAsync(id);
                if(character == null) { return NotFound();}

                _context.Characters.Remove(character);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex) { return BadRequest(ex.Message); }
            return Ok();
        }
    }
}


