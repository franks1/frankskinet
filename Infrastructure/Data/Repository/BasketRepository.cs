using System.Text.Json;
using Core.Entities;
using Core.Interfaces;
using StackExchange.Redis;

namespace Infrastructure.Data.Repository;

public class BasketRepository:IBasketRepository
{
    private readonly IDatabase _database;
    public BasketRepository(IConnectionMultiplexer redis)
    {
        _database = redis.GetDatabase();
    }
    public async Task<CustomerBasket> GetBasketAsync(string basketId)
    {
        var data = await _database.StringGetAsync(basketId);
        return data.IsNullOrEmpty ? null : JsonSerializer.
            Deserialize<CustomerBasket>(data);
    }

    public async Task<CustomerBasket> UpdateBasketAsync(CustomerBasket item)
    {
        var created = await _database.StringSetAsync(item.Id,
            JsonSerializer.Serialize(item), TimeSpan.FromDays(30));

        if (!created) return null;
        return await GetBasketAsync(item.Id);
    }

    public  async Task<bool> DeleteBasketAsync(string basketId)
    {
        return await this._database.KeyDeleteAsync(basketId);
    }
}