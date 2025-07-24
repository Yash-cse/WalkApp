using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using WalkApp.DAL.WalkApp.DAL.Data;
using WalkApp.DAL.WalkApp.DAL.Repositories;
using WalkApp.Domain.WalkApp.Domain.Profiles;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// ConnectionString using DI
builder.Services.AddDbContext<WalkAppDbContext>(Opt =>
Opt.UseSqlServer(builder.Configuration.GetConnectionString("WalkAppConnectionString")));

// Repositories 
builder.Services.AddScoped<IRegionRepository, SqlRegionRepository>();

// Automapper

builder.Services.AddAutoMapper(cfg => cfg.AddProfile<AutoMapperProfiles>());


var app = builder.Build();

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
