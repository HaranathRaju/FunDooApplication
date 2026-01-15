using BusinessLogicLayer.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ModelLayer.DTO;
using ModelLayer.Entities;
using System;

namespace FunDooApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService iuserservice;
        private readonly ILogger<UserController> logger;

        public UserController(
            IUserService iuserservice,
            ILogger<UserController> logger)
        {
            this.iuserservice = iuserservice;
            this.logger = logger;
        }

        [HttpPost]
        [Route("Register")]
        public IActionResult Register([FromBody] RegisterModel model)
        {
            logger.LogInformation("Register request started for Email: {Email}", model.Email);

            var response = iuserservice.Register(model);

            logger.LogInformation("User registered successfully with Email: {Email}", model.Email);

            return Ok(response);
        }

        [HttpPost]
        [Route("Login")]
        public IActionResult Login([FromBody] LoginModel model)
        {
            logger.LogInformation("Login attempt for Email: {Email}", model.Email);

            var result = iuserservice.Login(model);

            logger.LogInformation("Login successful for Email: {Email}", model.Email);

            return Ok(result);
        }

        [HttpPost]
        [Route("ForgetPassword")]
        public IActionResult ForgetPassword([FromQuery] string email)
        {
            logger.LogInformation("ForgetPassword request received for Email: {Email}", email);

            var token = iuserservice.ForgetPassword(email);

            logger.LogInformation("Password reset token sent to Email: {Email}", email);

            return Ok("Token sent to email");
        }

        [HttpPost]
        [Route("ResetPassword")]
        [Authorize]
        public IActionResult ResetPassword(string email, string newpassword, string confirmpassword)
        {
            logger.LogInformation("ResetPassword request started for Email: {Email}", email);

            iuserservice.ResetPassword(email, newpassword, confirmpassword);

            logger.LogInformation("Password reset successful for Email: {Email}", email);

            return Ok("Password changed successfully");
        }
    }
}
