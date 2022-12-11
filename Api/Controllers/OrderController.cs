using System.Security.Claims;
using Api.Dto;
using Api.Errors;
using Api.Extensions;
using AutoMapper;
using Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[Authorize]
public class OrderController:BaseApiController
{
    private readonly IOrderService _service;
    private readonly IMapper _mapper;

    public OrderController(IOrderService service,IMapper mapper)
    {
        _mapper = mapper;
        _service = service;
    }

    [HttpPost()]
    public async Task<IActionResult> CreateOrder(OrderDto order)
    {
        var email = HttpContext.User?.RetrieveEmailFromClaims();
        var address = this._mapper.Map<Core.Entities.OrderAggregate.Address>
            (order.ShipToAddress);
        var orderSaved=await this._service.
            CreateOrderAsync(email, order.DeliveryMethodId, order.BasketId, address);

        if (orderSaved == null) return BadRequest(new ApiResponse(400,"Problem creating order"));

        return Ok(orderSaved);
    }
}