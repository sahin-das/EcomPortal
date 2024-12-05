using EcomPortal.Models.Entities;
using EcomPortal.Models.Dtos.Order;
using EcomPortal.Repositories;

namespace EcomPortal.Services
{
    public class OrderService(IGenericRepository<Order> orderRepository) :
        GenericService<Order, AddOrderDto, UpdateOrderDto>(orderRepository),
        IGenericService<Order, AddOrderDto, UpdateOrderDto>
    {
        private readonly IGenericRepository<Order> _orderRepository = orderRepository;

        public override Order MapToEntity(AddOrderDto dto)
        {
            ArgumentNullException.ThrowIfNull(dto);
            var order = new Order()
            {
                UserId = dto.UserId,
                ItemName = dto.ItemName,
                ItemDescription = dto.ItemDescription,
                BuyerName = dto.BuyerName,
                TotalBill = dto.TotalBill
            };

            return order;
        }

        public override void MapToEntity(UpdateOrderDto dto, Order entity)
        {
            ArgumentNullException.ThrowIfNull(dto);
            entity.ItemName = dto.ItemName;
            entity.ItemDescription = dto.ItemDescription;
            entity.BuyerName = dto.BuyerName;
            entity.TotalBill = dto.TotalBill;
        }
    }
}
