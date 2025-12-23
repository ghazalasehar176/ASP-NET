var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

//Project me session ko enable karta hai.
builder.Services.AddSession(
//options => options.IdleTimeout = TimeSpan.FromMinutes(1)
//ek object hai jisme session ki settings hoti hain (timeout wagaira).
//Session kitni der baad expire hoga wo set karha hai (4 seconds).

//IdleTimeout:Session kitni der tak active rahega jab user koi activity na kare.Time pura hote hi session automatically expire ho jata hai.
  options => options.IdleTimeout = TimeSpan.FromSeconds(4));


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();
//UseSession: har HTTP request me session ka data use karne ke liye hy
app.UseSession();
app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
