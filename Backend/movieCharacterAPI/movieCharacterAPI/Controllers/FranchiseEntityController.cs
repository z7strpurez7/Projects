using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using movieCharacterAPI.Context;
using movieCharacterAPI.Models;
using System.Net;
using static movieCharacterAPI.Dto.FranchiseDtos;
using static movieCharacterAPI.Dto.MovieDtos;


namespace movieCharacterAPI.Controllers
{
    [Route("[controller]")]
    public class FranchiseEntityController : ControllerBase
    {
        private readonly MovieContext _context;
        public FranchiseEntityController(MovieContext context)
        {
            _context = context;
        }
        //Get all Franchises
        [HttpGet("Franchise/AllFranchises")]
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
            } catch (Exception ex) {
                return BadRequest(ex.Message);
            }
            if (franchises == null || franchises.Count is 0){return NoContent();}
            return Ok(franchises);
        }
        ///Returns "ok" result with the list of franchises as responsebody
        ///If an error occurs, it returns a "BadRequest"
        ///also if the table is empty(null), it returns no content
        [HttpGet("Franchise/{id}")]
        public async Task<IActionResult> GetFranchiseById([FromRoute] int id)
        {
            FranchiseDto? result = null;
            try
            {
                result = await _context.Franchises.Where(e => e.FranchiseId == id).Include(e => e.Movies).Select( e=>
                      new FranchiseDto(
                        e.FranchiseId,
                        e.Name,
                        e.Description,
                        e.Movies.Select(e => new MovieTitlesDto(e.MovieId, e.Title)).ToList())).SingleOrDefaultAsync();
                if (result == null){return NotFound();}
            } catch (Exception ex) {
                Console.WriteLine($"Exception occurred while retrieving movie: {ex}");
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing the request.");
            }
            return Ok(result);
        }

        //POST
        [HttpPost]
        public async Task<IActionResult> PostFranchise(PostFranchiseDto franchiseDto)
        {
            if(franchiseDto == null) { return NoContent();}
            FranchiseEntity franchise = null;
            try {

                franchise = new FranchiseEntity
                {
                    Name = franchiseDto.Name,
                    Description = franchiseDto.Description

                };

                await _context.Franchises.AddAsync(franchise);
                await _context.SaveChangesAsync();
            
            }catch (Exception ex) { Console.WriteLine(ex); }

            return new ObjectResult(franchise.FranchiseId) { StatusCode = (int)HttpStatusCode.Created };

        }

        //Update
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

            return new ObjectResult(franchise.FranchiseId) { StatusCode= (int)HttpStatusCode.Accepted };
        }

        [HttpDelete("Franchise/{id}")]
        public async Task<IActionResult> DeleteFranchiseById([FromRoute] int id)
        {

            try
            {
                var franchise = await _context.Franchises.FindAsync(id);
                if(franchise == null){return NotFound();}
                var movies = await _context.Movies.Where(e => e.FranchiseId == id).ToListAsync();
                
                //Set movie foreign key to null to allow delete
                foreach (var movie in movies)
                {
                    movie.FranchiseId = null;
                }

                _context.Remove(franchise);
                await _context.SaveChangesAsync();


            }catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok();
        }

    }
}
    
