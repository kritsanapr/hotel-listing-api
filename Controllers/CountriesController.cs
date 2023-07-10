using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using HotelListingAPI.VSCode.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace HotelListingAPI.VSCode.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CountriesController : Controller
    {
        private readonly ILogger<CountriesController> _logger;
        private readonly HotelListingDbContext _context;

        public CountriesController(ILogger<CountriesController> logger, HotelListingDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        // GET : api/Countries
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Country>>> GetCountries()
        {
            var countryList = await _context.Countries.ToListAsync();
            return Ok(countryList);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Country>> GetCountry(int id)
        {
            var country = await _context.Countries.FindAsync(id);
            if (country == null)
            {
                return NotFound();
            }
            return Ok(country);

        }

        // POST : api/Countries
        [HttpPost]
        public async Task<ActionResult<Country>> CreateCountry(Country country)
        {
            _context.Countries.Add(country);
            await _context.SaveChangesAsync();
            return CreatedAtAction("GetCountry", new { id = country.Id }, country);

        }


        // PUT : api/Countries/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCountry(int id, Country country)
        {
            if (id != country.Id)
            {
                return BadRequest("Invalid Record Id");
            }
            _context.Entry(country).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CountryExists(id))
                {
                    return NotFound(id);
                }
                else
                {
                    throw;
                }

            }
            return NoContent();

            // _context.Countries.Update(country);
            // await _context.SaveChangesAsync();
            // return NoContent();
        }

        // DELETE : api/Countries/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCountry(int id)
        {
            var country = await _context.Countries.FindAsync(id);
            if (country == null)
            {
                return NotFound();
            }
            _context.Countries.Remove(country);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        private bool CountryExists(int id)
        {
            return _context.Countries.Any(e => e.Id == id);
        }

    }
}