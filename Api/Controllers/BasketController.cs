using Api.Dto;
using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

public class BasketController : BaseApiController
{
    private readonly IBasketRepository _repository;
    private readonly IMapper _mapper;


    public BasketController(IBasketRepository repository,IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    [HttpGet()]
    public async Task<IActionResult> GetBaskketById([FromQuery] string id)
    {
        var basket = await this._repository.GetBasketAsync(id);
        return Ok(basket ?? new CustomerBasket(id));
    }

    [HttpPost()]
    public async Task<IActionResult> UpdateBasket([FromBody] CustomerBasketDto basket)
    {
        var basketentity = this._mapper.Map<CustomerBasket>(basket);
        var result = await this. _repository.UpdateBasketAsync(basketentity);
        return Ok(result);
    }

    [HttpDelete]
    public async Task DeleteBasket([FromQuery] string id)
    {
        await this._repository.DeleteBasketAsync(id);
    }
}