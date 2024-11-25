using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using SchemaPalWebApi.Repositories;
using SchemaPalWebApi.Services;
using Microsoft.EntityFrameworkCore;
using SchemaPalWebApi.SchemaPalDbContext;

var builder = WebApplication.CreateBuilder(args);

var jwtSettings = builder.Configuration.GetSection("JwtSettings");

builder.Services.AddControllers();

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = jwtSettings["Issuer"], 
        ValidAudience = jwtSettings["Audience"], 
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["SecretKey"]))
    };
});

builder.Services.AddAuthorization();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<IPasswordService, PasswordService>();

builder.Services.AddSingleton<IUserRepository, InMemoryUserRepository>();
builder.Services.AddSingleton<IDatabaseSchemaRepository, InMemoryDatabaseSchemaRepository>();

builder.Services.AddScoped<ITokenService, TokenService>();

//builder.Services.AddDbContext<SchemaPalContext>(options =>
//    options.UseSqlServer(builder.Configuration.GetConnectionString("SchemaPalDb")));
//builder.Services.AddScoped<IUserRepository, SqlUserRepository>();
//builder.Services.AddScoped<IDatabaseSchemaRepository, SqlDatabaseSchemaRepository>();

builder.Services.AddHostedService<DataSeederService>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSchemaPalApp",
        builder => builder.WithOrigins("https://localhost:7135")
        .AllowAnyHeader()
        .AllowAnyMethod());
});

var app = builder.Build();

app.UseHttpsRedirection();
app.UseRouting();

app.UseCors("AllowSchemaPalApp");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
