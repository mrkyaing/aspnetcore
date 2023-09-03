using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using RestaurantManagementSystem.DAO;
using RestaurantManagementSystem.Services;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddControllersWithViews();
var config = builder.Configuration;//create the config object 
builder.Services.AddDbContext<RMSDBContext>(o =>
    o.UseSqlServer(config.GetConnectionString("RMSConnnectionString")));//getting the connection string from appSetting.json
builder.Services.AddRazorPages();//for enabling identity functions UIs
//register the Identity for Identity User and Identity Role with related dbContext
builder.Services.AddIdentity<IdentityUser, IdentityRole>().AddEntityFrameworkStores<RMSDBContext>()
    .AddDefaultUI()
    .AddDefaultTokenProviders();
var app = builder.Build();
if (!app.Environment.IsDevelopment()) {
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
//for enabling the Authentication & Authorization
app.UseAuthentication();
app.UseAuthorization();
//app.MapControllerRoute(name: "default", pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapDefaultControllerRoute();
app.MapRazorPages();//for enabling  route paths for the Authentication & Authorization (Controllers Path)
app.Run();
