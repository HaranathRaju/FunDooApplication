using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModelLayer.Entities;
using ModelLayer.DTO;


namespace BusinessLogicLayer.Interfaces
{
    public interface IUserService
    {
        UserResponse Register(RegisterModel model);


        UserResponse  Login(LoginModel model);

    }
}
