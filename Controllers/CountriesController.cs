using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using HotelListingAPI.VSCode.Contracts;
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
        private readonly IMapper _mapper;
        private readonly ICountriesRepository _countriesRepository;

        public CountriesController(ILogger<CountriesController> logger, IMapper mapper, ICountriesRepository countriesRepository)
        {
            _countriesRepository = countriesRepository;
            _logger = logger;
            // _context = context;
            _mapper = mapper;
        }

        // GET : api/Countries
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GetCountryDto>>> GetCountries()
        {
            var countryList = await _countriesRepository.GetAllAsync();
            var records = _mapper.Map<List<GetCountryDto>>(countryList);
            return Ok(records);
        }



        [HttpGet("{id}")]
        public async Task<ActionResult<CountryDto>> GetCountry(int id)
        {
            var country = await _countriesRepository.GetDetails(id);
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
            var country = _mapper.Map<Country>(createCountry);
            await _countriesRepository.AddAsync(country);
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

            var country = await _countriesRepository.GetAsync(id);
            if (country == null)
            {
                return NotFound();
            }

            _mapper.Map(updateCountryDto, country);

            try
            {
                await _countriesRepository.UpdateAsync(country);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await CountryExists(id))
                {
                    return NotFound(id);
                }
                else
                {
                    throw;
                }

            }
            return NoContent();
        }

        // DELETE : api/Countries/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCountry(int id)
        {
            var country = await _countriesRepository.GetAsync(id);
            if (country == null)
            {
                return NotFound();
            }
            _countriesRepository.DeleteAsync(id);
            await _countriesRepository.UpdateAsync(country);
            return NoContent();
        }

        private async Task<bool> CountryExists(int id)
        {
            return await _countriesRepository.Exists(id);
        }

    }
}