using Authsome;
using DashReportViewer.ClickUp;
using DashReportViewer.Shared.Models;
using DashReportViewer.Shared.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace DashReportViewer
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
            //services.AddDbContext<dbcontext>(options =>
            //{
            //    options.UseSqlServer(mydbconnectionstring);
            //}, ServiceLifetime.Scoped);

            services.AddScoped<IReportService, ReportService>();
            services.AddScoped<IAuthsomeService, AuthsomeService>();
            services.AddScoped<IClickUpService, ClickUpService>();


            var appSettings = Configuration.GetSection("DashReportAppSettings");
            services.Configure<DashReportAppSettings>(appSettings);

            services.AddControllersWithViews();
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
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();


            app.UseStaticFiles();
            //app.UseStaticFiles(StaticFiles.UseDashReportViewer());


            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
