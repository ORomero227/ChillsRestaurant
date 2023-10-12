using ChillsRestaurant.Models;
using ChillsRestaurant.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace ChillsRestaurant
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

            builder.Services.AddDbContext<ChillsRestaurantDBContext>(options => options.UseSqlServer(builder.Configuration["ConnectionStrings:DefaultConnection"]));

            builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
                            .AddRoles<IdentityRole>()
                            .AddEntityFrameworkStores<ChillsRestaurantDBContext>()
                            .AddDefaultTokenProviders();

            builder.Services.Configure<IdentityOptions>(options => {
                //Login
                options.SignIn.RequireConfirmedEmail = true;
                options.Lockout.AllowedForNewUsers = true;
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(1);
                options.Lockout.MaxFailedAccessAttempts = 3;

                //Password Setting
                options.Password.RequiredLength = 3;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                
                //Email Setting
                options.User.RequireUniqueEmail = true;
            });

            builder.Services.ConfigureApplicationCookie(options => 
            {
                options.LoginPath = "/Account/Login";
            });

            //Add services to container
            builder.Services.AddControllersWithViews();

            builder.Services.AddScoped<UsersSetupService>();

            var app = builder.Build();

            using (var scope = app.Services.CreateScope())
            {
                var usersSetupService = scope.ServiceProvider.GetRequiredService<UsersSetupService>();

                await usersSetupService.SetupRolesAndUsersAsync();
            }

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

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "accountProfile",
                pattern: "Account/AccountProfile/{username}",
                defaults: new { controller = "Account", action = "AccountProfile" });

            app.MapControllerRoute(
                name: "moreInfo",
                pattern: "Manager/MoreInfo/{username}",
                defaults: new { controller = "Manager",action = "MoreInfo" });
            
            app.MapControllerRoute(
                name: "moreInfo",
                pattern: "Manager/EditAccount/{username}",
                defaults: new { controller = "Manager",action = "EditAccount" });

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}