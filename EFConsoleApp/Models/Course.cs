namespace EFConsoleApp.Models;

public class Course {
    public int Id { get; set; }
    public string Subject { get; set; }



    // Foreign Key til Customer
    public int InstructorId { get; set; }

    // Reference Navigation Property
    public Instructor? Instructor { get; set; }

    // Collection Navigation Property
    public ICollection<Student> Students { get; set; } = [];
}