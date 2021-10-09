using System;
using System.Collections.Generic;
using vtbbook.Application.Domain.Models;
using vtbbook.Core.DataAccess.Models;

namespace vtbbook.Application.Service
{
    public interface IStoreService
    {
        IEnumerable<Product> GetProducts(int page, int count);
        Coupon BuyProduct(Guid userId, Guid productId);
        DbProduct GetProduct(Guid productId);
    }
}