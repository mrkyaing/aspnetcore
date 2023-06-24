using Microsoft.Extensions.FileSystemGlobbing.Internal.Patterns;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();
var app = builder.Build();


app.MapControllerRoute(
    name: "default",
    pattern: "{controller=exercise}/{action=index}/{id?}"
    );

app.Run();
