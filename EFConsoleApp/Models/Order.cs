namespace EFConsoleApp.Models; 

public class Order {
  public int Id { get; set; }
  public DateTime OrderDate { get; set; }
  public decimal TotalAmount { get; set; }

  // Foreign Key til Customer
  public int CustomerId { get; set; }

  // Reference Navigation Property
  public Customer? Customer { get; set; }
}