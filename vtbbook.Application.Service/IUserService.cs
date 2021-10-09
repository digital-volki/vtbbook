using System;

namespace vtbbook.Application.Service
{
    public interface IUserService
    {
        Guid UserRegistration(User user);
    }
}