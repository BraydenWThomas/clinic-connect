using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using LoginProject.Areas.Identity.Data;
using System.Configuration;
using System.Net;
using LoginProject.Models;

namespace LoginProject
{
    public class Program
    {
        public static async Task Main(string[] args)
        {

            var builder = WebApplication.CreateBuilder(args);
            var connectionString = builder.Configuration.GetConnectionString("ApplicationDbContextConnection") ?? throw new InvalidOperationException("Connection string 'ApplicationDbContextConnection' not found.");

            //SQL SERVER
            //builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connectionString));

            builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));
            
            builder.Services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = false)
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>();

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            builder.Services.AddRazorPages();


            var app = builder.Build();

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

            app.MapRazorPages();

            //Create Roles (every time server refreshed) <- Temp
            using (var scope = app.Services.CreateScope())
            {
                var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
                var roles = new[] { "Admin", "Practitioner", "Patient" };

                foreach (var role in roles)
                {
                    if (!await roleManager.RoleExistsAsync(role))
                        await roleManager.CreateAsync(new IdentityRole(role));
                }
            }

            //Create Users (every time server refreshed) <- Temp
            using (var scope = app.Services.CreateScope())
            {
                var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
                var roles = new[] { "Admin", "Practitioner", "Patient" };

                string email = "admin@admin.com";
                string password = "Password123!";
                string firstName = "User";
                string lastName = "Admin";
                string address = "asdasdasd";
                DateTime dob = DateTime.Now;
                string phoneNumber = "1231231";
                string state = "active";
                string postcode = "3233";
                string suburb = "asdasdas";
                string title = "Title";

                if (await userManager.FindByEmailAsync(email) == null)
                {
                    var user = new ApplicationUser();
                    user.UserName = email;
                    user.Email = email;
                    user.FirstName = firstName;
                    user.LastName = lastName;
                    user.Address = address;
                    user.Dob = dob;
                    user.PhoneNumber = phoneNumber;
                    user.State = state;
                    user.Postcode = postcode;
                    user.Suburb = suburb;
                    user.Title = title;


                    await userManager.CreateAsync(user, password);
                    await userManager.AddToRoleAsync(user, "Admin");
                }
                
            }

            app.Run();

        }
    

    }

}


