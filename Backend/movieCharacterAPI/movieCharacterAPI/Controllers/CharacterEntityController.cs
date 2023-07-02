using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using movieCharacterAPI.Context;
using movieCharacterAPI.Dto;
using movieCharacterAPI.Models;
using System.Net;
using static movieCharacterAPI.Dto.CharacterDtos;
using static movieCharacterAPI.Dto.MovieDtos;

namespace movieCharacterAPI.Controllers
{
    [Route("[controller]")]
    public class CharacterEntityController : ControllerBase
    {
        private readonly MovieContext _context;
        public CharacterEntityController(MovieContext context)
        {
            _context = context;
        }

        // Prints out All characters, all the movies the act in + every character in the movie 
        [HttpGet("GetAllCharacterRelatedDetails")]
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

            } catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok(characters);
        }

        //Prints out all Character names with Id
        [HttpGet("Character/CharacterTitles")]
        public async Task<IActionResult> GetAllCharacterTitles()
        {
            List<CharacterTitle> characters = null;
            try
            {
                characters = await _context.Characters.Select(e => new CharacterTitle(
                e.CharacterId, e.Name)).ToListAsync();
                if (characters == null) { return NotFound(); }
            } catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok(characters);
        }

        //Prints out character by Id, prints out 
        [HttpGet("Character/{id}")]
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
            } catch (Exception ex)
            {
                return BadRequest(ex);
            }
            return Ok(result);
        }


        //Add a regular CharacterEntity
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
            
            }catch (Exception ex) { }
            return new ObjectResult(character.CharacterId) { StatusCode = (int)HttpStatusCode.Created};
        }



        //Edit a Character and its movies
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
            return new ObjectResult(character.CharacterId) { StatusCode = (int)HttpStatusCode.Created };
        }
     
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
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok();
        }
    }
}


