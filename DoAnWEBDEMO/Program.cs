using DoAnWEBDEMO.ApplicationDB;
using DoAnWEBDEMO.SeedData;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);



<<<<<<< HEAD
// ??ng k� d?ch v? Session
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30); // Th?i h?n c?a session
=======
// Đăng ký dịch vụ Session
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30); // Thời gian hết hạn session
>>>>>>> thinh
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});


// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<ApplicationDb>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


var app = builder.Build();

app.UseSession();


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
    pattern: "{controller=Home}/{action=Index}/{id?}");


var context = app.Services.CreateScope().ServiceProvider.GetRequiredService<ApplicationDb>();
SeedData.SeedingData(context);


app.Run();
