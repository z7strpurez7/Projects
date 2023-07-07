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
using NuGet.Packaging;
using System.Net.Mime;
using AutoMapper;

namespace movieCharacterAPI.Controllers
{
    //MovieEntityController inherits from asp.net controllerbase
    [Route("[controller]")]
    [ApiController]
    [ApiConventionType(typeof(DefaultApiConventions))]
    [Produces(MediaTypeNames.Application.Json)]
    [Consumes(MediaTypeNames.Application.Json)]
    public class MovieEntityController : ControllerBase
    {
        private readonly MovieContext _context;
        private readonly IMapper _mapper;
        public MovieEntityController(MovieContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }


        /// <summary>
        /// Gets all movie names
        /// </summary>
        /// <returns>List of MovieTitleDto</returns>
        [HttpGet]
        public async Task<IActionResult> GetAllMovieTitles()
        {
            List<MovieTitlesDto>? titles = null;
            try
            {
                titles = await _context.Movies.Select(e => new MovieTitlesDto(
                   e.MovieId,
                   e.Title)).ToListAsync();
            } catch (Exception ex){ return BadRequest(ex.Message); }
            return Ok(titles);
        }


        /// <summary>
        /// Gets all movies details, including Character and Franchise names
        /// </summary>
        /// <returns>List of Movies with Character/Franchise names</returns>
        [HttpGet("Details")]
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
                if (result is null || result.Count() is 0) {return NoContent(); }

            }catch (Exception _)  {return BadRequest("An error accoured during request processing"); }

            return Ok(result);
        }
   


        /// <summary>
        /// Gets a specific movie based on id
        /// </summary>
        /// <param name="id">Movie Identifier</param>
        /// <returns></returns>
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
                if (result is null) { return NoContent(); }
            }
            catch (Exception _){ return BadRequest("An error accoured during request processing");}

            return Ok(result);
        }


        /// <summary>
        /// Takes in moviedetails into specified movieDto and Posts
        /// a new movie with the details
        /// </summary>
        /// <param name="movieDto"></param>
        /// <returns>The created Movie Id and status code 201(Created)</returns>
        [HttpPost]
        public async Task<IActionResult> PostMovie(PostMovieDto movieDto)
        {
            MovieEntity? movie = null;
            try
            {
                if (movieDto is null){return BadRequest();}
                movie = new MovieEntity
                {
                    Title = movieDto.MovieTitle,
                    Genre = movieDto.Genre,
                    ReleaseYear = movieDto.ReleaseYear,
                    Director = movieDto.Director,
                    Picture = movieDto.Picture,
                    Trailer = movieDto.Trailer,
                    FranchiseId = movieDto.franchiseId,    
                };
                await _context.Movies.AddAsync(movie);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)  {return BadRequest("An error occurred while processing the request."); }
            return new ObjectResult(movie.MovieId) { StatusCode = (int)HttpStatusCode.Created };
        }
    

        /// <summary>
        /// Updates movie details to the given specified Id
        /// </summary>
        /// <param name="command">New movie details and the movieIdentifier</param>
        /// <returns>The modified movieId and status 202(Accepted)</returns>
        [HttpPut]
        public async Task<IActionResult> UpdateMovie([FromBody] UpdateMovieCommand command)
        {
            if (command is null) { return BadRequest(); }

            var movie = await _context.Movies.FirstOrDefaultAsync(e => e.MovieId == command.Id);
            try
            {
                movie.Title = command.Dto.MovieTitle;
                movie.Genre = command.Dto.Genre;
                movie.ReleaseYear = command.Dto.ReleaseYear;
                movie.Director = command.Dto.Director;
                movie.Picture = command.Dto.Picture;
                movie.Trailer = command.Dto.Trailer;

                if (command.Dto.franchiseId == 0)
                {
                    movie.FranchiseId = null;
                } else
                {
                    movie.FranchiseId = command.Dto.franchiseId;
                }
                await _context.SaveChangesAsync();
            }
            catch (Exception _){ return BadRequest("An error accoured during request processing"); }

            return new ObjectResult(movie.MovieId) { StatusCode = (int)HttpStatusCode.Accepted };
        }
     

        /// <summary>
        /// Adds the given Characters to a movie (Replaces existing Characters with the new ones)
        /// </summary>
        /// <param name="identifiers">Movie identifier and Character Identifiers</param>
        /// <returns>Status 200 Ok with Movie identifier</returns>
        [HttpPut("SetCharacters")]
        public async Task<IActionResult> UpdateMovieCharacters([FromBody] UpdateMovieCharacterRelation identifiers)
        {
            try
            {
                if (identifiers.CharacterId == null || identifiers.CharacterId.Length == 0) {  return BadRequest("CharacterId array is empty.");}

                var movie = await _context.Movies.Include(m => m.Character).FirstOrDefaultAsync(e => e.MovieId == identifiers.MovieId);
                if (movie is null) {return NotFound("Movie not found."); }

                var characters = await _context.Characters.Where(c => identifiers.CharacterId.Contains(c.CharacterId)).ToListAsync();
                if (characters.Count != identifiers.CharacterId.Length){ return NotFound("One or more characters not found.");}

                movie.Character.Clear();
                movie.Character.AddRange(characters);

                await _context.SaveChangesAsync();
            }
            catch (Exception _) {  return BadRequest("An error occurred during request processing."); }

            return Ok(identifiers.MovieId);
        }


        /// <summary>
        /// Deletes a movie by given id specifier
        /// </summary>
        /// <param name="id"></param>
        /// <returns>200 Ok</returns>
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
            catch (Exception) { return BadRequest("An error occurred during request processing"); }
            return Ok();
        }
        //Identifies and deletes the Movie with the given Id
        //returns not found if not found
        //Returns OK()
        //
    }
}

  
   