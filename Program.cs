using CakeVilla.web.Data;
using CakeVilla.web.Models;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DbConnection")));

builder.Services.AddDistributedMemoryCache();       //Required for session state
builder.Services.AddSession(options => {
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});   //Session Timeout
builder.Services.AddScoped<CheckoutReceiptService>();

builder.Services.AddScoped<OrderReceiptService>();
builder.Services.AddScoped<ProductReportService>();
builder.Services.AddScoped<OrdersReportService>();
builder.Services.AddScoped<CustomersReportService>();
builder.Services.AddScoped<PasswordHasher<Register>, PasswordHasher<Register>>();








builder.Services.AddHttpContextAccessor();
var app = builder.Build();
app.UseSession();



//builder.Services.AddHttpContextAccessor();
//builder.Services.AddScoped<OrderReceiptService>();
//builder.Services.AddScoped<ProductReportService>();
//builder.Services.AddScoped<CustomerReportSerivce>();



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

app.Run();
