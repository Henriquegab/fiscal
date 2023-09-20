using Microsoft.EntityFrameworkCore;
using MySql.Data.MySqlClient;
using Pomelo.EntityFrameworkCore.MySql;
using Fiscalio.Persistence;
var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllersWithViews();


builder.Services.AddDbContext<AppDbContext>(options =>
{
    var connectionString = "server=localhost;user=root;password=;database=fiscal";
    var serverVersion = new MySqlServerVersion(new Version(10, 4, 27));
    options.UseMySql(connectionString, serverVersion);
});


var dbContext = AppDbContext.GetInstance(); 



var app = builder.Build();




if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();


app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");
    endpoints.MapControllerRoute(
        name: "notaFiscais",
        pattern: "NotaFiscais/{action=Index}/{id?}",
        defaults: new { controller = "NotaFiscalController" });
});


app.Run();
