using Framework.Core.Contracts.Security;
using Management.Infrastructure.Data;
using Management.Infrastructure.Models;
using Management.Infrastructure.Models.Repositories;
using Management.Infrastructure.Service.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;

namespace SecurityService.SSO.Services
{
    /// <summary>
    /// this service use for recovery password
    /// </summary>
    public class UserService 
    {
        private readonly QueryDbContext _dbContext = new QueryDbContext();
        private  UserRepository userRepository;


        public UserService()
        {
            userRepository = new UserRepository(_dbContext);
        }
        public User FindByName(string name)
        {
            return _dbContext.Users.Where(u => u.UserName == name).FirstOrDefault();
        }
       
        public PasswordVerificationResult VerifyHashedPassword(string hashedPassword, string providedPassword)
        {
            return VerifyHashedPassword(hashedPassword, providedPassword);
        }

        public void UpdatePassword(User user, string password)
        {
            if (user != null)
            {
                user.PasswordHash = Crypto.HashPassword(password);
                userRepository.Update(user);
                _dbContext.SaveChanges();
            }
        }
    }
}