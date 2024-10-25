using Domain.Contracts;
using Domain.Entities.OrderEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Services.Specifications
{
    public class OrderWithIncludeSpecifications : Specifications<Order>
    {
        public OrderWithIncludeSpecifications(Guid id) 
            : base(order=> order.Id == id)
        {
            AddInclude(order => order.deliveryMethod);
            AddInclude(order => order.OrderItems);
        }
        public OrderWithIncludeSpecifications(string email)
            : base(order => order.UserEmail == email)
        {
            AddInclude(order => order.deliveryMethod);
            AddInclude(order => order.OrderItems);

            SetOrderBy(order => order.date);
        }
    }
}
