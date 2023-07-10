# .Net Core Hotel Listing API


### .Net 6.0 CLI Package

Entity Framework
```
dotnet add package Microsoft.EntityFrameworkCore
```

Serilog.AspNetCore
```terminal
dotnet add package Serilog.AspNetCore
```


Serilog.Sinks.Seq 
```
dotnet add package Serilog.Sinks.Seq
```


Serilog.Expressions 
```
dotnet add package Serilog.Expressions
```


### Setup CORS (Cross Origin Resource Sharing)
setup in Program.cs file
```cs
// Add CORS 
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
    b => b.AllowAnyHeader()
    .AllowAnyOrigin()
    .AllowAnyMethod());
});
```

```C#
app.UseCors("AllowAll");
```


Add Serilog Seq (Show logger on browser)
```cs
// Config Serilog 
builder.Host.UseSerilog((ctx, lc) => lc.WriteTo.Console().ReadFrom.Configuration(ctx.Configuration));
```

In file appsetting.json remove old code.
```cs
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
```

And add this code instead.
```json
  "Serilog": {
    "MinimumLevel": {
      "Default": "Information",
      "Override": {
        "Microsoft": "Warning",
        "Microsoft.Hosting.Lifetime": "Information"
      }
    },
    "WriteTo": [
      {
        "Name": "File",
        "Args": {
          "path": "./logs/log-.txt",
          "rollingInterval": "Day"
        }
      },
      {
        "Name": "Seq",
        "Args": {
          "serverUrl": "http://localhost:5341"
        }
      }
    ]
  },
```


Add Serilog Request Logging
```cs
app.UseSerilogRequestLogging();
```

### Connect database
Connect database.
With PostgresSB
```json
 "ConnectionStrings": {
    "HotelListingDbConnectionString": "Host=localhost;Database=postgres;Port=6543;Username=super_admin;Password=admin@1234"
  },
```

With SQLServer
```json
 "ConnectionStrings": {
    "HotelListingDbConnectionString": "Server=(localhost)\mssqllocaldb;Database=HotelListingAPIDb;Trusted_Connection=True;MultipleActiveResultSets=True"
  },
```


Then add this code in Program.cs file for connect database.
```cs
// Connect Database.
var connectionString = builder.Configuration.GetConnectionString("HotelListingDbConnectionString");
builder.Services.AddDbContext<HotelListingDbContext>(options => options.UseNpgsql(connectionString));

```

and Create directory name Data and create new Class file Name ...DbContext and push this code into the file.
```cs
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace HotelListingAPI.VSCode.Data
{
    public class HotelListingDbContext: DbContext
    {
        public HotelListingDbContext(DbContextOptions options): base(options) {}
    }
}
```

Create Models.
First I will Create Country.cs file 
```cs
namespace HotelListingAPI.VSCode.Data
{
    public class Country
    {

        public int Id { get; set; }
        public string? Name { get; set; }
        public string? ShortName { get; set; }

        public virtual List<Hotel> Hotels { get; set; }
    }
}
```

and then create Hotel.cd file
```cs
namespace HotelListingAPI.VSCode.Data
{
    public class Hotel
    {
        [Required]
        [Key]
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Address { get; set; }
        public double Rating { get; set; }

        [ForeignKey(nameof(CountryId))]
        public int CountryId { get; set; }
        public Country Country { get; set; }
    }
}
```
update code in HotelListDbContext.cs like this 
```cs
namespace HotelListingAPI.VSCode.Data
{
    public class HotelListingDbContext: DbContext
    {
        public HotelListingDbContext(DbContextOptions options): base(options) {}

        // Add DbSet
        public DbSet<Hotel> Hotels { get; set; }
        public DbSet<Country> Countries { get; set; }
    }
}
```


** If want to seed database add following code in HotelListingDbContext.cs
```cs
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
```

Use command line for migrate and update database
> Command line for make migration
```
dotnet ef migrations add "Message"
```

>Command line of update database, This command it's will insert data in the table.
```
dotnet ef database update
```

### Key Terms and Definitions

Our scaffolded code features some keywords that are defined below for full understanding.

__Task__ - A task in C# is used to implement Task-based Asynchronous Programming. The Task object is typically executed asynchronously on a thread pool thread rather than synchronously on the main thread of the application.

__async__ - Signals to the compiler that this method contains an await statement; it contains asynchronous operations.

__await__ - The await keyword provides a non-blocking way to start a task, then continue execution when that task completes.

__ActionResult__ - An action is capable of returning a specific data type (see WeatherForecastController action).  When multiple return types are possible, it's common to return ActionResult, IActionResult or ActionResult<T>, where T represents the data type to be returned.



Command for run project 
```
dotnet run 

```

```
dotnet watch run
```




## Refactor Code
// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754

Create directory name Models and then create subdirectory inside Models name Country,
Create file name CreateCountryDto.cs
```cs
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelListingAPI.VSCode.Models.Country
{
    public class CreateCountryDto
    {
        [required]
        public string? Name { get; set; }
        public string? ShortName { get; set; }
    }
}
```
> and implement it instead of data in Controller.cs file like this (CreateCountryDto createCountry)
```cs
[HttpPost]
public async Task<ActionResult<Country>> CreateCountry(CreateCountryDto createCountry)
{
    var country = new Country {
        Name = createCountry.Name,
        ShortName = createCountry.ShortName
    };
    
    _context.Countries.Add(country);
    await _context.SaveChangesAsync();
    return CreatedAtAction("GetCountry", new { id = country.Id }, country);

}
```

### Add AutoMapper for map dto with data
Install "AutoMapper.Extensions.Microsoft.DependencyInjection" on nuGet from VSCode extension or web browser [Auto Mapper](https://www.nuget.org/packages/AutoMapper.Extensions.Microsoft.DependencyInjection)

This step it's will use Auto Mapper for mapping Dto with data model files after install AutoMapper.

Create folder directory Name : Configurations , And then create file MapperConfig.cs file and push this code into file.
```cs
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using HotelListingAPI.VSCode.Data; // Country
using HotelListingAPI.VSCode.Models.Country; // CountryDto

namespace HotelListingAPI.VSCode.Configurations
{
    public class MapperConfig : Profile
    {
        public MapperConfig()
        {
            CreateMap<Country, CreateCountryDto>().ReverseMap();
        }
    }
}
```
create method MapperConfig and Map Models with Dto file like the example.


Then go to Program.cs file and add Mapper config like this.
Import Configuration file with 
```cs
using HotelListingAPI.VSCode.Configurations;
```

Add following code before __var app = builder.Build();__
```cs
builder.Services.AddAutoMapper(typeof(MapperConfig));
```
Finally update CountriesController.cs file like this.
```cs
[HttpPost]
public async Task<ActionResult<Country>> CreateCountry(CreateCountryDto createCountry)
{
    // var countryOld = new Country {
    //     Name = createCountry.Name,
    //     ShortName = createCountry.ShortName
    // };

    // Use this code for mapping Dto With Model  
    var country = _mapper.Map<Country>(createCountry);

    _context.Countries.Add(country);
    await _context.SaveChangesAsync();
    return CreatedAtAction("GetCountry", new { id = country.Id }, country);
}
```


### Mapper with relational data






## Reference
- https://dev.to/moe23/net-6-with-postgresql-576a