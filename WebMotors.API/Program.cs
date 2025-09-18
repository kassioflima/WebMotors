using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using System;
using WebMotors.API.Configurations;
using WebMotors.API.Middleware;
using WebMotors.API.Models;
using WebMotors.API.Services;
using WebMotors.Domain.Shared.Models;
using WebMotors.Infra.CrossCutting.IoC;

var builder = WebApplication.CreateBuilder(args);

// Configure Serilog
Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .CreateLogger();

// Use Serilog
builder.Host.UseSerilog();

// Add services to the container
builder.Services.AddControllers().AddNewtonsoftJson(options =>
    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);

builder.Services.AddContextConfiguration(builder.Configuration);

// Configure Identity
builder.Services.AddIdentity<WebMotors.Domain.Shared.Models.ApplicationUser, IdentityRole>(options =>
{
    // Password settings
    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = true;
    options.Password.RequiredLength = 6;
    options.Password.RequiredUniqueChars = 1;

    // Lockout settings
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
    options.Lockout.MaxFailedAccessAttempts = 5;
    options.Lockout.AllowedForNewUsers = true;

    // User settings
    options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
    options.User.RequireUniqueEmail = true;
})
.AddEntityFrameworkStores<WebMotors.Data.Context.DataContext>()
.AddDefaultTokenProviders();

// Add custom services
builder.Services.AddScoped<AuthService>();
builder.Services.AddScoped<SeedDataService>();

builder.Services.AddSwaggerDocumentation();

builder.Services.AddTokenSecurity(builder.Configuration);

builder.Services.AddRouting(options => options.LowercaseUrls = true);

NativeInjectorBootStrapper.RegisterServices(builder.Services);

var app = builder.Build();

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

// Add exception handling middleware
app.UseMiddleware<ExceptionHandlingMiddleware>();

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.UseSwaggerConfig();

// Execute seeding on startup
using (var scope = app.Services.CreateScope())
{
    try
    {
        var seedDataService = scope.ServiceProvider.GetRequiredService<SeedDataService>();
        await seedDataService.SeedAsync();
        Log.Information("Seeding executado com sucesso");
    }
    catch (Exception ex)
    {
        Log.Error(ex, "Erro ao executar seeding");
    }
}

try
{
    Log.Information("Starting WebMotors API");
    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Application terminated unexpectedly");
}
finally
{
    Log.CloseAndFlush();
}
