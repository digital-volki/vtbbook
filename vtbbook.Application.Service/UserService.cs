using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using vtbbook.Application.Domain;
using vtbbook.Application.Domain.Models;
using vtbbook.Core.Common;
using vtbbook.Core.DataAccess.Models;

namespace vtbbook.Application.Service
{
    public class UserService : IUserService
    {
        private readonly IUserDomain _userDomain;
        private readonly ISettings _settings;

        public UserService(IUserDomain userDomain, ISettings settings)
        {
            _userDomain = userDomain;
            _settings = settings;
        }

        public Guid UserRegistration(User user) 
        {
            return _userDomain.Add(new DbUser 
            {
                Email = user.Email,
                PasswordHash = user.Password,
            }).Id;
        }

        public int? GetCurrentBalance(Guid userId)
        {
            if (!UserExist(userId))
            {
                return null;
            }

            return GetUser(userId).Currency;
        }

        public bool UserExist(string email) 
        {
            return GetUser(email) != null;
        }

        public bool UserExist(Guid userId)
        {
            return GetUser(userId) != null;
        }

        public string UserAuth(User user)
        {
            DbUser dbUser = GetUser(user.Email);
            if (dbUser == null || dbUser.PasswordHash != user.Password)
            {
                return null;
            }
            return GenerateToken(dbUser);
        }

        public IEnumerable<Coupon> GetCoupons(Guid userId, int page, int count)
        {
            if (!UserExist(userId))
            {
                return null;
            }

            return GetUser(userId).Coupons
                .Skip((page - 1) * count)
                .Take(count)
                .Select(x => new Coupon
                {
                    Data = x.Data,
                    Expire = x.Expire
                });
        }

        public void AddCurrency(Guid userId, int currencyVolume)
        {
            if (!UserExist(userId))
            {
                return;
            }

            var dbUser = GetUser(userId);
            dbUser.Currency += currencyVolume;
            _userDomain.Update(dbUser);
        }

        public bool IsEnoughFunds(Guid userId, int currencyVolume)
        {
            if (!UserExist(userId))
            {
                return false;
            }

            return GetUser(userId).Currency >= currencyVolume;
        }

        private DbUser GetUser(string email)
        {
            return _userDomain.Get().Where(x => x.Email == email).FirstOrDefault();
        }

        private DbUser GetUser(Guid userId)
        {
            return _userDomain.Get().Where(x => x.Id == userId).FirstOrDefault();
        }

        private string GenerateToken(DbUser user)
        {
            AuthOptions authOptions = _settings.GetSection<AuthOptions>($"{nameof(AuthOptions)}");
            var claimsList = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.Email)
            };

            var now = DateTime.UtcNow;
            var jwt = new JwtSecurityToken(
                issuer: authOptions.Issuer,
                audience: authOptions.Audience,
                notBefore: now,
                claims: claimsList,
                expires: now.Add(TimeSpan.FromMinutes(authOptions.Lifetime)),
                signingCredentials: new SigningCredentials(
                    authOptions.GetSymmetricSecurityKey(),
                    SecurityAlgorithms.HmacSha256)
                );

            return new JwtSecurityTokenHandler().WriteToken(jwt);
        }
    }
}
