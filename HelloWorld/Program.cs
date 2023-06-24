//define a web app builder 
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();// register for all Controllers & Views 
//create the web app using the builder 
var app = builder.Build();

//routing the path for url 
//app.MapGet("/", () => "Hello World!");
app.MapGet("/SayHello", () => "Hi,Nice to See you!!");
app.MapGet("/now",()=>DateTime.Now.ToString());
app.MapGet("/me", ()=>"mr kyaing");

//mapping the routes to hit the urls
//eg www.fb.com/profile/me

app.MapControllerRoute(name:"default",pattern:"{controller=home}/{action=index}/{id?}");
//run the web app
app.Run();
