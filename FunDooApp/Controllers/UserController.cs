using BusinessLogicLayer.Interfaces;
using BusinessLogicLayer.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ModelLayer.DTO;
using ModelLayer.Entities;

namespace FunDooApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService iuserservice;
        private readonly IEmailService emailservice;
        

        public UserController(IUserService iuserservice , IEmailService emailservice)
        {
            this.iuserservice = iuserservice;
            this.emailservice = emailservice;
        }

        [HttpPost]
        [Route("Register")]
        
        public IActionResult Register([FromBody] RegisterModel model)
        {
            var response=iuserservice.Register(model);

            EmailRequest request = new EmailRequest(model.Email, "welcome to fundoo application", "successfully registered to fundoo application");

            emailservice.SendEmail(request);
            return Ok(response);
 
        }

        [HttpPost]
        [Route("Login")]
        
        public IActionResult Login([FromBody] LoginModel model)
        {
            try
            {
                var result = iuserservice.Login(model);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpPost]
        [Route("ForgetPassword")]

        public IActionResult ForgetPassword([FromQuery] string email)
        {
            var token = iuserservice.ForgetPassword(email);


            EmailRequest request = new EmailRequest(email, "use this token to generate new password", token.ToString());

            emailservice.SendEmail(request);

            return Ok("token sent to email");
        }

        [HttpPost]
        [Route("ResetPassword")]
        [Authorize] 
        public  IActionResult ResetPassword(string email,string newpassword,string confirmpassword)
        {
            iuserservice.ResetPassword(email, newpassword,confirmpassword);

            return Ok("password changed successfully");

        }
        
    }
}

