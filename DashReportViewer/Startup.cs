using Authsome;
using DashReportViewer.ClickUp;
using DashReportViewer.ClickUp.Settings;
using DashReportViewer.Context;
using DashReportViewer.GA;
using DashReportViewer.GoogleSheets;
using DashReportViewer.Models;
using DashReportViewer.Shared.Models;
using DashReportViewer.Shared.Services;
using DashReportViewer.Stripe;
using DashReportViewer.Stripe.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;

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
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddDbContext<DashReportViewerContext>(options =>
                options.UseSqlServer("Data Source=localhost;Initial Catalog=DashReportViewer;Integrated Security=true;Trusted_Connection=true;TrustServerCertificate=true;"));

            services.AddDefaultIdentity<ApplicationUser>()
                .AddEntityFrameworkStores<DashReportViewerContext>();


            services.Configure<IdentityOptions>(options =>
            {
                // Password settings.
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequireUppercase = true;
                options.Password.RequiredLength = 6;
                options.Password.RequiredUniqueChars = 1;

                // Lockout settings.
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.AllowedForNewUsers = false;

                // User settings.
                //options.User.AllowedUserNameCharacters =
                //"abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
                options.User.RequireUniqueEmail = true;
            });


            services.AddScoped<IReportService, ReportService>();
            services.AddScoped<IAuthsomeService, AuthsomeService>();
            services.AddScoped<IClickUpService, ClickUpService>();
            services.AddScoped<IGAService, GAService>();
            services.AddScoped<IAzureDevOpsService, AzureDevOpsService>(); // required for AzureDevOps Webhook
            services.AddScoped<IStripeService, StripeService>();
            services.AddScoped<IGSheetsService, GSheetsService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<INotesService, NotesService>();

            var appSettings = Configuration.GetSection("DashReportAppSettings");
            services.Configure<DashReportAppSettings>(appSettings);

            var clickUpSettings = Configuration.GetSection("ClickUpSettings");
            services.Configure<ClickUpSettings>(clickUpSettings);

            var StripeSettings = Configuration.GetSection("StripeSettings");
            services.Configure<StripeSettings>(StripeSettings);

            var GSheetSettings = Configuration.GetSection("GoogleService");
            services.Configure<GoogleJson>(GSheetSettings);


            services.AddControllersWithViews();
            services.AddRazorPages();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            ApplyMigration(app);

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

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Report}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });
        }

        private void ApplyMigration(IApplicationBuilder app)
        {
            using (var scope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                var reportContext = scope.ServiceProvider.GetRequiredService<DashReportViewerContext>();
                reportContext.Database.Migrate();


                var userService = scope.ServiceProvider.GetRequiredService<IUserService>();

                userService.CreateUser(new Shared.Models.User.NewUser()
                {
                    FirstName = "admin",
                    LastName = "admin",
                    Email = "admin@admin.com",
                    Password = "Admin1234!",
                    Role = Shared.Models.User.Role.Admin,
                    Timezone = "Eastern Standard Time"
                }).GetAwaiter().GetResult();

            }
        }
    }
}
