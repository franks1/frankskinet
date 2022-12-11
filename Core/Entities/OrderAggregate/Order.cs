using Core.Entities.Base;
namespace Core.Entities.OrderAggregate;

public class Order:BaseEntity
{
    public Order()
    {
        
    }
    public Order(string buyerEmail, Address address, 
        DeliveryMethod delivery, 
        IReadOnlyList<OrderItem> items, decimal subTotal)
    {
        BuyerEmail = buyerEmail;
        Address = address;
        Delivery = delivery;
        Items = items;
        SubTotal = subTotal;
    }

    public string BuyerEmail { get; set; }
    public DateTimeOffset OrderDate { get; set; } = DateTimeOffset.Now;
    public Address Address { get; set; }
    public DeliveryMethod Delivery { get; set; }
    public IReadOnlyList<OrderItem> Items { get; set; }
    public decimal SubTotal { get; set; }
    public OrderStatus OrderStatus { get; set; } = OrderStatus.Pending;
    public string PaymentIntentId { get; set; }

    public decimal GetTotal()
        => SubTotal + Delivery.Price;

}