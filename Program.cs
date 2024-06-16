using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using FleetPulse_BackEndDevelopment.Data;
using FleetPulse_BackEndDevelopment.Filters;
using FleetPulse_BackEndDevelopment.Services;
using FleetPulse_BackEndDevelopment.Services.Interfaces;
using FleetPulse_BackEndDevelopment.Configuration;
using FleetPulse_BackEndDevelopment.Helpers;
using AutoMapper;
using FleetPulse_BackEndDevelopment.Utilities;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
ConfigureServices(builder.Services, builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
Configure(app, app.Environment);

app.Run();

void ConfigureServices(IServiceCollection services, IConfiguration configuration)
{
    // Add controllers with options
    services.AddControllers(options =>
        options.SuppressImplicitRequiredAttributeForNonNullableReferenceTypes = true)
        .AddNewtonsoftJson(options =>
            options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);

    // Add Swagger
    services.AddEndpointsApiExplorer();
    services.AddSwaggerGen(option =>
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
        option.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());
        option.IgnoreObsoleteActions();
        option.IgnoreObsoleteProperties();
        option.CustomSchemaIds(type => type.FullName);
    });

    // Add authentication
    services.AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
    }).AddJwtBearer(o =>
    {
        o.TokenValidationParameters = new TokenValidationParameters
        {
            ValidIssuer = configuration["Jwt:Issuer"],
            ValidAudience = configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"])),
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true
        };
    });

    services.AddAuthorization();

    // Add CORS
    services.AddCors(options =>
    {
        options.AddDefaultPolicy(builder =>
        {
            builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
        });
    });

    // Add Db context
    services.AddDbContext<FleetPulseDbContext>(options =>
    {
        options.UseSqlServer(configuration.GetConnectionString("SqlServerConnectionString"), sqlOptions =>
        {
            sqlOptions.EnableRetryOnFailure(5, TimeSpan.FromSeconds(10), null);
        });
    });

    // Add AutoMapper
    var mapperConfig = new MapperConfiguration(cfg =>
    {
        cfg.AddProfile(new MappingProfiles());
    });
    
    var mapper = mapperConfig.CreateMapper();
    services.AddSingleton(mapper);

    // Register your services
    services.Configure<MailSettings>(configuration.GetSection("MailSettings"));
    services.AddTransient<IMailService, MailService>();
    services.AddTransient<IVerificationCodeService, VerificationCodeService>();
    services.AddTransient<IAuthService, AuthService>();
    services.AddScoped<DBSeeder>();
    services.AddScoped<IVehicleMaintenanceService, VehicleMaintenanceService>();
    services.AddScoped<IVehicleMaintenanceTypeService, VehicleMaintenanceTypeService>();
    services.AddScoped<IFuelRefillService, FuelRefillService>();
    services.AddScoped<IPushNotificationService, PushNotificationService>();
    services.AddScoped<IEmailService, EmailService>();
    services.AddScoped<IVehicleMaintenanceConfigurationService, VehicleMaintenanceConfigurationService>();

    // Add logging
    services.AddLogging();

    // Initialize Firebase
    FirebaseInitializer.InitializeFirebase();
}

void Configure(WebApplication app, IWebHostEnvironment env)
{
    app.UseHttpsRedirection();
    app.UseAuthentication();
    app.UseAuthorization();
    app.UseCors();

    if (env.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
        app.UseSeedDB();
    }

    app.MapControllers();
}
