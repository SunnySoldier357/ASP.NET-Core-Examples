using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace StoringImages
{
    public class Startup
    {
        // Public Properties
        public IConfiguration Configuration { get; }

        // Constructors
        public Startup(IConfiguration configuration) => 
            Configuration = configuration;

        // Public Methods
        public void ConfigureServices(IServiceCollection services) => 
            services.AddMvc();

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}