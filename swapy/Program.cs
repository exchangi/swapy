using Microsoft.EntityFrameworkCore; //additional library
using swapy.Models;
var builder = WebApplication.CreateBuilder(args);
// Creates builder (also part of boilerplate code for web apps)
//  Creates the db connection string
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddHttpContextAccessor(); //session1
builder.Services.AddSession();  //session2
builder.Services.AddDbContext<MyContext>(options =>
{
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseSession();  //session3
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
