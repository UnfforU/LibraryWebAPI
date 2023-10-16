using AutoMapper;
using LibraryWebAPI.Models.DB;

namespace LibraryWebAPI.Services.OrderService
{
    public class OrderService : IOrderService
    {

        private readonly WebLibraryDbContext _context;
        private readonly IMapper _mapper;
        public OrderService(WebLibraryDbContext context, IMapper mapper)
        {
            this._context = context;
            this._mapper = mapper;
        }

        public async Task<OrderDTO> AddOrderAsync(OrderDTO order)
        {

            var newOrder = _mapper.Map<Order>(order);

            _context.Orders.Add(newOrder);
            await _context.SaveChangesAsync();

            return _mapper.Map<OrderDTO>(newOrder);
        }
    }
}
