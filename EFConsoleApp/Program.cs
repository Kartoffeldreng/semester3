
using EFConsoleApp.Data;
using EFConsoleApp.Models;
using Microsoft.EntityFrameworkCore;

Console.WriteLine("Programmet starter");

/*
 * Opsætning af DbContext
 */
var connectionString = DbConfig.ConnectionString;

var options = new DbContextOptionsBuilder<AppDbContext>()
    .UseSqlServer(connectionString)
    .Options;

using AppDbContext dbcontext = new(options);

/*
 * Sletter tabellernes indhold,
 * så vi altid starter forfra i testanvendelsen.
 */
dbcontext.Courses.RemoveRange(dbcontext.Courses);
dbcontext.Students.RemoveRange(dbcontext.Students);
dbcontext.Instructors.RemoveRange(dbcontext.Instructors);
dbcontext.SaveChanges();

/*
 * Testanvendelse
 */

// Opret underviser
var bastian = new Instructor { Name = "Bastian" };

// Opret kurser
var sys = new Course {
    Subject = "Sys",
    Instructor = bastian
};

var pro = new Course {
    Subject = "Pro",
    Instructor = bastian
};

Console.WriteLine(bastian.Courses.Count);

// Opret studerende
var alice = new Student { Name = "Alice" };
alice.Courses.Add(pro);
alice.Courses.Add(sys);

var bob = new Student { Name = "Bob" };
bob.Courses.Add(sys);

/*
 * Brug af DbContext
 */

// Tilføjer studerende og deres relaterede objekter
dbcontext.Students.AddRange(alice, bob);
dbcontext.SaveChanges();

/*
 * Eager loading
 */
var students = dbcontext.Students
    .Include(s => s.Courses)
    .ThenInclude(i => i.Instructor)
    .ToList();

foreach (Student student in students) {
    foreach (Course course in student.Courses) 
        Console.WriteLine(
            $"{student.Name} - #instructor: {course.Instructor?.Name}");
}

Console.WriteLine("Programmet er færdigt");
Console.ReadLine();
