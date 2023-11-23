using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using shoppingAPI.Context;
using shoppingAPI.Models;
using System.Runtime.CompilerServices;

namespace shoppingAPI.Controllers
{
    [Route("[Controller]")]
    public class ProductModelController : ControllerBase
    {
        private readonly myDbContext _context;

        public ProductModelController(myDbContext context) { 
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetProducts()
        {
            List<ProductModel> products = null;
            try
            {
                products = await _context.Product.ToListAsync();

            }catch (Exception ex)
            {

            } 

            return Ok(products);
        }

        [HttpPost]
        public async Task<IActionResult> PostProduct(ProductModel product)
        {
            try
            {
                await _context.Product.AddAsync(product);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex) { }
          
            return Ok(product);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            try { 
                var product = await _context.Product.FindAsync(id);

                if(product == null)
                {
                    return NotFound();
                }

                _context.Remove(product);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex) { 
                return BadRequest(ex.Message);
            }
            return Ok();
        }
    }
}
