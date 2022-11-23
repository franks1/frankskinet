using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

public class BasketController : BaseApiController
{
    private readonly IBasketRepository _repository;

    public BasketController(IBasketRepository repository)
    {
        _repository = repository;
    }

    [HttpGet()]
    public async Task<IActionResult> GetBaskketById([FromQuery] string id)
    {
        var basket = await this._repository.GetBasketAsync(id);
        return Ok(basket ?? new CustomerBasket(id));
    }

    [HttpPost()]
    public async Task<IActionResult> UpdateBasket([FromBody] CustomerBasket basket)
    {
        var result = await this. _repository.UpdateBasketAsync(basket);
        return Ok(result);
    }

    [HttpDelete]
    public async Task DeleteBasket([FromQuery] string id)
    {
        await this._repository.DeleteBasketAsync(id);
    }
}