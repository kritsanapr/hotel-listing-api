using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using HotelListingAPI.VSCode.Data;
using HotelListingAPI.VSCode.Models.Country;
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
        private readonly IMapper _mapper;

        public CountriesController(ILogger<CountriesController> logger, HotelListingDbContext context, IMapper mapper)
        {
            _logger = logger;
            _context = context;
            _mapper = mapper;
        }

        // GET : api/Countries
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GetCountryDto>>> GetCountries()
        {
            var countryList = await _context.Countries.ToListAsync();
            var records = _mapper.Map<List<GetCountryDto>>(countryList);
            return Ok(countryList);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CountryDto>> GetCountry(int id)
        {
            var country = await _context.Countries.Include(c => c.Hotels)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (country == null)
            {
                return NotFound();
            }

            var countryDto = _mapper.Map<CountryDto>(country);
            return Ok(countryDto);

        }

        // POST : api/Countries
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Country>> CreateCountry(CreateCountryDto createCountry)
        {
            // var countryOld = new Country {
            //     Name = createCountry.Name,
            //     ShortName = createCountry.ShortName
            // };

            var country = _mapper.Map<Country>(createCountry);

            _context.Countries.Add(country);
            await _context.SaveChangesAsync();
            return CreatedAtAction("GetCountry", new { id = country.Id }, country);
        }


        // PUT : api/Countries/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCountry(int id, UpdateCountryDto updateCountryDto)
        {
            if (id != updateCountryDto.Id)
            {
                return BadRequest("Invalid Record Id");
            }
            // _context.Entry(country).State = EntityState.Modified;

            var country = await _context.Countries.FindAsync(id);

            if(country == null) {
                return NotFound();
            }

            _mapper.Map(updateCountryDto, country);
            
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