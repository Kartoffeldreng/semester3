using EFConsoleApp.Models;
using Microsoft.EntityFrameworkCore;

namespace EFConsoleApp.Data; 

public class AppDbContext : DbContext {

  public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) {
  }

  public DbSet<Customer> Customers => Set<Customer>();
  public DbSet<Order> Orders => Set<Order>();
}
