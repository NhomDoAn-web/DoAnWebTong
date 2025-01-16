using DoAnWEBDEMO.ApplicationDB;
using DoAnWEBDEMO.SeedData;
using DoAnWEBDEMO.Services;
using Microsoft.AspNetCore.Http.Features; // For file upload size configuration
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddScoped<EmailService>();

// Đăng ký dịch vụ Session
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

// Configure file upload size
builder.Services.Configure<FormOptions>(options =>
{
    options.MultipartBodyLengthLimit = 100_000_000; // Giới hạn kích thước tệp upload (100MB)
});

// Add controllers with views
builder.Services.AddControllersWithViews();
builder.Services.AddSession();

// Configure DbContext
builder.Services.AddDbContext<ApplicationDb>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
);

var app = builder.Build();

// Enable Session middleware
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

// Configure area routing
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
      name: "areas",
      pattern: "{area:exists}/{controller=Admin}/{action=Index}/{id?}"
    );
});

// Default route
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=TrangChu}/{action=Index}/{id?}"
);

// Seed the database with initial data
var context = app.Services.CreateScope().ServiceProvider.GetRequiredService<ApplicationDb>();
SeedData.SeedingData(context);

app.Run();
