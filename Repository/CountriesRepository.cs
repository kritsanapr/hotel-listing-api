using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HotelListingAPI.VSCode.Contracts;
using HotelListingAPI.VSCode.Data;
using Microsoft.EntityFrameworkCore;

namespace HotelListingAPI.VSCode.Repository
{
    public class CountriesRepository : GenericRepository<Country>, ICountriesRepository
    {
        private readonly HotelListingDbContext _context;
        public CountriesRepository(HotelListingDbContext context) : base(context)
        {
            _context = context;

        }

        public async Task<Country> GetDetails(int? id)
        {
            return await _context.Countries.Include("Hotels").FirstOrDefaultAsync(c => c.Id == id);
        }
    }
}