using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace EFConsoleApp.Data; 

// Denne klasse kaldes ikke i koden, men bruges af EF Core CLI-værktøjerne
// (f.eks. 'dotnet ef migrations add' og 'dotnet ef database update') til
// oprettelse af AppDbContext ved design-time.
public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext> {

  public AppDbContext CreateDbContext(string[] args) {
    string connectionString = DbConfig.ConnectionString;

    var options = new DbContextOptionsBuilder<AppDbContext>()
      .UseSqlServer(connectionString)
      .Options;

    return new AppDbContext(options);
  }
}
