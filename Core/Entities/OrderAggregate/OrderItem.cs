using Core.Entities.Base;

namespace Core.Entities.OrderAggregate;

public class OrderItem:BaseEntity
{
    public OrderItem()
    {
    }

    public OrderItem(ProductItemOrdered ordered, int quantity, decimal price)
    {
        Ordered = ordered;
        Quantity = quantity;
        Price = price;
    }

    public ProductItemOrdered Ordered { get; set; }
    public int Quantity { get; set; }
    public decimal Price { get; set; }
}
