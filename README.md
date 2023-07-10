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