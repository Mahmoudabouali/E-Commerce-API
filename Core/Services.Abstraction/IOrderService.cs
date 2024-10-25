using Shared.OrderModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Abstraction
{
    public interface IOrderService
    {
        // get order by id => OrderResult(Guid id)
        public Task<OrderResult> GetOrderByIdAsync(Guid id);

        // get order for user by email => IEnumrable<OrderResult>(string email)
        public Task<IEnumerable<OrderResult>> GetOrderByEmailAsync(string email);

        // create order => OrderResult(OrderRequest, string email)
        public Task<OrderResult> CreateOrderAsync(OrderRequest request, string email);

        // get all delivery methods
        public Task<IEnumerable<DeliveryMethodResult>> GetDeliveryMethodAsync();

    }
}
