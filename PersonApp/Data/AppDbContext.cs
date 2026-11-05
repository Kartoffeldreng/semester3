using Microsoft.EntityFrameworkCore;
using PersonApp.Models;

namespace PersonApp.Data;

public class AppDbContext : DbContext {
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) {
    }

    public DbSet<Person> Persons => Set<Person>();
}
