namespace LibraryWebAPI.Services.OrderService
{
    public interface IOrderService
    {
        Task<OrderDTO> AddOrderAsync(OrderDTO order);
    }
}
