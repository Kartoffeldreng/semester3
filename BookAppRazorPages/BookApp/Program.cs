using BookApp.Repositories;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();



builder.Services.AddSingleton<IBookRepository, BookRepository>();


var app = builder.Build();

app.UseRouting();

app.UseStaticFiles();
app.MapStaticAssets();


app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
  .WithStaticAssets();


app.MapRazorPages();
app.Run();
