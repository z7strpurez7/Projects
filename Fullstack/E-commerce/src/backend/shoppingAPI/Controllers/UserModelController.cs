using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using shoppingAPI.Context;
using shoppingAPI.Models;
using System.Net.Mime;

namespace shoppingAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [ApiConventionType(typeof(DefaultApiConventions))]
    [Produces(MediaTypeNames.Application.Json)]
    [Consumes(MediaTypeNames.Application.Json)]
    public class UserModelController : ControllerBase
    {
        private readonly myDbContext _context;

        public UserModelController(myDbContext context)
        {
            _context = context;
        }


        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            List<UserModel> users = null;
            try
            {
                users = await _context.User.ToListAsync();
            }catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok(users);
        }



        // In the works
        [HttpPost]
        public async Task<IActionResult> PostUser(UserModel user)
        {
         
            try
            {
                await _context.User.AddAsync(user);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex) { }
            return Ok(user);
        }

       

    }

}
