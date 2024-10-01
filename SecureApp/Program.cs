var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

builder.Services.AddAuthentication("CookieAuth").AddCookie("CookieAuth", options =>
{
    options.Cookie.Name = "CookieAuth";
});

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("HrOnly", policy => policy.RequireClaim("Department", "HR"));
	options.AddPolicy("AdminOnly", policy => policy.RequireClaim("Admin"));
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

app.MapRazorPages();

app.Run();
