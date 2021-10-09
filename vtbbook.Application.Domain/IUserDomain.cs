#nullable enable
using vtbbook.Core.DataAccess.Models;

namespace vtbbook.Application.Domain
{
    public interface IUserDomain
    {
        DbUser? Add(DbUser? dbUser);
    }
}