using EcomPortal.Models.Entities;
using EcomPortal.Models.Dtos.Order;
using EcomPortal.Repositories;

namespace EcomPortal.Services
{
    public class OrderService(IGenericRepository<Order> orderRepository,
                              IGenericRepository<Product> productRepository,
                              IGenericRepository<User> userRepository,
                              IGenericRepository<OrderProduct> orderProductRepository) :
        GenericService<Order, AddOrderDto, UpdateOrderDto>(orderRepository),
        IGenericService<Order, AddOrderDto, UpdateOrderDto>
    {
        private readonly IGenericRepository<Order> _orderRepository = orderRepository;
        private readonly IGenericRepository<Product> _productRepository = productRepository;
        private readonly IGenericRepository<User> _userRepository = userRepository;
        private readonly IGenericRepository<OrderProduct> _orderProductRepository = orderProductRepository;

        public new async Task<Order> CreateAsync(AddOrderDto dto)
        {
            ArgumentNullException.ThrowIfNull(dto);

            var user = await _userRepository.GetByIdAsync(dto.UserId);
            if (user == null)
            {
                throw new ArgumentException($"Product with ID {dto.UserId} not found.");
            }

            var order = new Order
            {
                UserId = dto.UserId,
                ItemName = dto.ItemName,
                ItemDescription = dto.ItemDescription,
                BuyerName = dto.BuyerName,
                TotalBill = dto.TotalBill,
                User = user
            };

            foreach (var productDto in dto.OrderProducts)
            {
                var product = await _productRepository.GetByIdAsync(productDto.ProductId) 
                    ?? throw new ArgumentException($"Product with ID {productDto.ProductId} not found.");
                
                var orderProduct = new OrderProduct
                {
                    OrderId = order.Id,
                    Order = order,
                    Product = product,
                    ProductId = product.Id,
                    Quantity = productDto.Quantity
                };
                order.OrderProducts.Add(orderProduct);
            }
            
            order = await _orderRepository.AddAsync(order);

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
