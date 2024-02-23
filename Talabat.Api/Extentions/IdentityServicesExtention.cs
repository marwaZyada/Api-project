using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Talabat.Core.Entities.Identity;
using Talabat.Core.Services;
using Talabat.Repository.Data.Identity;
using Talabat.Services;

namespace Talabat.Api.Extentions
{
    public static class IdentityServicesExtention
    {
        public static IServiceCollection AddIdentityServices(this IServiceCollection Services
            ,IConfiguration configuration)
        {
            Services.AddScoped<ITokenService, TokenService>();
          Services.AddIdentity<ApplicationUser, IdentityRole>(Options =>
          {
              Options.Password.RequireDigit =true;
              Options.Password.RequireNonAlphanumeric = true;
              Options.Password.RequireLowercase = true;
              Options.Password.RequireUppercase = true;
          }).AddEntityFrameworkStores<AppIdentityDbContext>();
            Services.AddAuthentication(Options =>
            {
                Options.DefaultAuthenticateScheme=JwtBearerDefaults.AuthenticationScheme;
                Options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(Options =>
                {
                    Options.TokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidateIssuer= true,
                        ValidIssuer = configuration["JWT:ValidIssuer"],
                        ValidateAudience= true,
                        ValidAudience= configuration["JWT:ValidAudience"],
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey=true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Key"]))

                    };
                });

            return Services;
        }
    }
}
