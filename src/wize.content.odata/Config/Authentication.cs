using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace wize.content.odata.Config
{
    public static class Authentication
    {
        public static IServiceCollection AddJwt(this IServiceCollection services, IConfiguration configuration)
        {
            JwtModel jwt = new JwtModel();
            jwt.ValidAudience = configuration.GetValue<string>("JwtAuthentication_ValidAudience");
            jwt.ValidIssuer = configuration.GetValue<string>("JwtAuthentication_ValidIssuer");
            
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidIssuer = jwt.ValidIssuer,
                    ValidAudience = jwt.ValidAudience,
                };
                options.SaveToken = true;
                options.IncludeErrorDetails = true;
                options.RequireHttpsMetadata = false;
                options.Authority = jwt.ValidIssuer;
                options.Audience = jwt.ValidAudience;
                //options.TokenValidationParameters = tokenParameters;
            });

            services.AddAuthorization(options =>
            {
                options.AddPolicy("read:content", policy => {
                    policy.Requirements.Add(new HasPermissionsRequirement("read:content", jwt.ValidIssuer));
                });
                options.AddPolicy("add:content", policy => {
                    policy.Requirements.Add(new HasPermissionsRequirement("add:content", jwt.ValidIssuer));
                });
                options.AddPolicy("list:content", policy => {
                    policy.Requirements.Add(new HasPermissionsRequirement("list:content", jwt.ValidIssuer));
                });
                options.AddPolicy("update:content", policy => {
                    policy.Requirements.Add(new HasPermissionsRequirement("update:content", jwt.ValidIssuer));
                });
                options.AddPolicy("delete:content", policy => {
                    policy.Requirements.Add(new HasPermissionsRequirement("delete:content", jwt.ValidIssuer));
                });
            });
            services.AddSingleton<IAuthorizationHandler, HasPermissionsHandler>();

            return services;
        }

        public static IApplicationBuilder UseJwt(this IApplicationBuilder app)
        {
            app.UseAuthentication();
            app.UseAuthorization();

            return app;
        }
    }
}
