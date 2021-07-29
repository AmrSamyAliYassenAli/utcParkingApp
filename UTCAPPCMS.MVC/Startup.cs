using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UTCAPPCMS.MVC.Utility;
using AutoMapper;

namespace UTCAPPCMS.MVC
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
            var connectionString = Configuration.GetConnectionString("CRMMaster");
            services.AddEntityFrameworkCore(connectionString);
            services.AddControllersWithViews().AddRazorRuntimeCompilation().AddJsonOptions(op=>op.JsonSerializerOptions.PropertyNamingPolicy=null);

            services.AddAutoMapper(typeof(Startup));

            services.AddDistributedMemoryCache();
            services.AddSession();
            services.AddDataProtection();
            /*services.AddSession(options => {
                options.IdleTimeout = TimeSpan.FromMinutes(1);//You can set Time   
            });*/
            //


            //
            services.AddSwagger();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseSession();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                pattern: "{controller=Register}/{action=Login}/{id?}");
                //pattern: "{controller=Subscription}/{action=Index}/{id?}");
                //pattern: "{controller=UserManagment}/{action=Create}/{id?}");
                //pattern: "{controller=GroupPrivilage}/{action=Index}/{id?}"); 
                //pattern: "{controller=ParkingImages}/{action=Index}/{id?}");
            });

            app.UseSwagger();

            app.UseStaticFiles();
            app.UseSwaggerUI(c =>
            {
                if (env.IsDevelopment())
                {
                    c.SwaggerEndpoint("/swagger/v2/swagger.json", "Utc Parking App");
                }
                else
                {
                    c.SwaggerEndpoint("/UTCParkingAPP_Test/swagger/v2/swagger.json", "Utc Parking App");

                }

            });
            //app.UseSwaggerUI(options => options.SwaggerEndpoint("/swagger/v2/swagger.json", "PlaceInfo Services"));
        }
    }
}
