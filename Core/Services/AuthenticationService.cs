using Domain.Entities;
using Domain.Exceptions;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Services.Abstraction;
using Shared;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class AuthenticationService(UserManager<User> UserManager, IOptions<JwtOptions> options) : IAuthenticationService
    {
        public async Task<UserResultDTO> LoginAsync(LoginDTO login)
        {
            // check if there is user under this email
            var user = await UserManager.FindByEmailAsync(login.Email);
            if (user == null) throw new UnAuthorizedException("email dosn't exist");

            // check if the password is correct for this email
            var result = await UserManager.CheckPasswordAsync(user,login.Password);
            if(!result) throw new UnAuthorizedException();
            //create token and return response
            return new UserResultDTO(
                user.DisplayName,
                user.Email!,
                await CreateTokenAsync(user));
            
        }

        public async Task<UserResultDTO> RegisterAsync(UserRegisterDTO register)
        {
            // check email uniquness
            // check username

            var user = new User()
            {
                Email = register.Email,
                DisplayName = register.DisplayName,
                PhoneNumber = register.PhoneNumber,
                UserName = register.UserName,
            };
            var result = await UserManager.CreateAsync(user,register.Password);

            if(!result.Succeeded)
            {
                var errors = result.Errors.Select(e => e.Description).ToList();
                throw new ValidationException(errors);
            }
            return new UserResultDTO(
                user.DisplayName,
                user.Email!,
                await CreateTokenAsync(user));
        }
        private async Task<string> CreateTokenAsync(User user)
        {
            var jwpOptions = options.Value;
            // private claims 
            var authClaims = new List<Claim>
            {
                new Claim(ClaimTypes.Name,user.UserName!),
                new Claim(ClaimTypes.Email,user.Email!)
            };

            // add roles to claims if exist
            var roles = await UserManager.GetRolesAsync(user);

            foreach (var role in roles)
                authClaims.Add(new Claim(ClaimTypes.Role, role));

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwpOptions.SecretKey));

            var signingCreds = new SigningCredentials(key,SecurityAlgorithms.HmacSha256);


            var token = new JwtSecurityToken(
                audience: jwpOptions.Audience,
                issuer: jwpOptions.Issure,
                expires: DateTime.UtcNow.AddDays(jwpOptions.DurationInDays),
                claims:authClaims,
                signingCredentials:signingCreds);
                
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
