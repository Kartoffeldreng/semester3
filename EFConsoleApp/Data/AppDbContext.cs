using EFConsoleApp.Models;
using Microsoft.EntityFrameworkCore;

namespace EFConsoleApp.Data; 

public class AppDbContext : DbContext {

  public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) {
  }

  public DbSet<Student> Students => Set<Student>();
  public DbSet<Course> Courses => Set<Course>();
  public DbSet<Instructor> Instructors => Set<Instructor>();
}
