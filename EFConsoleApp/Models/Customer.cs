namespace EFConsoleApp.Models; 

public class Customer {
  public int Id { get; set; }
  public string? Name { get; set; }

  // Collection Navigation Property
  public ICollection<Order> Orders { get; set; } = [];
}