using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using BenefitsDataService;
using BenefitsService.BenefitsService;
using Unity;

namespace WebApplication
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
            services.AddControllers();

            services.AddDbContext<BenefitsDbContext>(opt =>
                                               opt.UseInMemoryDatabase("BenefitsService"));

            services.AddScoped<IBenefitsDataSource, BenefitsDataSource>();
            
            services.AddScoped<BenefitsService.BenefitsService.BenefitsDiscountCalculator.IDiscountCalculator, 
                BenefitsService.BenefitsService.BenefitsDiscountCalculator.BenefitsDiscountCalculator>();
            services.AddScoped<BenefitsService.BenefitsService.BenefitsCostCalulator.IBenefitsCostCalculator, 
                BenefitsService.BenefitsService.BenefitsCostCalulator.BenefitsCostCalculator>();
            services.AddScoped<IBenefitsService, BenefitsService.BenefitsService.BenefitsService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseDefaultFiles();
            app.UseStaticFiles();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
