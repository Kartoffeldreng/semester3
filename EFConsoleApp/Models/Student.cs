namespace EFConsoleApp.Models; 

public class Student {
  public int Id { get; set; }
  public string? Name { get; set; }


  // Collection Navigation Property
  public ICollection<Course> Courses { get; set; } = [];

  
}