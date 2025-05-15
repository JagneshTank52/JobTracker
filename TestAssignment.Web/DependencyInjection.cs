
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using TestAssignment.Entity.Data;
using TestAssignment.Repository.Implementaion;
using TestAssignment.Repository.Interface;
using TestAssignment.Service.Implementaion;
using TestAssignment.Service.Interface;

namespace TestAssignment.Web;

public static class DependencyInjection
{
    public static void RegisterServices(IServiceCollection services, string connectionString)
    {
        // Connect Database
        services.AddDbContext<TestAssignmentContext>(options =>
        options.UseNpgsql(connectionString,x => x.MigrationsAssembly("TestAssignment.Entity")));

        // Register JWT Authentication
        var key = Encoding.UTF8.GetBytes(services.BuildServiceProvider().GetRequiredService<IConfiguration>()["JwtSettings:Key"]!);
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {

                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidIssuer = services.BuildServiceProvider().GetRequiredService<IConfiguration>()["JwtSettings:Issuer"],
                    ValidateAudience = true,
                    ValidAudience = services.BuildServiceProvider().GetRequiredService<IConfiguration>()["JwtSettings:Audience"],
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    RoleClaimType = ClaimTypes.Role
                };

                options.Events = new JwtBearerEvents
                {
                    OnMessageReceived = context =>
                    {
                        if (context.Request.Cookies.ContainsKey("AuthToken"))
                        {
                            context.Token = context.Request.Cookies["AuthToken"];
                        }
                        return Task.CompletedTask;
                    }
                };
            });
        
        // Register Service
        services.AddScoped<IAuthenticationService, AuthenticationService>();
        services.AddScoped<ITokenService, TokenService>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();


    }

}
