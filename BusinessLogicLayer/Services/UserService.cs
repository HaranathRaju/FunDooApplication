using DataLogicLayer.Interfaces;
using BusinessLogicLayer.Interfaces;
using ModelLayer.Entities;
using ModelLayer.DTO;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BusinessLogicLayer.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository iuserrepository;
        private readonly IEmailService emailservice;
        private readonly IConfiguration configuration;
        private readonly ILogger<UserService> logger;
        private readonly IRabbitMQProducer rabbitMQProducer;

        public UserService(
            IUserRepository iuserrepository,
            IEmailService emailservice, 
            IConfiguration configuration,
            ILogger<UserService> logger,
            IRabbitMQProducer rabbitMQProducer
            )
        {
            this.iuserrepository = iuserrepository;
            this.emailservice = emailservice;   
            this.configuration = configuration;
            this.logger = logger;
            this.rabbitMQProducer= rabbitMQProducer;    
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
            logger.LogInformation("Register process started for Email: {Email}", model.Email);

            var existingUser = iuserrepository.GetUserByEmail(model.Email);
            if (existingUser != null)
            {
                logger.LogWarning("Registration failed. Email already exists: {Email}", model.Email);
                throw new Exception("Email already exists");
            }

            var user = new User
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
                Password = BCrypt.Net.BCrypt.HashPassword(model.Password),
                CreatedAt = DateTime.UtcNow,
                ChangedAt = DateTime.UtcNow
            };

            iuserrepository.AddUser(user);

            EmailRequest email = new EmailRequest(
                  user.Email,
                  "Welcome to Fundoo",
                  $"Hi {user.FirstName}, your registration was successful!"
            );

            rabbitMQProducer.SendEmailMessage(email);



            logger.LogInformation("User registered successfully for Email: {Email}", model.Email);

            return new UserResponse
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email
            };
        }

        public UserResponse Login(LoginModel model)
        {
            logger.LogInformation("Login attempt started for Email: {Email}", model.Email);

            var user = iuserrepository.GetUserByEmail(model.Email);
            if (user == null)
            {
                logger.LogWarning("Login failed. User not found for Email: {Email}", model.Email);
                throw new Exception("Email or password incorrect");
            }

            bool isValid = BCrypt.Net.BCrypt.Verify(model.Password, user.Password);
            if (!isValid)
            {
                logger.LogWarning("Login failed. Invalid password for Email: {Email}", model.Email);
                throw new Exception("Password is incorrect");
            }

            var token = GenerateToken(user);

            logger.LogInformation("Login successful for Email: {Email}", model.Email);

            return new UserResponse
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Token = token
            };
        }

        public string ForgetPassword(string email)
        {
            logger.LogInformation("ForgetPassword requested for Email: {Email}", email);

 
            var user = iuserrepository.GetUserByEmail(email);
            if (user == null)
            {
                logger.LogWarning("ForgetPassword failed. Email not found: {Email}", email);
                throw new Exception("Email incorrect");
            }

            var token = GenerateToken(user);

            EmailRequest emailRequest = new EmailRequest(
                user.Email,
                "Reset your password",
                $"Use this token to reset your password: {token}"
            );

            emailservice.SendEmail(emailRequest);

            logger.LogInformation("Password reset token sent to Email: {Email}", email);
            return token;
        }


        public bool ResetPassword(string email, string newpassword, string confirmpassword)
        {
            logger.LogInformation("ResetPassword process started for Email: {Email}", email);

            if (newpassword != confirmpassword)
            {
                logger.LogWarning("ResetPassword failed. Password mismatch for Email: {Email}", email);
                throw new Exception("Password should match");
            }

            string hashPassword = BCrypt.Net.BCrypt.HashPassword(newpassword);
            iuserrepository.UpdatePassword(email, hashPassword);

            logger.LogInformation("Password reset successful for Email: {Email}", email);

            return true;
        }
    }
}
