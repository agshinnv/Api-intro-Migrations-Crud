using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Task.Data;
using Task.Models;

namespace Task.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CityController : ControllerBase
    {
        private readonly AppDbContext _context;

        public CityController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _context.Cities.Include(m=>m.Country)
                                           .ToListAsync());
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] City city)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            await _context.Cities.AddAsync(city);

            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(Create), city);

        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var existData = await _context.Cities.Include(m => m.Country)
                                                 .FirstOrDefaultAsync(m => m.Id == id);

            if (existData is null) return NotFound();

            return Ok(existData);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete([FromQuery] int? id)
        {
            if(id is null) return BadRequest();

            var existData = await _context.Cities.Include(m=>m.Country)
                                                 .FirstOrDefaultAsync(m=>m.Id == id);

            if(existData is null) return NotFound();

            _context.Cities.Remove(existData);
            await _context.SaveChangesAsync();

            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> Edit([FromQuery] int? id, [FromBody] City newCity)
        {
            if(!ModelState.IsValid) return BadRequest(ModelState);

            if (id is null) return BadRequest();

            var existCity = await _context.Cities.Include(m => m.Country)
                                                 .FirstOrDefaultAsync(m => m.Id == id);

            if (existCity is null) return NotFound();

            existCity.CityName = newCity.CityName;

            await _context.SaveChangesAsync();

            return AcceptedAtAction(nameof(Edit), newCity);
        }
    }
}
