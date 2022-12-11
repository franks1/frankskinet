using Core.Entities.OrderAggregate;

namespace Core.Specifications;

public class OrderWithOrderingSpecification : BaseSpecification<Order>
{
    //Projects orders for a user
    public OrderWithOrderingSpecification(string email) :
        base(a => a.BuyerEmail == email)
    {
        AddInclude(a => a.Items);
        AddInclude(a => a.Delivery);
        AddOrderByDescending(a => a.OrderDate);
    }

    //Projects order for a user
    public OrderWithOrderingSpecification(int id, string email) :
        base(a => a.Id == id && a.BuyerEmail == email)
    {
        AddInclude(a => a.Items);
        AddInclude(a => a.Delivery);
    }
}