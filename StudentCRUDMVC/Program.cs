using Microsoft.EntityFrameworkCore;
using StudentCRUDMVC.DAO;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();
var config = builder.Configuration;//create the config object 
builder.Services.AddDbContext<StudentDbContext>(o=>
    o.UseSqlServer(config.GetConnectionString("studentdbcon")));//getting the connection string from appSetting.json
var app = builder.Build();
app.MapControllerRoute(name: "default",pattern:"{controller=home}/{action=index}/{id?}");
app.Run();
