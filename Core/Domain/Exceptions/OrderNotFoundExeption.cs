using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Exceptions
{
    public class OrderNotFoundExeption(Guid id)
        : NotFoundException($"no order with id {id} was found")
    {
    }
}
