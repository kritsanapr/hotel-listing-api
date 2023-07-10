using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace HotelListingAPI.VSCode.Data
{
    public class HotelListingDbContext : DbContext
    {
        public HotelListingDbContext(DbContextOptions options) : base(options) { }

        public DbSet<Hotel> Hotels { get; set; }
        public DbSet<Country> Countries { get; set; }

        // Seed database 
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Country>().HasData(
                new Country { Id = 1, Name = "United States of America", ShortName = "USA" },
                new Country { Id = 2, Name = "Canada", ShortName = "Canada" },
                new Country { Id = 3, Name = "France", ShortName = "France" },
                new Country { Id = 4, Name = "Germany", ShortName = "Germany" },
                new Country { Id = 5, Name = "Italy", ShortName = "Italy" },
                new Country { Id = 6, Name = "Japan", ShortName = "Japan" },
                new Country { Id = 7, Name = "United Kingdom", ShortName = "UK" }
            );

            modelBuilder.Entity<Hotel>().HasData(
                new Hotel { Id = 1, Name = "Lakewood", Address = "Lakewood Address", Rating = 4.3, CountryId = 1 },
                new Hotel { Id = 2, Name = "Bridgewood", Address = "Bridgewood Address", Rating = 4.4, CountryId = 1 },
                new Hotel { Id = 3, Name = "Ridgewood", Address = "Ridgewood Address", Rating = 4.5, CountryId = 1 },
                new Hotel { Id = 4, Name = "Hilton", Address = "Hilton Address", Rating = 4.6, CountryId = 2 }
            );
        }


    }
}