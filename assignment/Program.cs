var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

//cookie authentication
builder.Services.AddAuthentication("CookieAuth").AddCookie("CookieAuth", options =>
{
    options.LoginPath = "/Account/Login";
});

//session
builder.Services.AddSession(options =>
options.IdleTimeout = TimeSpan.FromMinutes(10)
);

//authorization
builder.Services.AddAuthorization();

var app = builder.Build();


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();
app.UseSession();

app.UseAuthentication();
app.UseAuthorization();

app.UseRouting();



app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
