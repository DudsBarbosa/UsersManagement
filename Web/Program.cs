using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Web.Configuration;
namespace Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddDbContext<UserContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("WebContext"), b => b.MigrationsAssembly("Infrastructure")));

            // Provides useful error information in the development environment.
            builder.Services.AddDatabaseDeveloperPageExceptionFilter();

            // Add services to the container.
            builder.Services.AddCoreServices();
            builder.Services.AddControllersWithViews();

            var app = builder.Build();

            app.Logger.LogInformation("App created...");

            app.Logger.LogInformation("Seeding Database...");


            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.UseRequestLocalization("pt-BR");
            app.Run();
        }
    }
}
