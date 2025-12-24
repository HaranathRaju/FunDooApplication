using ModelLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLogicLayer.Interfaces
{
    public interface IUserRepository
    {
        User AddUser(User user);
        User GetUserByEmail(string email);
    }
}
