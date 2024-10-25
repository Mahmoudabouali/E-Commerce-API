using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.OrderEntities
{
    // order module 
    public class Order : BaseEntity<Guid>
    {
        public Order() { }
        public Order(string userEmail,
            Address shippingAddress,
            ICollection<OrderItem> orderItems, 
            DeliveryMethod deliveryMethod, 
            decimal subtotal)
        {
            UserEmail = userEmail;
            ShippingAddress = shippingAddress;
            OrderItems = orderItems;
            this.deliveryMethod = deliveryMethod;
            Subtotal = subtotal;
        }

        // user email
        public string UserEmail { get; set; }
        //address
        public Address ShippingAddress { get; set; }
        // order item
        public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>(); //=> collection navigational prop
        // payment status
        public OrderPaymentStatus paymentStatus { get; set; } = OrderPaymentStatus.Pending;
        // delivery methods
        public DeliveryMethod deliveryMethod { get; set; } // ref navigational prop
        public int? deliveryMethodId { get; set; }

        // sub total => item.Q * price
        public decimal Subtotal { get; set; }
        // order date 
        public DateTimeOffset date {  get; set; } = DateTimeOffset.Now;

        // payment
        public string PaymentIntentId { get; set; } = string.Empty;

    }
}
