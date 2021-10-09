#nullable enable
using Microsoft.EntityFrameworkCore;
using System.Linq;
using vtbbook.Core.DataAccess;
using vtbbook.Core.DataAccess.Models;

namespace vtbbook.Application.Domain
{
    public class StoreDomain : IStoreDomain
    {
        private readonly IDataContext _dataContext;

        public StoreDomain(IDataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public DbProduct? Add(DbProduct? dbProduct)
        {
            if (dbProduct == null)
            {
                return null;
            }

            var account = _dataContext.Insert(dbProduct);

            if (account == null)
            {
                return null;
            }

            return _dataContext.Save() != 0 ? account : null;
        }

        public IQueryable<DbProduct> Get()
        {
            return _dataContext.GetQueryable<DbProduct>()
                .Include(x => x.Coupons);
        }
    }
}
