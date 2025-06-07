using Microsoft.EntityFrameworkCore;
using ISF.Data.Model;
using ISF.Core;
using ISF.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;


internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = builder.Configuration["Jwt:Issuer"],
                ValidAudience = builder.Configuration["Jwt:Audience"],
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:SecretKey"]!))
            };
        });

        builder.Services.AddDbContext<ISFDbContext>(options =>
        options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

        

        builder.Services.AddOpenApi();
        builder.Services.AddControllers();
        builder.Services.AddHttpContextAccessor();
        builder.Services.AddScoped<ISentencesRepository, SentenceRepository>();
        builder.Services.AddScoped<ISoundRepository, SoundRepository>();
        builder.Services.AddScoped<IPlatformTokenQueryService, PlatformTokenQueryService>();
        builder.Services.AddScoped<ITokenService, TokenService>();
        
            
        builder.Services.AddLogging(loggingBuilder =>
        {
            loggingBuilder.AddConsole();
            loggingBuilder.AddDebug();
        });

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.MapOpenApi();
        }
        app.MapControllers();
        app.UseAuthentication();
        app.UseAuthorization();
        app.UseHttpsRedirection();
        app.Run();
    }
}