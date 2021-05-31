using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace word_count_app.api.App_Start
{
    internal static class CorsConfiguration
    {
        internal static void Register(IServiceCollection services)
        {
            _ = services.AddCors();
        }

        internal static void UseCors(IApplicationBuilder app)
        {
            _ = app.UseCors(builder =>
            {
                _ = builder.AllowAnyHeader()
                .AllowAnyMethod()
                .AllowAnyOrigin();
            });
        }
    }
}
