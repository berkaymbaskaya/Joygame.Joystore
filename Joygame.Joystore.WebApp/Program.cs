using Joygame.Joystore.Services.Interfaces;
using Joygame.Joystore.WebApp.Middlewares;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using Refit;

var builder = WebApplication.CreateBuilder(args);

#region Environment Configuration
builder.Configuration
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true, reloadOnChange: true)
    .AddEnvironmentVariables();
#endregion

//#region Authentication Configuration
//builder.Services.AddControllersWithViews(options =>
//{
//    var policy = new AuthorizationPolicyBuilder()
//        .RequireAuthenticatedUser()
//        .Build();

//    options.Filters.Add(new AuthorizeFilter(policy));
//});
//#endregion

#region Session Configuration
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(60);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

builder.Services.Configure<CookiePolicyOptions>(options =>
{
    options.CheckConsentNeeded = context => false;
    options.MinimumSameSitePolicy = SameSiteMode.Lax;
});
#endregion

#region Services Configuration
builder.Services.AddHttpContextAccessor();
builder.Services.AddTransient<TokenHandler>();
builder.Services.AddControllersWithViews();
#endregion
// Refit

var apiBaseUri = new Uri(builder.Configuration["ApiSettings:BaseUrl"]);

builder.Services.AddRefitClient<IProductService>()
    .ConfigureHttpClient(c => c.BaseAddress = apiBaseUri)
    .AddHttpMessageHandler<TokenHandler>();

builder.Services.AddRefitClient<IAuthService>()
    .ConfigureHttpClient(c => c.BaseAddress = apiBaseUri)
    .AddHttpMessageHandler<TokenHandler>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseCookiePolicy();

app.UseRouting();

app.UseSession();

#region Token Middleware
app.UseTokenCheck();

#endregion
app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

app.Run();
