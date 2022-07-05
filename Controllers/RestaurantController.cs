using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using RestaurantRaterAPI.Models;

namespace RestaurantRaterAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RestaurantController : Controller
    {
        private RestaurantDbContext _context;

        public RestaurantController(RestaurantDbContext context)
        {
            _context = context;
        }
        [HttpPost]
        public async Task<IActionResult> PostRestaurant([FromForm] ResturantEdit model){
            if (!ModelState.IsValid){
                return BadRequest(ModelState);
            }
            _context.Restaurants.Add(new Restaurant(){
                Name = model.Name,
                Location = model.Location,
            });
            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> GetAllRestuarants(){
            var restaurants = await _context.Restaurants.Include(r => r.Ratings).ToListAsync();
            List<RestaurantListItem> restaurantList = restaurants.Select(r => new RestaurantListItem(){
                Id = r.Id,
                Name = r.Name,
                Location = r.Location,
                AverageScore = r.AverageScore
            }).ToList();
            return Ok(restaurantList);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetRestaurantById(int id){
            var restuarant = await _context.Restaurants.Include(r => r.Ratings).FirstOrDefaultAsync(r => r.Id == id);
            if (restuarant == null){
                return NotFound();
            }
            return Ok(restuarant);
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteRestaurant([FromRoute] int id){
            var restuarant = await _context.Restaurants.FindAsync(id);
            if (restuarant == null){
                return NotFound();
            }
            _context.Restaurants.Remove(restuarant);
            await _context.SaveChangesAsync();
            return Ok();
        }
    }
}