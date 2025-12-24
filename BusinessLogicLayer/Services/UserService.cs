using DataLogicLayer.Interfaces;
using ModelLayer.Entities;
using BusinessLogicLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BCrypt.Net;
using ModelLayer.DTO;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;


namespace BusinessLogicLayer.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository iuserrepository;
        private readonly IConfiguration configuration;
        public UserService(IUserRepository iuserrepository,IConfiguration configuration)
        {
            this.iuserrepository = iuserrepository;
            this.configuration = configuration;
        }

        private string GenerateToken(User user)
        {
            var claims = new List<Claim>
            {
                  new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
                  new Claim(ClaimTypes.Email, user.Email),
                  new Claim(ClaimTypes.Name, user.FirstName)
            };

            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(configuration["Jwt:Key"])
            );
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: configuration["Jwt:Issuer"],
                audience: configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(
                    Convert.ToDouble(configuration["Jwt:ExpiryMinutes"])
                ),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }


        public UserResponse Register(RegisterModel model)
        {
         
            var user = new User();
            user.FirstName = model.FirstName;
            user.LastName = model.LastName;
            user.Email = model.Email;
            user.Password = BCrypt.Net.BCrypt.HashPassword(model.Password);
            user.CreatedAt = DateTime.UtcNow;
            user.ChangedAt = DateTime.UtcNow;

            iuserrepository.AddUser(user);

            UserResponse response = new UserResponse();

            response.FirstName = user.FirstName;
            response.LastName = user.LastName;
            response.Email = user.Email;

            return  response;

        }

        public UserResponse Login(LoginModel model)
        {

            var user = iuserrepository.GetUserByEmail(model.Email);
            if (user == null)
            {
                throw new Exception("email or password incorrect");
            }
            bool isvalid = BCrypt.Net.BCrypt.Verify(model.Password, user.Password);
            if (!isvalid)
            {
                throw new Exception("password is incorrect");
            }
       
            var token = GenerateToken(user);

            UserResponse response = new UserResponse();
            response.FirstName = user.FirstName;
            response.LastName = user.LastName;  
            response.Email= user.Email;
            response.Token = token;

            return response;

        }
    }
}
