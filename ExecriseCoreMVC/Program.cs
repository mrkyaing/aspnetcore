var builder = WebApplication.CreateBuilder(args);
//register the controllers and views folder(s) to know the urls 
builder.Services.AddControllersWithViews();
var app = builder.Build();
//mapping the routes to know via the browser https://www.facebook/profile/me
app.MapControllerRoute(name:"default",pattern: "{controller=Execrise}/{action=Index}/{id?}");
app.Run();
