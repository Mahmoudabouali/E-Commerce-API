using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Shared.OrderModels
{
    public record OrderResult
    {
        public Guid Id { get; set; }
        public string UserEmail { get; set; }
        //address
        public AddressDTO ShippingAddress { get; set; }
        // order item
        public ICollection<OrderItemDTO> OrderItems { get; set; } = new List<OrderItemDTO>(); //=> collection navigational prop
        // payment status
        public string paymentStatus { get; set; } 
        // delivery methods
        public string deliveryMethod { get; set; } // ref navigational prop


        // sub total => item.Q * price
        public decimal Subtotal { get; set; }
        // order date 
        public DateTimeOffset date { get; set; } = DateTimeOffset.Now;

        // payment
        public string PaymentIntentId { get; set; } = string.Empty;

        public decimal Total { get; set; }
    }
}
