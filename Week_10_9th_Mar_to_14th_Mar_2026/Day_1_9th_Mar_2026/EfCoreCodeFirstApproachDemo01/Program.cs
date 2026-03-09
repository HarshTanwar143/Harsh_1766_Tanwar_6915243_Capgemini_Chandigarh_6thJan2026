using Microsoft.EntityFrameworkCore;
using EfCoreCodeFirstApproachDemo01.Models;

namespace EfCoreCodeFirstApproachDemo01
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            // EF Core Connection
            var cs1 = builder.Configuration.GetConnectionString("cs1");
            builder.Services.AddDbContext<EmployeeDBContext>(options =>
                options.UseSqlServer(cs1));

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseRouting();

            app.UseAuthorization();

            app.MapStaticAssets();
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=EmployeeModels}/{action=Index}/{id?}")
                .WithStaticAssets();

            app.Run();
        }
    }
}