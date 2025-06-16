using Library.Database;
using Library.Services.Auth;
using Library.Services.IssueBook;
using Library.Services.MemberManagement;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

// 1. Add DbContext
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// 2. Add Identity
builder.Services.AddIdentity<IdentityUser, IdentityRole>()
    .AddEntityFrameworkStores<AppDbContext>()
    .AddDefaultTokenProviders();

// 3. Configure Identity options (for testing, simple password)
builder.Services.Configure<IdentityOptions>(options =>
{
    options.Password.RequireDigit = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireUppercase = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequiredLength = 6;
});

// 4. Configure cookie authentication
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/auth/login";
        options.LogoutPath = "/auth/logout";

        options.Cookie.HttpOnly = true;
        options.Cookie.IsEssential = true;
        options.Cookie.SameSite = SameSiteMode.Lax;
        options.Cookie.SecurePolicy = CookieSecurePolicy.SameAsRequest;

        options.ExpireTimeSpan = TimeSpan.FromMinutes(30);
        options.SlidingExpiration = true;

        options.Events = new CookieAuthenticationEvents
        {
            OnSigningOut = async context =>
            {
                // Clear session on sign out event
                context.HttpContext.Session.Clear();
                await Task.CompletedTask;
            }
        };
    });

// 5. Register your services
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IBookIssueService, BookIssueService>();
builder.Services.AddScoped<IMemberService, MemberService>();

// 6. Add MVC and AutoMapper
builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddControllersWithViews();

// 7. Add session
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

WebApplication app = builder.Build();

// 8. Middleware pipeline
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseSession();

app.UseAuthentication();
app.UseAuthorization();

// 9. Prevent caching so back button doesn't show logged-in pages after logout
app.Use(async (context, next) =>
{
    context.Response.Headers["Cache-Control"] = "no-cache, no-store, must-revalidate";
    context.Response.Headers["Pragma"] = "no-cache";
    context.Response.Headers["Expires"] = "0";
    await next();
});

// 10. Access control middleware
app.Use(async (context, next) =>
{
    string path = context.Request.Path.Value?.ToLower() ?? "";
    bool isAuthenticated = context.User.Identity?.IsAuthenticated ?? false;

    // Allow logout endpoint always (no redirect loops)
    if (path == "/auth/logout")
    {
        await next();
        return;
    }

    switch (isAuthenticated)
    {
        // Allow unauthenticated users to access login and register pages
        case false when path is "/auth/login" or "/auth/register":
            await next();
            return;
        // Redirect authenticated users away from login and register pages
        case true when path is "/auth/login" or "/auth/register":
            context.Response.Redirect("/home");
            return;
        // Block unauthenticated users from all other pages (except static content)
        case false when !path.StartsWith("/auth") &&
                        !path.StartsWith("/css") && !path.StartsWith("/js") && !path.StartsWith("/lib"):
            context.Response.Redirect("/auth/login");
            return;
        default:
            await next();
            break;
    }
});

// 11. Root redirect to login page
app.MapGet("/", context =>
{
    context.Response.Redirect("/auth/login");
    return Task.CompletedTask;
});

// 12. MVC routing
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Auth}/{action=Login}/{id?}");

app.Run();