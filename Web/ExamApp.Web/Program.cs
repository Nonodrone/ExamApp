using ExamApp.Data;
using ExamApp.Data.Models;
using ExamApp.Data.Repositories;
using ExamApp.Web.Seed;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ExamApp.Web
{
    public class Program
    {
        public static void ConfigureServices(WebApplicationBuilder builder)
        {


            // Add services to the container.
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

            builder.Services.AddDbContext<ExamAppDbContext>(options =>
                options.UseSqlServer(connectionString));

            //Add Custom Services
            //builder.Services.AddTransient<ProductRepository, ProductRepository>();


            //builder.Services.AddTransient<IProductService, ProductService>();


            builder.Services.AddDatabaseDeveloperPageExceptionFilter();

            builder.Services
                 .AddDefaultIdentity<ExamAppUser>(options =>
                 {
                     options.Password.RequireNonAlphanumeric = false;
                     options.Password.RequiredLength = 5;
                 })
                 .AddRoles<IdentityRole>()
                 .AddEntityFrameworkStores<ExamAppDbContext>();

            builder.Services.AddRazorPages();
            builder.Services.AddHttpContextAccessor();
        }

        public static void ConfigureAndRunApplication(WebApplicationBuilder builder)
        {
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.SeedRoles();

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();

                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");

                endpoints.MapControllerRoute(
                  name: "areas",
                  pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
                );
            });
            app.Run();
        }

        public static void Main(String[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            ConfigureServices(builder);
            ConfigureAndRunApplication(builder);
        }
    }
}