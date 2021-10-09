using System;
using vtbbook.Application.Domain;
using vtbbook.Core.DataAccess.Models;

namespace vtbbook.Application.Service
{
    public class UserService : IUserService
    {
        private readonly IUserDomain _userDomain;

        public UserService(IUserDomain userDomain)
        {
            _userDomain = userDomain;
        }

        public Guid UserRegistration(User user) 
        {
            DbUser dbUser = new();
            dbUser.Email = user.Email;
            dbUser.PasswordHash = user.Password;
            return _userDomain.Add(dbUser).Id;
        }
    }
}
