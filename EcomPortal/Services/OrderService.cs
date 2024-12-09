using EcomPortal.Models.Entities;
using EcomPortal.Models.Dtos.Order;
using EcomPortal.Repositories;

namespace EcomPortal.Services
{
    public class OrderService(
        ICrudRepository<Order> orderRepository,
        ICrudRepository<Product> productRepository,
        ICrudRepository<User> userRepository,
        ICrudRepository<OrderProduct> orderProductRepository)
    {
        private readonly ICrudRepository<Order> _orderRepository = orderRepository;
        private readonly ICrudRepository<Product> _productRepository = productRepository;
        private readonly ICrudRepository<User> _userRepository = userRepository;
        private readonly ICrudRepository<OrderProduct> _orderProductRepository = orderProductRepository;

        public async Task<IEnumerable<Order>> GetAllAsync()
        {
            return await _orderRepository.GetAllAsync();
        }

        public async Task<Order?> GetByIdAsync(Guid id)
        {
            return await _orderRepository.GetByIdAsync(id);
        }

        public async Task<Order> CreateAsync(AddOrderDto dto)
        {
            ArgumentNullException.ThrowIfNull(dto);

            var user = await _userRepository.GetByIdAsync(dto.UserId) ??
                throw new ArgumentException($"User with ID {dto.UserId} not found.");
            var order = new Order
            {
                User = user,
                OrderProducts = []
            };

            foreach (var productDto in dto.OrderProducts)
            {
                var product = await _productRepository.GetByIdAsync(productDto.ProductId) ?? 
                    throw new ArgumentException($"Product with ID {productDto.ProductId} not found.");
                var orderProduct = new OrderProduct
                {
                    Order = order,
                    Product = product,
                    Quantity = productDto.Quantity
                };
                order.OrderProducts.Add(orderProduct);
            }

            return await _orderRepository.AddAsync(order);
        }

        public async Task<Order> UpdateAsync(Guid id, UpdateOrderDto dto)
        {
            ArgumentNullException.ThrowIfNull(dto);

            var order = await _orderRepository.GetByIdAsync(id) ??
                throw new KeyNotFoundException($"Order with ID {id} not found.");
            order.OrderProducts.Clear();
            foreach (var productDto in dto.OrderProducts)
            {
                var product = await _productRepository.GetByIdAsync(productDto.ProductId) ??
                    throw new ArgumentException($"Product with ID {productDto.ProductId} not found.");
                var orderProduct = new OrderProduct
                {
                    Order = order,
                    Product = product,
                    Quantity = productDto.Quantity
                };
                order.OrderProducts.Add(orderProduct);
            }

            return await _orderRepository.UpdateAsync(order);
        }

        public async Task DeleteAsync(Guid id)
        {
            await _orderRepository.DeleteAsync(id);
        }
    }
}
