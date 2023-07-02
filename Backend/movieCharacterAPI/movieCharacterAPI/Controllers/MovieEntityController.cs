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

using static movieCharacterAPI.Dto.MovieDtos;

using static movieCharacterAPI.Dto.CharacterDtos.CharacterTitle;
using static movieCharacterAPI.Dto.CharacterDtos;




namespace movieCharacterAPI.Controllers
{
    //MovieEntityController inherits from asp.net controllerbase
    [Route("[controller]")]
    public class MovieEntityController : ControllerBase
    {
        private readonly MovieContext _context;

        public MovieEntityController(MovieContext context)
        {
            _context = context;
        }


        //GetMovieTitles and IDs
        [HttpGet("MovieNames")]
        public async Task<IActionResult> GetAllMovieTitles()
        {

            List<MovieTitlesDto>? titles = null;
            try
            {
                titles = await _context.Movies.Select(e => new MovieTitlesDto(
                   e.MovieId,
                   e.Title)).ToListAsync();
            } catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok(titles);
        }
        //GetRequest for Title and Id (Just to make it easier to check out the movies)

        [HttpGet("GetAllMovieDetails")]
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
                    e.Genre,
                    e.ReleaseYear,
                    e.Director,
                    e.Picture,
                    e.Trailer,
                    e.Character.Select(c => new CharacterTitle(c.CharacterId, c.Name)).ToList(),
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
                e.Title, e.Genre,
                e.ReleaseYear,
                e.Director,
                e.Picture,
                e.Trailer,
                e.Character.Select(c => new CharacterTitle(c.CharacterId, c.Name)).ToList(),
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
        [HttpPost("Post")]
        public async Task<IActionResult> PostMovie( PostMovieDto movieDto)
        {
            MovieEntity? movie = null;
            try
            {
                if(movieDto is null)
                {
                    return BadRequest();
                }
              

                movie = new MovieEntity
                {
                    Title = movieDto.MovieTitle,
                    Genre = movieDto.Genre,
                    ReleaseYear = movieDto.ReleaseYear,
                    Director = movieDto.Director,
                    Picture = movieDto.Picture,
                    Trailer = movieDto.Trailer,
                    FranchiseId = movieDto.franchiseId,
               /*     Character = movieDto.Character.Select(e => new CharacterEntity
                    {
                        Name = e.Name,
                        Alias = e.Alias,
                        Gender = e.Gender,
                        Picture = e.Picture,
                        Movies = new List<MovieEntity>()
                    }).ToList(); */
                // Franchise = new FranchiseEntity {Name = movieDto.FranchiseName, Description = movieDto.FranchiseDescription, Movies = new List<MovieEntity>()}
            };
                    
                await _context.Movies.AddAsync(movie);
                await _context.SaveChangesAsync();

            }
            catch (Exception ex)
            {
                return BadRequest("An error occurred while processing the request.");
            }

            return new ObjectResult(movie.MovieId) { StatusCode = (int)HttpStatusCode.Created};
        } 


        //Update a movie with the option to set movie to a franchise
        [HttpPut("Update")]
        public async Task<IActionResult> UpdateMovie([FromBody] UpdateMovieCommand command)
        {
            if (command is null)
            {
                return BadRequest();
            }
            var movie = await _context.Movies.FirstOrDefaultAsync(e => e.MovieId == command.Id);
            
            try
            {
                movie.Title = command.Dto.MovieTitle;
                movie.Genre = command.Dto.Genre;
                movie.ReleaseYear = command.Dto.ReleaseYear;
                movie.Director = command.Dto.Director;
                movie.Picture = command.Dto.Picture;
                movie.Trailer = command.Dto.Trailer;
                if(command.Dto.franchiseId == 0)
                {
                    movie.FranchiseId = null;
                } else
                {
                    movie.FranchiseId = command.Dto.franchiseId;
                }
              

                await _context.SaveChangesAsync();
            }
            catch (Exception _)
            {
                return BadRequest("An error accoured during request processing");
            }

            return new ObjectResult(movie.MovieId) { StatusCode = (int)HttpStatusCode.Accepted };
        }
        /// UpdateMovie updates the values, you have to type in every value 
        /// or it will be default "string". When testing note that "releaseYear"
        /// only allows 5 characters. 
        /// UpdateValue only allows you to change values, also you are allowed to update the franchiseId
        /// If updatebody = null -> returns BadRequest. <summary>
        /// UpdateMovie updates the values, you have to type in every value 
        /// Returns BadRequest if Updatebody is invalid

        //Delete movie by id
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMovieById([FromRoute] int id)
        {
            try
            {
                var movie = await _context.Movies.FindAsync(id);
                if (movie == null) { return NotFound();
 }

                _context.Movies.Remove(movie);
                await _context.SaveChangesAsync();

             
            }
            catch (Exception)
            {
                return BadRequest("An error occurred during request processing");
            }
            return Ok();
        }
        //deletes by Id, returns not found if id movie is null.

    }
}

  
   