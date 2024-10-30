using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace LikeTrackerAPI.Commons.Extensions
{
    public static class JwtExtensions
    {
        public static IServiceCollection AddJwtAuthentication(this IServiceCollection services, IConfiguration Configuration)
        {
            services.AddAuthentication(opts =>
            {
                opts.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opts.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                opts.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(cfg =>
            {
                cfg.SaveToken = true;
                cfg.RequireHttpsMetadata = false;
                cfg.TokenValidationParameters = new TokenValidationParameters
                {

                    ValidateIssuerSigningKey = Configuration.GetValue<bool>("Jwt:ValidateSigningKey"),
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration.GetValue<string>("ApplicationSettings:SecurityKey"))),
                    ValidateIssuer = Configuration.GetValue<bool>("Jwt:ValidateIssuer"),
                    ValidIssuer = Configuration.GetValue<string>("Jwt:Issuer"),
                    ValidateAudience = Configuration.GetValue<bool>("Jwt:ValidateAudience"),
                    ValidAudience = Configuration.GetValue<string>("Jwt:Audience"),
                    ValidateLifetime = Configuration.GetValue<bool>("Jwt:ValidateLifeTime"),
                    ClockSkew = TimeSpan.FromMinutes(Configuration.GetValue<int>("Jwt:DateToleranceMinutes"))
                };
            });

            return services;
        }
    }
}

