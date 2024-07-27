using DotNetEnv;
using fleet_management_backend.Data;
using fleet_management_backend.Repositories.Auth;
using fleet_management_backend.Repositories.Drivers;
using fleet_management_backend.Repositories.File;
using fleet_management_backend.Repositories.Fuels;
using fleet_management_backend.Repositories.Maintenances;
using fleet_management_backend.Repositories.Trips;
using fleet_management_backend.Repositories.Users;
using fleet_management_backend.Repositories.Vehicles;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

Env.Load();
builder.Configuration.AddEnvironmentVariables();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var awsOptions = builder.Configuration.GetAWSOptions();
awsOptions.Region = Amazon.RegionEndpoint.GetBySystemName(Environment.GetEnvironmentVariable("AWS_REGION"));

builder.Services.AddDefaultAWSOptions(awsOptions);

builder.Services.AddAWSService<Amazon.S3.IAmazonS3>();

builder.Services.AddDbContext<FleetManagerDbContext>(option =>
    option.UseMySql(Environment.GetEnvironmentVariable("FLEETMANAGER_CONNECTION"),
    ServerVersion.AutoDetect(Environment.GetEnvironmentVariable("FLEETMANAGER_CONNECTION"))));

builder.Services.AddScoped<IAuthRepository, AuthRepository>();
builder.Services.AddScoped<IFileRepository, FileRepository>();
builder.Services.AddScoped<IVehicleRepository, VehicleRepository>();
builder.Services.AddScoped<IDriverRepository, DriverRepository>();
builder.Services.AddScoped<ITripRepository, TripRepository>();
builder.Services.AddScoped<IFuelRepository, FuelRepository>();
builder.Services.AddScoped<IMaintenanceRepository, MaintenanceRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();

var key = Encoding.UTF8.GetBytes(Environment.GetEnvironmentVariable("JWT_KEY"));

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = Environment.GetEnvironmentVariable("JWT_ISSUER"),
        ValidAudience = Environment.GetEnvironmentVariable("JWT_AUDIENCE"),
        IssuerSigningKey = new SymmetricSecurityKey(key)
    };

    options.Events = new JwtBearerEvents
    {
        OnChallenge = context =>
        {
            context.HandleResponse();
            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            context.Response.ContentType = "application/json";
            var result = JsonSerializer.Serialize(new { message = "Unauthorized access" });
            return context.Response.WriteAsync(result);
        },
        OnForbidden = context =>
        {
            context.Response.StatusCode = StatusCodes.Status403Forbidden;
            context.Response.ContentType = "application/json";
            var result = JsonSerializer.Serialize(new { message = "Forbidden access" });
            return context.Response.WriteAsync(result);
        }
    };
});

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdminPolicy", policy => policy.RequireClaim("type", "Admin"));
    options.AddPolicy("FleetManagerPolicy", policy => policy.RequireClaim("type", "Fleet-Manager"));
    options.AddPolicy("DriverPolicy", policy => policy.RequireClaim("type", "Driver"));
    options.AddPolicy("AdminOrFleetManagerPolicy", policy => policy.RequireAssertion(context =>
            context.User.HasClaim(c => c.Type == "type" && (c.Value == "Admin" || c.Value == "Fleet-Manager"))));
});

var app = builder.Build();

app.UseCors(options => options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
