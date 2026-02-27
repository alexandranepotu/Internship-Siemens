using SieMarket;

//2.1
var order1 = new Order(1, "Alice", new DateTime(2026, 2, 26));
order1.AddItem(new OrderItem("USB-C Hub", 2, 29.99m));
order1.AddItem(new OrderItem("Wireless Mouse", 1, 49.95m));

var order2 = new Order(2, "Bob", new DateTime(2026, 2, 26));
order2.AddItem(new OrderItem("4K Monitor", 1, 349.00m));
order2.AddItem(new OrderItem("USB-C Hub", 1, 29.99m));
order2.AddItem(new OrderItem("Webcam HD", 1, 179.99m));

var allOrders = new List<Order> { order1, order2 };

// 2.2
Console.WriteLine($"Order 1 final price: {order1.CalculateFinalPrice():C2}");
Console.WriteLine($"Order 2 final price: {order2.CalculateFinalPrice():C2}");

// 2.3
Console.WriteLine($"\nTop spender: {Order.FindTopSpender(allOrders)}");

// 2.4
Console.WriteLine("\nPopular products:");
foreach (var entry in Order.GetPopularProducts(allOrders))
    Console.WriteLine($"  {entry.Key}: {entry.Value} units sold");
