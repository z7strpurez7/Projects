using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using movieCharacterAPI.Context;
using movieCharacterAPI.Models;

namespace movieCharacterAPI.Controllers
{
    //MovieEntityController inherits from asp.net controllerbase
    public class MovieEntityController : ControllerBase
    {
        private readonly MovieContext _context;

        public MovieEntityController(MovieContext context)
        {
            _context = context;
        }

        [HttpGet("All Movies")]
        public async Task<IActionResult> GetAllMovies()
        {
            //Getting movies with DTO, not displaying all types of info
            List<MovieDto>? result = null;
            try
            {
                //Get movies from db and contents
                result = await _context.Movies
                .Include(e => e.Character)
                .Include(e => e.Franchise)
                .Select(e =>
                new MovieDto(
                    e.MovieId,
                    e.Title,
                    e.Character.Select(c => new CharacterDto(c.CharacterId,c.Name, c.Alias, c.Gender)).ToList(),
                    e.Franchise.Name)).ToListAsync();
            }
            catch (Exception _)
            {
                return BadRequest("An error accoured during request processing");
            }
            if (result is null || result.Count() is 0)
            {
                return NoContent();
            }

            return Ok(result);
        } 
        /// <summary>
        /// Returns a list of movies without all info (DTO) and ok(200), returns 400 if bad request
        /// And 204 if no content
        /// </summary>
     
        
        //Get movie by ID
        [HttpGet("{id}")]
        public async Task<IActionResult> GetMovieById([FromRoute] int id)
        {
            MovieDto? result = null;
            try
            {

                result = await _context.Movies
                .Include(e => e.Character)
                .Include(e => e.Franchise).Where(e => e.MovieId == id)
                .Select(e =>
                new MovieDto(
                    e.MovieId,
                    e.Title,
                    e.Character.Select(c => new CharacterDto(c.CharacterId,c.Name, c.Alias, c.Gender)).ToList(),
                    e.Franchise.Name))
                .FirstOrDefaultAsync();
                //Looks for first element or default
            }

            catch (Exception _)
            {
                return BadRequest("An error accoured during request processing");
            }

            if (result is null)
            {
                return NoContent();
            }

            return Ok(result);
        }
        /// <summary>
        /// returns the other get request
        /// </summary>
 

        //Add a movie
        [HttpPost("AddMovie")]
        public async Task<IActionResult> CreateNewMovie(MovieEntity movieDto)
        {
            //Return if 400 if null
            if (movieDto is null)
            {
                return BadRequest();
            }

            //Movie nullable
            MovieEntity? entity = null;
            try
            {
                entity = new MovieEntity
                {
                    Title = movieDto.Title,
                    Genre = movieDto.Genre,
                    ReleaseYear = movieDto.ReleaseYear,
                    Director = movieDto.Director,
                    Picture = movieDto.Picture,
                    Trailer = movieDto.Trailer,
                    Character = movieDto.Character.Select(e => new CharacterEntity
                    {
                        Name = e.Name,
                        Alias = e.Alias,
                        Gender = e.Gender,
                        Picture = e.Picture,
                        Movies = new List<MovieEntity>(),
                        
                    }).ToList(),
                    Franchise = new FranchiseEntity { Name = movieDto.Franchise.Name, Description =movieDto.Franchise.Description, Movies = new List<MovieEntity>() },
                 
                };

                await _context.Movies.AddAsync(entity);

                await _context.SaveChangesAsync();
            }

            catch (Exception _)
            {
                return BadRequest("An error accoured during request processing");
            }

            return new ObjectResult(entity.MovieId) { StatusCode = (int)HttpStatusCode.Created };
        }
        //Returns movieId value if sucessfull

        //Update a movie
        [HttpPost("Update")]
        public async Task<IActionResult> UpdateMovie([FromBody] UpdateMovieCommand command)
        {
            if (command is null)
            {
                return BadRequest();
            }

            MovieEntity? updatedEntity = null;

            try
            {
                updatedEntity = new MovieEntity
                {
                    MovieId = command.Dto.MovieId,
                    Title = command.Dto.Title,
                    Genre = command.Dto.Genre,
                    ReleaseYear = command.Dto.ReleaseYear,
                    Director = command.Dto.Director,
                    Picture = command.Dto.Picture,
                    Trailer = command.Dto.Trailer,
                    
                    FranchiseId = command.Dto.FranchiseId,

                    Character = command.Dto.Character.Select(e => new CharacterEntity
                    {
                        Name = e.Name,
                        Alias = e.Alias,
                        Gender = e.Gender,
                        Picture = e.Picture,
                        Movies = new List<MovieEntity>(),
                    }).ToList(),
                    Franchise = new FranchiseEntity { Name = command.Dto.Franchise.Name, Description = command.Dto.Franchise.Description, Movies = new List<MovieEntity>() },
                };

                _context.Movies.Attach(updatedEntity);
                _context.Entry(updatedEntity).State = EntityState.Modified;

                await _context.SaveChangesAsync();
            }


            catch (Exception _)
            {
                return BadRequest("An error accoured during request processing");
            }

            return new ObjectResult(updatedEntity.MovieId) { StatusCode = (int)HttpStatusCode.Created };
        }

        //Delete movie by id
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMovieById([FromRoute] int id)
        {
            try
            {
                var movie = await _context.Movies.FindAsync(id);
                if (movie == null)
                {
                    return NotFound();
                }

                _context.Movies.Remove(movie);
                await _context.SaveChangesAsync();

                return NoContent();
            }
            catch (Exception)
            {
                return BadRequest("An error occurred during request processing");
            }
        }
        //deletes by Id, returns not found if id movie is null.

    }
}

    public record GetMovieByIdQuery([FromRoute] int Id);

    public record UpdateMovieCommand(int Id, MovieEntity Dto);


    public record MovieDto(int MovieId,string MovieName, List<CharacterDto> Characters, string FranchiseName);

    public record CharacterDto(int CharacterId, string FullName, string? Alias, string Gender);