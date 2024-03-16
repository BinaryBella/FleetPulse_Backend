using Microsoft.AspNetCore.Mvc;
using System.Text;
using Microsoft.OpenApi.Models;
using FleetPulse_BackEndDevelopment.Data;
using FleetPulse_BackEndDevelopment.Filters;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using FleetPulse_BackEndDevelopment.Helpers;
using Microsoft.IdentityModel.Tokens;
using FleetPulse_BackEndDevelopment.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers(options =>
                    options.SuppressImplicitRequiredAttributeForNonNullableReferenceTypes = true)
                .AddNewtonsoftJson(options =>
                    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
                );

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(option =>
{
    option.SwaggerDoc("v1", new OpenApiInfo { Title = "User Authentication", Version = "v1" });
    option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter a valid token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "bearer"
    });

    option.OperationFilter<AuthResponsesOperationFilter>();
});

// Add Db context
var dbProvider = builder.Configuration.GetConnectionString("Provider");
builder.Services.AddDbContext<FleetPulseDbContext>(options =>
{
    if (dbProvider == "SqlServer")
    {
        object value = options.UseSqlServer(builder.Configuration.GetConnectionString("SqlServerConnectionString"), b =>
        {
            b.EnableRetryOnFailure(5, TimeSpan.FromSeconds(10), null);
        });
    }
});

var serviceProvider = builder.Services.BuildServiceProvider();
var logger = serviceProvider.GetRequiredService<ILogger<ControllerBase>>();
builder.Services.AddSingleton(typeof(ILogger), logger);

var config = new MapperConfiguration(cfg =>
{
    cfg.AddProfile(new MappingProfiles());
});

var mapper = config.CreateMapper();
builder.Services.AddSingleton(mapper);

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(o =>
{
    o.TokenValidationParameters = new TokenValidationParameters
    {
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey
        (Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"])),
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = false,
        ValidateIssuerSigningKey = true
    };
});

builder.Services.AddAuthorization();

var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

//Add CORS
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(
        builder =>
        {
            //you can configure your custom policy
            builder.AllowAnyOrigin()
            .AllowAnyHeader()
            .AllowAnyMethod();
        });
});

// Declared services
builder.Services.AddScoped<DBSeeder>();
builder.Services.AddTransient<AuthService>();

var app = builder.Build();

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.UseCors();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseSeedDB();
}

app.MapControllers();
app.Run();
