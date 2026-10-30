using EFConsoleApp.Data;
using EFConsoleApp.Models;
using Microsoft.EntityFrameworkCore;
Console.WriteLine("Programmet starter");

/*
 * opsætning af DbContext
 */
var connectionString = DbConfig.ConnectionString;

var options = new DbContextOptionsBuilder<AppDbContext>()
    .UseSqlServer(connectionString)
    .Options;

using AppDbContext dbcontext = new(options);

/*
 * sletter tabellernes indhold, så altid starter
 * forfra i vores testanvendelse.
 */
dbcontext.Orders.RemoveRange(dbcontext.Orders);
dbcontext.Customers.RemoveRange(dbcontext.Customers);
dbcontext.SaveChanges();

/*
 * testanvendelse
 */
var alice = new Customer { Name = "Alice" };
alice.Orders.Add(new Order { OrderDate = DateTime.Now, TotalAmount = 199.95m });
alice.Orders.Add(new Order { OrderDate = DateTime.Now, TotalAmount = 9.95m });

var bob = new Customer { Name = "Bob" };
bob.Orders.Add(new Order { OrderDate = DateTime.Now, TotalAmount = 100.95m });

/*
 * brug af DbContext
 */
dbcontext.Customers.Add(alice);
dbcontext.Customers.Add(bob);
dbcontext.SaveChanges();

// eager loading
var customers = dbcontext.Customers
  .Include(c => c.Orders);

foreach (Customer c in customers)
    Console.WriteLine($"{c.Name} - #orders: {c.Orders.Count}");

Console.WriteLine("Programmet er færdigt");
Console.ReadLine();
