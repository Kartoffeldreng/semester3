using Microsoft.EntityFrameworkCore;
using PersonApp.Data;
using PersonApp.Repository;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<AppDbContext>(options =>
  options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IPersonRepository, PersonRepository>();

string uploadPath = Path.Combine(
  builder.Environment.ContentRootPath,
  builder.Configuration["UploadPath"] ?? "wwwroot/Fotos");

builder.Services.AddSingleton<IFileRepository>(new FileRepository(uploadPath));

var app = builder.Build();

app.UseRouting();

app.UseStaticFiles();
app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
  .WithStaticAssets();

app.Run();
