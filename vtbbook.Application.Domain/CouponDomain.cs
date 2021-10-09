#nullable enable
using Microsoft.EntityFrameworkCore;
using System.Linq;
using vtbbook.Core.DataAccess;
using vtbbook.Core.DataAccess.Models;

namespace vtbbook.Application.Domain
{
    public class CouponDomain : ICouponDomain
    {
        private readonly IDataContext _dataContext;

        public CouponDomain(IDataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public DbCoupon? Add(DbCoupon? dbCoupon)
        {
            if (dbCoupon == null)
            {
                return null;
            }

            var account = _dataContext.Insert(dbCoupon);

            if (account == null)
            {
                return null;
            }

            return _dataContext.Save() != 0 ? account : null;
        }

        public IQueryable<DbCoupon> Get()
        {
            return _dataContext.GetQueryable<DbCoupon>()
                .Include(x => x.Product);
        }
    }
}
