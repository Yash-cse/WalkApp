using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using WalkApp.DAL.WalkApp.DAL.Data;
using WalkApp.DAL.WalkApp.DAL.Repositories.WalkApp.DAL.Repositories.Interface;
using WalkApp.DAL.WalkApp.DAL.Repositories.WalkApp.DAL.Repositories.Sql;
using WalkApp.Domain.WalkApp.Domain.Profiles;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// ConnectionString using DI
builder.Services.AddDbContext<WalkAppDbContext>(Opt =>
Opt.UseSqlServer(builder.Configuration.GetConnectionString("WalkAppConnectionString")));

//Auth DbContext injection
builder.Services.AddDbContext<WalkAppAuthDbContext>(opt =>
opt.UseSqlServer(builder.Configuration.GetConnectionString("WalkAppAuthConnectionString")));

// Repositories 
builder.Services.AddScoped<IRegionRepository, SqlRegionRepository>();
builder.Services.AddScoped<IWalkRepository, SqlWalkRepository>();
builder.Services.AddScoped<IDifficultyRepository, SqlDifficultyRepository>();

// Automapper
builder.Services.AddAutoMapper(cfg => cfg.AddProfile<MainAutoMapper>());

//Autherntication JWT
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(opt =>
    opt.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
    });


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

//Authentication 
app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
