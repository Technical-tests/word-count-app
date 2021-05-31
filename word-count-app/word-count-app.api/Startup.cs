using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using word_count_app.api.App_Start;

namespace word_count_app.api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            _ = services.AddControllers();
            CorsConfiguration.Register(services);
            DependencyInjectionConfiguration.Register(services);
            SwaggerConfiguration.Register(services);
            WordCountJwt.Register(services);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            SwaggerConfiguration.Configure(app, env);
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();                
            }

            CorsConfiguration.UseCors(app);
            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthorization();
            app.UseAuthentication();

            _ = app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
