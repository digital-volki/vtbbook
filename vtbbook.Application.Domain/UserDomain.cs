#nullable enable
using Microsoft.EntityFrameworkCore;
using System.Linq;
using vtbbook.Core.DataAccess;
using vtbbook.Core.DataAccess.Models;

namespace vtbbook.Application.Domain
{
    public class UserDomain : IUserDomain
    {
        private readonly IDataContext _dataContext;

        public UserDomain(IDataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public DbUser? Add(DbUser? dbUser)
        {
            if (dbUser == null)
            {
                return null;
            }

            var user = _dataContext.Insert(dbUser);

            if (user == null)
            {
                return null;
            }

            return _dataContext.Save() != 0 ? user : null;
        }

        public DbUser? Update(DbUser? dbUser)
        {
            if (dbUser == null)
            {
                return null;
            }

            var user = _dataContext.Update(dbUser);

            if (user == null)
            {
                return null;
            }

            return _dataContext.Save() != 0 ? user : null;
        }

        public IQueryable<DbUser> Get()
        {
            return _dataContext.GetQueryable<DbUser>()
                .Include(x => x.Coupons);
        }
    }
}
