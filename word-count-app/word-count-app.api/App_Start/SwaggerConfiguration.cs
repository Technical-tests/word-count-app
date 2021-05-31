using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Reflection;

namespace word_count_app.api.App_Start
{
    [ExcludeFromCodeCoverage]
    internal static class SwaggerConfiguration
    {
        internal static void Register(IServiceCollection services)
        {
            string version = GetAssemblyVersion();
            _ = services.AddSwaggerGen(g =>
            {
                g.SwaggerDoc(name: version, new Microsoft.OpenApi.Models.OpenApiInfo { Title = "word-count-app", Version = "v1" });

                string xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                string xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                g.IncludeXmlComments(xmlPath);

                g.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 12345abcdef\"",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });

                g.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            },
                            Scheme = "oauth2",
                            Name = "Bearer",
                            In = ParameterLocation.Header,

                        },
                        new List<string>()
                    }
                });
            });
        }

        internal static void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            _ = app.UseSwagger();
            _ = app.UseSwaggerUI(config =>
            {
                config.SwaggerEndpoint(url: $"/swagger/{GetAssemblyVersion()}/swagger.json", name: "word-count-app v1");
            });
        }

        private static string GetAssemblyVersion()
        {
            return Assembly.GetExecutingAssembly().GetName().Version.ToString().Substring(0, 3);
        }
    }
}
