//using Microsoft.EntityFrameworkCore;
//using Microsoft.Extensions.Logging;
//using FleetPulse_BackEndDevelopment.Data;

//var builder = WebApplication.CreateBuilder(args);

//builder.Services.AddDbContext<FleetPulseDbContext>(options =>
//{
//    options.UseSqlServer(builder.Configuration.GetConnectionString("VMSDBConnection"));
//});

//builder.Services.AddControllers();
//// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
//builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();

//var app = builder.Build();

//// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
//    app.UseSwagger();
//    app.UseSwaggerUI();
//}

//app.UseHttpsRedirection();

//app.UseAuthorization();

//app.MapControllers();

//app.Run();

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using FleetPulse_BackEndDevelopment.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<FleetPulseDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("FleetPulseDBConnection"));
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Apply database migrations during startup
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    var dbContext = services.GetRequiredService<FleetPulseDbContext>();
    dbContext.Database.Migrate();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
