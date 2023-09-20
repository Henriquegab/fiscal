using Microsoft.EntityFrameworkCore;
using MySql.Data.MySqlClient;
using Pomelo.EntityFrameworkCore.MySql;
using Fiscalio.Persistence;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<AppDbContext>(options =>
{
    var connectionString = "server=localhost;user=root;password=;database=fiscal";
    var serverVersion = new MySqlServerVersion(new Version(10, 4, 27));
    options.UseMySql(connectionString, serverVersion);
});

builder.Services.AddDbContext<AppDbContext>(options =>
{
    var connectionString = "server=localhost;user=root;password=;database=fiscal";
    var serverVersion = new MySqlServerVersion(new Version(10, 4, 27));
    options.UseMySql(connectionString, serverVersion);
});

// Registrar o Singleton manualmente
var dbContext = AppDbContext.GetInstance(); // Obtém a instância Singleton

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
