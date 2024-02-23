using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core;
using Talabat.Core.Entities;
using Talabat.Core.Entities.Order_Aggregation;
using Talabat.Core.Repositories;
using Talabat.Core.Repository;
using Talabat.Core.Services;
using Talabat.Core.Specifications.Order_Spec;

namespace Talabat.Services
{
    public class OrderService : IOrderService
    {
        private readonly IBasketRepository _basketRepo;
        private readonly IUnitOfWork _unitOfwork;
     

        public OrderService(IBasketRepository basketRepo,IUnitOfWork unitOfwork)
        {
            _basketRepo = basketRepo;
            _unitOfwork = unitOfwork;
        }

        public async Task<Order?> CreateOrderAsync(string BuyerEmail, string basketId, int deliveryMethodId, Address shippingAddress)
        {
            var basket =await _basketRepo.GetBasketAsync(basketId);
            var orderItems=new List<OrderItem>();
            if (basket?.Items?.Count > 0)
            {
                foreach (var item in basket.Items)
                {
                    var product = await _unitOfwork.Repository<Product>().GetByIdAsync(item.Id);
                    var productItemOrdered = new ProductOrderItem(product.Id, product.Name, product.PictureUrl);
                    var orderItem = new OrderItem(productItemOrdered, product.Price, item.Quantity);
                    orderItems.Add(orderItem);
                }
            }
                    var subTotal = orderItems.Sum(item => item.Price * item.Quantity);
                    var deliverMethod = await _unitOfwork.Repository<DeliveryMethod>().GetByIdAsync(deliveryMethodId);
                    var order = new Order(BuyerEmail, shippingAddress, deliverMethod, orderItems, subTotal);
                   await _unitOfwork.Repository<Order>().Add(order);
                var result =await _unitOfwork.Complete();
            if (result <= 0) return null;
                    return order;
                
          

        }

        public async Task<IReadOnlyList<DeliveryMethod>> GetAllDeliveryMethodsAsync()
        {
            var deliveryMethods = await _unitOfwork.Repository<DeliveryMethod>().GetAllAsync();
            return deliveryMethods;
        }

        public async Task<Order> GetOrderByIdForUserAsync(int orderId, string buyerEmail)
        {
            var spec = new OrderSpecification(orderId,buyerEmail);
            var order = await _unitOfwork.Repository<Order>().GetByIdWithSpecAsync(spec);
            return order;
        }

        public async Task<IReadOnlyList<Order>> GetOrdersForUserAsync(string buyerEmail)
        {
            var spec = new OrderSpecification(buyerEmail);
            var orders = await _unitOfwork.Repository<Order>().GetAllWithSpecAsync(spec);
            return orders;
        }
    }
}
