using System;

namespace vtbbook.Application.Service
{
    public interface IUserService
    {
        Guid UserRegistration(User user);
        bool UserExist(string email);
        string UserAuth(User user);
    }
}