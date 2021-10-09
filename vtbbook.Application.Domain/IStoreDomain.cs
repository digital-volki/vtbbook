#nullable enable
using System.Linq;
using vtbbook.Core.DataAccess.Models;

namespace vtbbook.Application.Domain
{
    public interface IStoreDomain
    {
        DbProduct? Add(DbProduct? dbProduct);
        IQueryable<DbProduct> Get();
    }
}