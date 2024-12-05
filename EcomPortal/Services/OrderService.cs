using EcomPortal.Models.Entities;
using EcomPortal.Models.OrderRequest;
using EcomPortal.Repositories;

namespace EcomPortal.Services
{
    public class OrderService(IGenericRepository<Order> OrderRepository) :
        GenericService<Order, AddOrderDto, UpdateOrderDto>(OrderRepository),
        IGenericService<Order, AddOrderDto, UpdateOrderDto>
    {
        private readonly IGenericRepository<Order> _OrderRepository = OrderRepository;

        public override Order MapToEntity(AddOrderDto dto)
        {
            ArgumentNullException.ThrowIfNull(dto);
            var Order = new Order()
            {
                ItemName = dto.ItemName,
                ItemDescription = dto.ItemDescription,
                BuyerName = dto.BuyerName,
                TotalBill = dto.TotalBill
            };

            return Order;
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
