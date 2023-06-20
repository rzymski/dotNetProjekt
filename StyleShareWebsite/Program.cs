using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using StyleShareWebsite.Data;
using StyleShareWebsite.DataAccess.Interfaces;
using StyleShareWebsite.DataAccess.Interfaces.Common;
using StyleShareWebsite.DataAccess.Services;
using StyleShareWebsite.DataAccess.Services.Common;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
var OracleConnectionString = builder.Configuration.GetConnectionString("OracleConnection");

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    //options.UseOracle(OracleConnectionString));
    options.UseSqlServer(connectionString));

builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.AddScoped(typeof(IAsyncService<>), typeof(BaseService<>));

builder.Services.AddScoped<IAchievementService, AchievementService>();
builder.Services.AddScoped<IColorSetService, ColorSetService>();
builder.Services.AddScoped<ICommentService, CommentService>();
builder.Services.AddScoped<IPostableService, PostableService>();
builder.Services.AddScoped<IPostService, PostService>();
builder.Services.AddScoped<IStylePackService, StylePackService>();
builder.Services.AddScoped<IStyleService, StyleService>();
builder.Services.AddScoped<ITagService, TagService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IAchievementUserService, AchievementUserService>();
builder.Services.AddScoped<ILikeService, LikeService>();
builder.Services.AddScoped<IStyleStylePackService, StyleStylePackService>();

builder.Services.AddAuthentication(o =>
{
    o.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    o.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    o.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    o.DefaultSignOutScheme = CookieAuthenticationDefaults.AuthenticationScheme;
}).AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, o =>
{
    o.Cookie.IsEssential = true;
    o.Cookie.HttpOnly = true;
    o.Cookie.SameSite = SameSiteMode.Strict;
    o.Cookie.SecurePolicy = CookieSecurePolicy.Always;
    o.Cookie.MaxAge = TimeSpan.FromDays(30);
    o.AccessDeniedPath = "/AccessDenied";
    o.LoginPath = "/Login";
    o.ReturnUrlParameter = "return_url";
});

builder.Services.AddRazorPages();

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

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();

app.Run();
