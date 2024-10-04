using Microsoft.AspNetCore.Authorization;
using SecureApp.Authorization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

builder.Services.AddAuthentication("CookieAuth").AddCookie("CookieAuth", options =>
{
    options.Cookie.Name = "CookieAuth";
    options.ExpireTimeSpan = TimeSpan.FromMinutes(60);
});

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("HrOnly", policy => policy
        .RequireClaim("Department", "HR")
        .Requirements.Add(new HrManagerProbationRequirement(3))
    );
	options.AddPolicy("AdminOnly", policy => policy.RequireClaim("Admin"));
});

builder.Services.AddSingleton<IAuthorizationHandler, HrManagerProbationRequirementHandler>();
builder.Services.AddHttpClient("SecureApi", client =>
{
    client.BaseAddress = new Uri("http://localhost:5032/");
});

builder.Services.AddSession(options =>
{
    options.Cookie.HttpOnly = true;
    options.IdleTimeout = TimeSpan.FromMinutes(20);
    options.Cookie.IsEssential = true;
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

// app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.UseSession();

app.MapRazorPages();

app.Run();
