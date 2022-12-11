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
public class OrderController : BaseApiController
{
    private readonly IOrderService _service;
    private readonly IMapper _mapper;

    public OrderController(IOrderService service, IMapper mapper)
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
        var orderSaved = await this._service.CreateOrderAsync(email, order.DeliveryMethodId, order.BasketId, address);
        if (orderSaved == null) return BadRequest(new ApiResponse(400, "Problem creating order"));
        return Ok(orderSaved);
    }

    [HttpGet]
    public async Task<IActionResult> GetOrdersForUser()
    {
        var email = HttpContext.User?.RetrieveEmailFromClaims();
        var records = await this._service.GetOrderForUserAsync(email);
        var orders = this._mapper.
            Map<IReadOnlyList<OrderToReturnDto>>(records);
        return Ok(orders);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetOrdersForUser(int id)
    {
        var email = HttpContext.User?.RetrieveEmailFromClaims();
        var records = await this._service.GetOrderByIdAsync(id, email);
        if (records == null) return NotFound(new ApiResponse(404));
        var order = _mapper.Map<OrderToReturnDto>(records);
        return Ok(order);
    }

    [HttpGet("deliveryMethods")]
    public async Task<IActionResult> DeliveryMethods()
    {
        var records = await this._service.GetDeliveryMethodAsync();
        return Ok(records);
    }
}