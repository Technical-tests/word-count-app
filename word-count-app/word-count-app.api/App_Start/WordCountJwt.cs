using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

using System;
using System.Diagnostics.CodeAnalysis;
using System.Text;

using word_count_app.api.Services;
using word_count_app.common.Resources;

namespace word_count_app.api.App_Start
{
    [ExcludeFromCodeCoverage]
    internal static class WordCountJwt
    {
        public static void Register(IServiceCollection services)
        {
            _ = services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.Events = new JwtBearerEvents
                {
                    OnTokenValidated = ValidateJwt.TokenValidate
                };

                options.SaveToken = true;
                options.RequireHttpsMetadata = false;
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateAudience = true,
                    ValidateIssuer = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidAudience = GenericValuesResource.Audience,
                    ValidIssuer = GenericValuesResource.Issuer,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(GenericValuesResource.JwtKey)),
                    ClockSkew = TimeSpan.Zero
                };
            });
        }
    }
}
