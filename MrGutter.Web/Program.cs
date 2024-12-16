using Microsoft.AspNetCore.Authentication.Cookies;
using MrGutter.Repository;
using MrGutter.Repository.Helper;
using MrGutter.Repository.IRepository;
using MrGutter.Services;
using MrGutter.Services.IService;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddScoped<IAccountService, AccountService>();
builder.Services.AddScoped<IAccountRepository, AccountRepository>();
builder.Services.AddScoped<IUserManagerService, UserManagerService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddSession(); // Add session service


builder.Services.AddScoped<IUserRepository, UserRepository>();
// Register IHttpContextAccessor
builder.Services.AddHttpContextAccessor();
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = new PathString("/Home/Index");
        options.ExpireTimeSpan = TimeSpan.FromMinutes(10); // Set to 60 minutes for a more typical expiration time
        options.AccessDeniedPath = "/Home/Forbidden";
        options.SlidingExpiration = true;
    });

builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
// Register Custom HttpClient with CustomHttpClientHandler
builder.Services.AddTransient<CustomHttpClientHandler>();

builder.Services.AddHttpClient<ApiClient>()
        .AddHttpMessageHandler<CustomHttpClientHandler>();

// Add other services

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
app.UseSession(); // Ensure UseSession is added before UseAuthentication and UseAuthorization
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();
app.MapControllerRoute(
    name: "area",
    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
//app.MapControllerRoute(
//    name: "default",
//    pattern: "{controller=Home}/{action=Index}/{id?}");


app.Run();
