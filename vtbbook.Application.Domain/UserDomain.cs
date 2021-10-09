﻿#nullable enable
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

            var account = _dataContext.Insert(dbUser);

            if (account == null)
            {
                return null;
            }

            return _dataContext.Save() != 0 ? account : null;
        }
    }
}