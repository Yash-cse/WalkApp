using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using WalkApp.DAL.WalkApp.DAL.Data;
using WalkApp.DAL.WalkApp.DAL.Repositories.WalkApp.DAL.Repositories.Interface;
using WalkApp.DAL.WalkApp.DAL.Repositories.WalkApp.DAL.Repositories.Sql;
using WalkApp.Domain.WalkApp.Domain.Profiles;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Identity;

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
builder.Services.AddScoped<ITokenRepository, TokenRepository>();

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
        IssuerSigningKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
    });

//Setting up Identity
builder.Services.AddIdentityCore<IdentityUser>()
    .AddRoles<IdentityRole>()
    .AddTokenProvider<DataProtectorTokenProvider<IdentityUser>>("WalkApp")
    .AddEntityFrameworkStores<WalkAppAuthDbContext>()
    .AddDefaultTokenProviders();

//Setting up Identity Options
builder.Services.Configure<IdentityOptions>(Opt =>
{
    Opt.Password.RequireDigit = false;
    Opt.Password.RequireLowercase = false;
    Opt.Password.RequireNonAlphanumeric = false;
    Opt.Password.RequireUppercase = false;
    Opt.Password.RequiredLength = 6;
    Opt.Password.RequiredUniqueChars = 1;
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
