using AdminDashBoardV1._0._0.Helper;
using Ecommerce.Domain.Models.Contracts.UOW;
using Ecommerce.Domain.Models.Identity;
using Ecommerce.Presistence.Contexts;
using Ecommerce.Presistence.UnitOfWork;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace AdminDashBoardV1._0._0
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            // Add the configs of the contexts 
            builder.Services.AddDbContext<StoreIdntityDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("IdentityConnection")));
            builder.Services.AddDbContext<StoreDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            // Add the identity configs
            builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
            {
                // Password settings (optional - adjust as needed)
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireUppercase = true;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequiredLength = 6;

                // Lockout settings
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.AllowedForNewUsers = true;

                // User settings
                options.User.RequireUniqueEmail = true;
            })
            .AddEntityFrameworkStores<StoreIdntityDbContext>()
            .AddDefaultTokenProviders();


            //===============================================
            // UPDATED: Add Google OAuth Authentication
            //===============================================
            builder.Services.AddAuthentication()
                .AddGoogle(options =>
                {
                    // Use the same Google credentials from your API
                    options.ClientId = builder.Configuration["Authentication:Google:ClientId"]
                    ?? throw new InvalidOperationException("Google ClientId is not configured");

                    options.ClientSecret = builder.Configuration["Authentication:Google:ClientSecret"]
                        ?? throw new InvalidOperationException("Google ClientSecret is not configured");
                    // Dashboard-specific callback path
                    options.CallbackPath = "/signin-google-admin";

                    // Use cookie scheme for OAuth flow
                    options.SignInScheme = IdentityConstants.ExternalScheme;

                    // Save tokens for later use (optional)
                    options.SaveTokens = true;
                });

            // Configure Cookie Authentication
            builder.Services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = "/Admin/Login"; // Redirect to login page
                options.LogoutPath = "/Admin/Logout"; // Logout path
                options.AccessDeniedPath = "/Admin/AccessDenied"; // Access denied page
                options.ExpireTimeSpan = TimeSpan.FromHours(2); // Cookie expires in 2 hours by default
                options.SlidingExpiration = true; // Renew cookie if user is active
                options.Cookie.HttpOnly = true; // Prevent XSS attacks
                options.Cookie.SecurePolicy = CookieSecurePolicy.Always; // HTTPS only
                options.Cookie.SameSite = SameSiteMode.Strict; // CSRF protection
                options.Cookie.Name = "ECommerceAdminAuth"; // Custom cookie name
            });

            // Authorization Policies
            builder.Services.AddAuthorization(options =>
            {
                // Admin-only policy
                options.AddPolicy("AdminOnly", policy =>
                    policy.RequireRole("Admin", "SuperAdmin"));

                // Authenticated users only
                options.AddPolicy("AuthenticatedUsers", policy =>
                    policy.RequireAuthenticatedUser());
            });

            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            builder.Services.AddAutoMapper(cfg =>
            {
                cfg.AddProfile<productProfile>();
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles(); // Changed from MapStaticAssets
            app.UseRouting();

            //  IMPORTANT: Authentication must come before Authorization
            app.UseAuthentication(); //  ADD THIS
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Admin}/{action=Login}/{id?}");

            app.Run();
        }
    }
}
