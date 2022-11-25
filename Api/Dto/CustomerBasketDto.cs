namespace Api.Dto;

public class CustomerBasketDto
{
    public string Id { get; set; }
    public List<BasketItemDto> Items { get; set; } = new();
}