using Devboost.DroneDelivery.Domain.VOs;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace Devboost.DroneDelivery.IoC
{
    [ExcludeFromCodeCoverage]
    public static class TokenConfig
    {
        public static IServiceCollection AddTokenConfiguration(this IServiceCollection services,
            IConfiguration configuration)
        {
            var appSettingsSection = configuration.GetSection("TokenSettings");
            services.Configure<AppSettingsVO>(appSettingsSection);
            
            var appSettings = appSettingsSection.Get<AppSettingsVO>();
            
            services.AddSingleton(appSettings);
            
            var key = Encoding.ASCII.GetBytes(appSettings.Secret);

            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = true;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    ClockSkew = TimeSpan.Zero,
                    ValidateLifetime = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidAudience = appSettings.ValidOn,
                    ValidIssuer = appSettings.Emitter
                };
            });

            return services;
        }
    }
}