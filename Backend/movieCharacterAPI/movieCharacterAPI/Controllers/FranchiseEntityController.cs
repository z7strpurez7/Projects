using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using movieCharacterAPI.Context;
using movieCharacterAPI.Models;
using NuGet.Packaging;
using System.Net;
using System.Net.Mime;
using static movieCharacterAPI.Dto.CharacterDtos;
using static movieCharacterAPI.Dto.FranchiseDtos;
using static movieCharacterAPI.Dto.MovieDtos;


namespace movieCharacterAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [ApiConventionType(typeof(DefaultApiConventions))]
    [Produces(MediaTypeNames.Application.Json)]
    [Consumes(MediaTypeNames.Application.Json)]
    public class FranchiseEntityController : ControllerBase
    {
        private readonly MovieContext _context;
        private readonly IMapper _mapper;
        public FranchiseEntityController(MovieContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }


        /// <summary>
        /// Get all Characters related details in given a specific Franchise
        /// </summary>
        /// <param name="id">Franchise identifier</param>
        /// <returns>List of Characters</returns>
        [HttpGet("Characters{id}")]
        public async Task<IActionResult> GetCharactersByFranchiseId([FromRoute] int id)
        {
            List<MovieCharactersDto> characters = null;
            List<MovieEntity> franchise = null;
            try {
                characters = await _context.Movies.Where(e => e.FranchiseId == id)
                    .Select(e => new MovieCharactersDto(e.Character.Select(c => new CharacterTitle(c.CharacterId, c.Name)).ToList())).ToListAsync();

            } catch (Exception ex) {return BadRequest(ex.Message);}

            return Ok(characters);
        }


        /// <summary>
        /// Get all Franchises
        /// </summary>
        /// <returns>Returns a list of Franchises</returns>
        [HttpGet("Franchises")]
        public async Task<IActionResult> GetAllFranchises()
        {
            List<FranchiseDto> franchises = null;
            try {
                franchises = await _context.Franchises.Include(e => e.Movies).Select(e => new FranchiseDto(
                    e.FranchiseId,
                    e.Name,
                    e.Description,
                    e.Movies.Select(e => new MovieTitlesDto(e.MovieId, e.Title)).ToList()
                    )).ToListAsync();
            } catch (Exception ex) { return BadRequest(ex.Message);}

            if (franchises == null || franchises.Count is 0) { return NoContent(); }

            return Ok(franchises);
        }
    

        /// <summary>
        /// Get Franchise by Unique identifier
        /// </summary>
        /// <param name="id">Unique identifier</param>
        /// <returns>A FranchiseDto</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetFranchiseById([FromRoute] int id)
        {
            FranchiseDto? result = null;
            try
            {
                result = await _context.Franchises.Where(e => e.FranchiseId == id).Include(e => e.Movies).Select(e =>
                      new FranchiseDto(
                        e.FranchiseId,
                        e.Name,
                        e.Description,
                        e.Movies.Select(e => new MovieTitlesDto(e.MovieId, e.Title)).ToList())).SingleOrDefaultAsync();
                if (result == null) { return NotFound(); }

            } catch (Exception ex) { return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing the request.");}

            return Ok(result);
        }


        /// <summary>
        /// Create a new Franchise
        /// </summary>
        /// <param name="franchiseDto"></param>
        /// <returns>Object Result with the Id and status code Created</returns>
        [HttpPost]
        public async Task<IActionResult> PostFranchise(PostFranchiseDto franchiseDto)
        {
            if (franchiseDto == null) { return NoContent(); }

            FranchiseEntity franchise = null;
            try {
                franchise = new FranchiseEntity
                {
                    Name = franchiseDto.Name,
                    Description = franchiseDto.Description

                };

                await _context.Franchises.AddAsync(franchise);
                await _context.SaveChangesAsync();

            } catch (Exception ex) { Console.WriteLine(ex); }

            return new ObjectResult(franchise.FranchiseId) { StatusCode = (int)HttpStatusCode.Created };
        }


        /// <summary>
        /// Update existing details in a given Franchise Identifier
        /// </summary>
        /// <param name="command">Id and RequestBodyu</param>
        /// <returns>ObjectResult with Id and Status Accepted</returns>
        [HttpPut]
        public async Task<IActionResult> UpdateFranchise([FromBody] UpdateFranchiseCommand command)
        {
            if (command is null)
            {
                return BadRequest();
            }
            var franchise = await _context.Franchises.FirstOrDefaultAsync(e => e.FranchiseId == command.Id);
            try
            {
                franchise.FranchiseId = command.Id;
                franchise.Name = command.FranchiseDto.Name;
                franchise.Description = command.FranchiseDto.Description;

                await _context.SaveChangesAsync();
            }
            catch (Exception _)
            {
                return BadRequest("An error accoured during request processing");
            }

            return new ObjectResult(franchise.FranchiseId) { StatusCode = (int)HttpStatusCode.Accepted };
        }
    


        /// <summary>
        /// Add/Update movies in a given Franchise (Replaces existing Movies)
        /// </summary>
        /// <param name="identifiers">Franchise id and List of Movie Ids</param>
        /// <returns>Ok with FranchiseId</returns>
        [HttpPut("Movies")]
        public async Task<IActionResult> UpdateFranchiseMovies([FromBody] UpdateFranchiseMovies identifiers)
        {
                try
                {
                    if (identifiers.MovieIds == null || identifiers.MovieIds.Length == 0){ return BadRequest("Movie array is empty."); }

                    var Franchise = await _context.Franchises.Include(m => m.Movies).FirstOrDefaultAsync(e => e.FranchiseId == identifiers.FranchiseId);
                    if (Franchise is null) { return NotFound("Franchise not found.");}

                    var Movies = await _context.Movies.Where(c => identifiers.MovieIds.Contains(c.MovieId)).ToListAsync();
                    if (Movies.Count != identifiers.MovieIds.Length){return NotFound("One or more movies not found."); }

                    Franchise.Movies.Clear();
                    Franchise.Movies.AddRange(Movies);

                    await _context.SaveChangesAsync();
                }
                catch (Exception _){return BadRequest("An error occurred during request processing."); }

                return Ok(identifiers.FranchiseId);
            }
        

        /// <summary>
        /// Delete Franchise by Identifier
        /// </summary>
        /// <param name="id">Unique Identifier</param>
        /// <returns>Ok 200</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFranchiseById([FromRoute] int id)
        {
            try
            {
                var franchise = await _context.Franchises.FindAsync(id);
                if(franchise == null){return NotFound();}
                var movies = await _context.Movies.Where(e => e.FranchiseId == id).ToListAsync();
                
                //Set movie foreign keys to null to allow delete
                foreach (var movie in movies)
                {
                    movie.FranchiseId = null;
                }

                _context.Remove(franchise);
                await _context.SaveChangesAsync();

            }catch (Exception ex){ return BadRequest(ex.Message); }
            return Ok();
        }
       
    }

}

    
