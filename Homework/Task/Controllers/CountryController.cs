using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Task.Data;
using Task.Models;

namespace Task.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CountryController : ControllerBase
    {
        private readonly AppDbContext _context;

        public CountryController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _context.Countries.Include(m=>m.Cities)
                                              .ToListAsync());
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Country country)
        {
            if(!ModelState.IsValid) return BadRequest(ModelState);

            await _context.Countries.AddAsync(country);

            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(Create), country);

        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var existData = await _context.Countries.Include(m => m.Cities)
                                                    .FirstOrDefaultAsync(m => m.Id == id);

            if (existData is null) return NotFound();

            return Ok(existData);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete([FromQuery] int? id)
        {
            if(id is null) return BadRequest();

            var existData = await _context.Countries.Include(m=>m.Cities)
                                                    .FirstOrDefaultAsync(m=>m.Id == id);

            if (existData is null) return NotFound();

            _context.Countries.Remove(existData);

            await _context.SaveChangesAsync();

            return Ok();

        }

        [HttpPut]
        public async Task<IActionResult> Edit([FromQuery] int? id, [FromBody] Country newCountry)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            if(id is null) return BadRequest();

            var existCountry = await _context.Countries.Include(m => m.Cities)
                                                       .FirstOrDefaultAsync(m => m.Id == id);

            if (existCountry is null) return NotFound();

            existCountry.CountryName = newCountry.CountryName;
            await _context.SaveChangesAsync();
            return AcceptedAtAction(nameof(Edit), newCountry);
        }

    }
}
