using Microsoft.EntityFrameworkCore;
using HRManagementSystem.Data;
using HRManagementSystem.Models;

namespace HRManagementSystem
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllersWithViews();

            // ✅ Register DbContext
            builder.Services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            var app = builder.Build();

            // ✅ Seed Data
            using (var scope = app.Services.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
                context.Database.EnsureCreated();

                if (!context.Companies.Any())
                {
                    var c1 = new Company { Name = "Google", Location = "California", Industry = "Technology" };
                    var c2 = new Company { Name = "Amazon", Location = "Seattle", Industry = "E-Commerce" };
                    context.Companies.AddRange(c1, c2);
                    context.SaveChanges();

                    context.Employees.AddRange(
                        new Employee { Name = "Alice", Position = "Developer", Email = "alice@google.com", CompanyId = c1.Id },
                        new Employee { Name = "Bob", Position = "Designer", Email = "bob@google.com", CompanyId = c1.Id },
                        new Employee { Name = "Charlie", Position = "Manager", Email = "charlie@amazon.com", CompanyId = c2.Id }
                    );
                    context.SaveChanges();
                }
            }

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
                pattern: "{controller=Home}/{action=Index}/{id?}")
                .WithStaticAssets();

            app.Run();
        }
    }
}