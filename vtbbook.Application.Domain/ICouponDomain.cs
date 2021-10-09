#nullable enable
using System.Linq;
using vtbbook.Core.DataAccess.Models;

namespace vtbbook.Application.Domain
{
    public interface ICouponDomain
    {
        DbCoupon? Add(DbCoupon? dbCoupon);
        IQueryable<DbCoupon> Get();
    }
}