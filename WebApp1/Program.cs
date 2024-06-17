using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using Microsoft.Identity.Web;
using WebApp1.DataAccess;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<LitTextyDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("FreeAspConnectionString")));
builder.Services.AddApplicationInsightsTelemetry();

////authentication google
//builder.Services.AddAuthentication().AddGoogle(options =>
//{
//    options.ClientId = "102035095724-0apc70r5rk0kj342bjgb3sq3aa1pgdj8.apps.googleusercontent.com";
//    options.ClientSecret = "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";
//    options.CallbackPath = "/Texty/MainPage";
//});

////authentication Microsoft
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
        .AddMicrosoftIdentityWebApi(options =>
        {
            builder.Configuration.Bind("AzureAd", options);
            options.TokenValidationParameters.NameClaimType = "name";
        }, options => { builder.Configuration.Bind("AzureAd", options); });

builder.Services.AddAuthorization(config =>
{
    config.AddPolicy("AuthZPolicy", policyBuilder =>
        policyBuilder.Requirements.Add(new ScopeAuthorizationRequirement() { RequiredScopesConfigurationKey = $"AzureAd:Scopes" }));
});

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
    pattern: "{controller=Texty}/{action=MainPage}");

app.Run();
