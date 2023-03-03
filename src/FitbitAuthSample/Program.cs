using System.Net.Mime;
using FitbitAuthSample.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder
    .Services
    .AddRazorPages()
    .Services
    .AddMvc()
    .Services
    .AddAuthentication(
        options =>
        {
            options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
        })
    .AddFitbit(
        options =>
        {
            options.ClientId = builder.Configuration["Authentication:ClientId"]!;
            options.ClientSecret = builder.Configuration["Authentication:ClientSecret"]!;
            options.Scope.Add("activity");
            options.SaveTokens = true;
        })
    .AddCookie(
        options =>
        {
            options.LoginPath = "/signin";
            options.LogoutPath = "/signout";
        })
    .Services
    .AddHttpContextAccessor()
    .AddHttpClient<FitbitClient>(
        (services, options) =>
        {
            var httpContext
                = services
                    .GetRequiredService<IHttpContextAccessor>()
                    .HttpContext!;

            var accessToken
                = httpContext
                    .Features
                    .Get<IAuthenticateResultFeature>()
                    ?.AuthenticateResult
                    ?.Properties
                    ?.GetTokenValue("access_token");

            if (accessToken == null)
            {
                return;
            }

            options.BaseAddress = new("https://api.fitbit.com");
            options.DefaultRequestHeaders.Authorization = new("Bearer", accessToken);
            options.DefaultRequestHeaders.Accept.Add(new(MediaTypeNames.Application.Json));
        });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
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
app.MapControllers();

app.Run();
