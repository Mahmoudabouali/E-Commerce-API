using Shared;
using Shared.OrderModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Abstraction
{
    public interface IAuthenticationService
    {
        // login & register

        // (loginModel)=> UserResult
         // (Register Model) => UserResult
        public Task<UserResultDTO> LoginAsync(LoginDTO login);
        public Task<UserResultDTO> RegisterAsync(UserRegisterDTO register);

        //Get Current User
        public Task<UserResultDTO> GetUserByEmail(string email);
        //Check Current User
        public Task<bool> CheckEmailExist(string email);
        //Get User Addrress
        public Task<AddressDTO> GetUserByAddress(string email);
        //Update User Address
        public Task<AddressDTO> UpdateAddress(AddressDTO address, string email);
    }
}
