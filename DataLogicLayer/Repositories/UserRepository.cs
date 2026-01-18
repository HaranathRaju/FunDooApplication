using DataLogicLayer.Context;
using DataLogicLayer.Interfaces;
using ModelLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLogicLayer.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly FundooContext context;

        public UserRepository(FundooContext context)
        {
            this.context = context;
        }

        public User AddUser(User user)
        {

            var addUser = context.Users.FirstOrDefault(users => user.Email == users.Email);

            if (addUser != null)
                throw new Exception("email already exist");

            context.Users.Add(user);
            context.SaveChanges();

            return user;
        }

        public User GetUserByEmail(string email)
        {
            User user= context.Users.FirstOrDefault(x => x.Email == email);
            if (user == null)
            {
                return null;
            }
            return user;
        }

        public User UpdatePassword(string email,string newpassword)
        {
            var user=context.Users.Where(l => l.Email==email).FirstOrDefault();

            if (user == null)
            {
                return null;
            }
                
            user.Password = newpassword;
            context.SaveChanges();

            return user;


        } 
       
    }
}
