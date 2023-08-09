using Microsoft.EntityFrameworkCore;
using RestaurantManagementSystem.DAO;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddControllersWithViews();
var config = builder.Configuration;//create the config object 
builder.Services.AddDbContext<RMSDBContext>(o =>
    o.UseSqlServer(config.GetConnectionString("RMSConnnectionString")));//getting the connection string from appSetting.json

var app = builder.Build();
if (!app.Environment.IsDevelopment()) {
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();
app.MapControllerRoute(name: "default", pattern: "{controller=Home}/{action=Index}/{id?}");
app.Run();
