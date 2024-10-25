using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Exceptions
{
    public class UserNotFoundExeption(string email)
        :NotFoundException($"no user with email {email} was found")
    {
    }
}
