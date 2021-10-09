using System;
using System.Collections.Generic;
using vtbbook.Application.Domain.Models;

namespace vtbbook.Application.Service
{
    public interface IUserService
    {
        Guid UserRegistration(User user);
        int? GetCurrentBalance(Guid userId);
        bool UserExist(string email);
        bool UserExist(Guid userId);
        string UserAuth(User user);
        IEnumerable<Coupon> GetCoupons(Guid userId, int page, int count);
        void AddCurrency(Guid userId, int currencyVolume);
        bool IsEnoughFunds(Guid userId, int currencyVolume);
    }
}