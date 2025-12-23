var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

//Ye ASP.NET Core app me session service ko enable karta hai
//Matlab ab hum HttpContext.Session.SetString() ya SetInt32() use karke user ke data ko temporarily store kar sakte hain
builder.Services.AddSession();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();
//Ye line app ko bolti hai: ‘Har request me session ka data handle karo’
app.UseSession();
app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
