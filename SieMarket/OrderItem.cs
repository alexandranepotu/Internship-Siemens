namespace SieMarket;
public class OrderItem
{ 
    public string ProductName { get; set; } 
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
    public decimal LineTotal => Quantity * UnitPrice;
    
    public OrderItem(string productName, int quantity, decimal unitPrice)
    {
        if (string.IsNullOrWhiteSpace(productName))
            throw new ArgumentException("Product name cannot be empty.", nameof(productName));
        if (quantity < 1)
            throw new ArgumentOutOfRangeException(nameof(quantity), "Quantity must be at least 1.");
        if (unitPrice < 0)
            throw new ArgumentOutOfRangeException(nameof(unitPrice), "Unit price cannot be negative.");

        ProductName = productName;
        Quantity    = quantity;
        UnitPrice   = unitPrice;
    }

    public override string ToString() =>
        $"{ProductName} x{Quantity} @ {UnitPrice:C2}  =  {LineTotal:C2}";
}