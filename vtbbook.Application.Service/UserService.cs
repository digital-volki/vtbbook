using System;
using vtbbook.Application.Domain;

namespace vtbbook.Application.Service
{
    public class UserService : IUserService
    {
        private readonly IUserDomain _userDomain;

        public UserService(IUserDomain userDomain)
        {
            _userDomain = userDomain;
        }
    }
}
