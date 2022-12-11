using Core.Entities.OrderAggregate;

namespace Api.Dto;

public class OrderToReturnDto
{
    public string BuyerEmail { get; set; }
    public DateTimeOffset OrderDate { get; set; }
    public Address Address { get; set; }
    public string Delivery { get; set; }
    public decimal ShippingPrice { get; set; }
    public IReadOnlyList<OrderItemDto> Items { get; set; }
    public decimal SubTotal { get; set; }
    public string OrderStatus { get; set; }
}