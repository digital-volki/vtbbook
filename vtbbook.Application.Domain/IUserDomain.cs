#nullable enable
using System.Linq;
using vtbbook.Core.DataAccess.Models;

namespace vtbbook.Application.Domain
{
    public interface IUserDomain
    {
        DbUser? Add(DbUser? dbUser);
        DbUser? Update(DbUser? dbCoupon);
        IQueryable<DbUser> Get();
    }
}