// Import essential namespaces
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WhalleyClimate.Web.Data;
using WhalleyClimate.Web.Services;
using WorleyClimate.Web.Services;

// Create the WebApplicationBuilder (bootstraps app, reads config, sets up DI container)
var builder = WebApplication.CreateBuilder(args);

// ==========================
// Configure Services Section
// ==========================

// 1. Set up the database connection string
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ??
    throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

// 2. Register the ApplicationDbContext with Entity Framework Core using SQLite
builder.Services.AddDbContext<WhalleyClimateDbContext>(options =>
    options.UseSqlite(connectionString));

// 3. Show detailed error pages during database exceptions (good for development)
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

// 4. Set up ASP.NET Core Identity for authentication
builder.Services.AddDefaultIdentity<IdentityUser>(options =>
    {
        options.SignIn.RequireConfirmedAccount = false;
        options.Password.RequireNonAlphanumeric = false;
        options.Password.RequireUppercase = false;
    })
    .AddEntityFrameworkStores<WhalleyClimateDbContext>()
    .AddDefaultUI();

// 5. Add Razor Pages support
builder.Services.AddRazorPages();

// 6. (Optional) Customize login and access denied paths
builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Account/Login";
    options.AccessDeniedPath = "/Account/AccessDenied";
});

// 7. Register custom application services (ImageService, FeedbackService, etc.)
// Example (assuming you have these):
builder.Services.AddScoped<IImageService, ImageService>();

// builder.Services.AddScoped<IFeedbackService, FeedbackService>();


// ==========================
// Configure Middleware Section
// ==========================

var app = builder.Build();

// Use different behaviors for Dev and Production environments
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();  // Easier EF migrations when developing
}
else
{
    app.UseExceptionHandler("/Error"); // Friendly error page in production
    app.UseHsts(); // Enforce HTTPS Strict Transport Security (secure browsers)
}

// Enforce HTTPS redirects
app.UseHttpsRedirection();

// Serve static files (CSS, JS, uploaded images)
app.UseStaticFiles();

// Set up request routing
app.UseRouting();

// Enable authentication and authorization
app.UseAuthentication(); // Important: add authentication before authorization
app.UseAuthorization();

// Map Razor Pages routes
app.MapRazorPages();

// Run the web application
app.Run();
