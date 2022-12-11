using Core.Entities;
using Core.Entities.OrderAggregate;
using Core.Interfaces;

namespace Infrastructure.Servces;

public class OrderService : IOrderService
{
    private readonly IBasketRepository _basketRepository;
    private readonly IUnitOfWork _unitOfWork;

    public OrderService(IBasketRepository basketRepository, IUnitOfWork unitOfWork)
    {
        _basketRepository = basketRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Order> CreateOrderAsync(string buyerEmail, int deliveryMethodId,
        string basketId, Address shippingAddress)
    {
        //get basket
        var basket = await this._basketRepository.GetBasketAsync(basketId);

        //get order items
        var items = new List<OrderItem>();
        foreach (var item in basket.Items)
        {
            var productItem = await this._unitOfWork.Repository<Product>().GetByIdAsync(item.Id);
            var itemOrdered = new ProductItemOrdered(productItem.Id, productItem.Name, productItem.PictureUrl);
            var orderedItem = new OrderItem(itemOrdered, item.Quantity, productItem.Price);
            items.Add(orderedItem);
        }

        //get delivery method
        var deliveryMethod = await this._unitOfWork.Repository<DeliveryMethod>()
            .GetByIdAsync(deliveryMethodId);

        //get total
        var subTotal = items.Sum(a => a.Price * a.Quantity);
        var order = new Order(buyerEmail, shippingAddress, deliveryMethod, items, subTotal);
        _unitOfWork.Repository<Order>().Add(order);

        //save order
        int result = await _unitOfWork.Complete();
        if (result == 0) return null;
        //delete basket
        await this._basketRepository.DeleteBasketAsync(basketId);
        return order;
    }
    public Task<IReadOnlyList<Order>> GetOrderForUserAsync(string buyerEmail)
    {
        throw new NotImplementedException();
    }

    public Task<Order> GetOrderByIdAsync(int id, string buyerEmail)
    {
        throw new NotImplementedException();
    }

    public Task<IReadOnlyList<DeliveryMethod>> GetDeliveryMethodAsync()
    {
        throw new NotImplementedException();
    }
}