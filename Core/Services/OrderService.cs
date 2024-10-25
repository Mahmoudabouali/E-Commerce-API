using AutoMapper;
using Domain.Contracts;
using Domain.Entities;
using Domain.Entities.OrderEntities;
using Domain.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Services.Abstraction;
using Services.Specifications;
using Shared.OrderModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    internal class OrderService(IUnitOfWork unitOfWork,
        IMapper mapper,
        IBasketReposiory basketReposiory) 
        : IOrderService
    {
        public async Task<OrderResult> CreateOrderAsync(OrderRequest request, string useremail)
        {
            //1. Address 
            var address = mapper.Map<Address>(request.ShippingAddress);
            //2. order Items => basket => basket Items => order Items

            var basket = await basketReposiory.GetBasketAsync(request.BasketId)
                ?? throw new BasketNotFoundExeption(request.BasketId);  

            var orderItems = new List<OrderItem>();
            foreach (var item in basket.Items)
            {
                var product = await unitOfWork.GetRepository<Product,int>()
                    .GetAsync(item.Id) ?? throw new ProductNotFoundException(item.Id);

                orderItems.Add(CreateOrderItem(item, product));
            }

            //3. Delivery
            var deliveryMethod = await unitOfWork.GetRepository<DeliveryMethod, int>()
                .GetAsync(request.DeliveryMethodId) ?? throw new DeliveryMethodNotFoundExeption(request.DeliveryMethodId);
            //4. SubTotal
            var subTotal = orderItems.Sum(item=> item.Price * item.Quantity);

            //save to db
            var order = new Order(useremail, address, orderItems, deliveryMethod, subTotal);
            await unitOfWork.GetRepository<Order,Guid>()
                .AddAsync(order);
            await unitOfWork.SaveChangesAsync();
           //map & return
            return mapper.Map<OrderResult>(order);
        }

        private OrderItem CreateOrderItem(BasketItem item, Product product)
            => new OrderItem(new ProductInOrderItem(product.Id,product.Name,product.PictureUrl),
                item.Quantity,product.Price);

        public async Task<IEnumerable<DeliveryMethodResult>> GetDeliveryMethodAsync()
        {
            var methods = await unitOfWork.GetRepository<DeliveryMethod,int>()
                .GetAllAsync();
            return mapper.Map<IEnumerable<DeliveryMethodResult>>(methods);
        }

        public async Task<IEnumerable<OrderResult>> GetOrderByEmailAsync(string email)
        {
            var orderResult = await unitOfWork.GetRepository<Order, Guid>()
               .GetAllAsync(new OrderWithIncludeSpecifications(email));

            return mapper.Map<IEnumerable<OrderResult>>(orderResult);
        }

        public async Task<OrderResult> GetOrderByIdAsync(Guid id)
        {
            var orderResult = await unitOfWork.GetRepository<Order, Guid>()
                .GetAsync(new OrderWithIncludeSpecifications(id))
                ?? throw new OrderNotFoundExeption(id);

            return mapper.Map<OrderResult>(orderResult);
        }
    }
}
