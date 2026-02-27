namespace SieMarket;
public class Order
{
    private const decimal DiscountThreshold = 500m;   
    private const decimal DiscountRate = 0.10m;
    public int OrderId { get; set; }
    public string CustomerName { get; set; }
    public DateTime OrderDate { get; set; }
    public List<OrderItem> Items { get; set; } = new();
    public decimal GrossTotal => Items.Sum(i => i.LineTotal);
    public bool IsDiscountApplied => GrossTotal > DiscountThreshold;
    public decimal DiscountAmount => IsDiscountApplied ? GrossTotal * DiscountRate : 0m;
    public decimal NetTotal => GrossTotal - DiscountAmount;
    
    public Order(int orderId, string customerName, DateTime orderDate)
    {
        if (string.IsNullOrWhiteSpace(customerName))
            throw new ArgumentException("Customer name cannot be empty.", nameof(customerName));

        OrderId      = orderId;
        CustomerName = customerName;
        OrderDate    = orderDate;
    }
    public decimal CalculateFinalPrice()
    {
        decimal gross = Items.Sum(i => i.LineTotal);
        if (gross > DiscountThreshold)
            return gross - (gross * DiscountRate); 
        return gross;
    }
    
    public static string FindTopSpender(List<Order> orders)
    {
        return orders
            .GroupBy(o => o.CustomerName)
            .OrderByDescending(g => g.Sum(o => o.CalculateFinalPrice()))
            .First()
            .Key;
    }
    public static Dictionary<string, int> GetPopularProducts(List<Order> orders)
    {
        return orders
            .SelectMany(o => o.Items)
            .GroupBy(i => i.ProductName)
            .OrderByDescending(g => g.Sum(i => i.Quantity))
            .ToDictionary(g => g.Key, g => g.Sum(i => i.Quantity));
    }

    public void AddItem(OrderItem item)
    {
        ArgumentNullException.ThrowIfNull(item);
        Items.Add(item);
    }

    public override string ToString()
    {
        string result = $"Order #{OrderId} | {CustomerName} | {OrderDate:yyyy-MM-dd}\n";
        foreach (var item in Items)
            result += $"  {item.ProductName} x{item.Quantity} @ {item.UnitPrice:C2}\n";
        result += $"Gross: {GrossTotal:C2}";
        if (IsDiscountApplied)
            result += $" | Discount: -{DiscountAmount:C2}";
        result += $" | Total: {NetTotal:C2}";
        return result;
    }
}
