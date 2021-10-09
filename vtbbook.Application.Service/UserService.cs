using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using vtbbook.Application.Domain;
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
            DbUser dbUser = new();
            dbUser.Email = user.Email;
            dbUser.PasswordHash = user.Password;
            return _userDomain.Add(dbUser).Id;
        }

        private DbUser GetUserByEmail(string email)
        {
            return _userDomain.Get().Where(x => x.Email == email).FirstOrDefault();
        }

        public bool UserExist(string email) 
        {
            return GetUserByEmail(email) != null;
        }

        public string UserAuth(User user)
        {
            DbUser dbUser = GetUserByEmail(user.Email);
            if (dbUser == null || dbUser.PasswordHash != user.Password)
            {
                return null;
            }
            return GenerateToken(dbUser);
        }

        private string GenerateToken(DbUser user)
        {
            AuthOptions authOptions = _settings.GetSection<AuthOptions>($"{nameof(AuthOptions)}");
            var claimsList = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.Email)
            };

            var jwt = new JwtSecurityToken(
                issuer: authOptions.Issuer,
                audience: authOptions.Audience,
                notBefore: DateTime.UtcNow,
                claims: claimsList,
                expires: DateTime.UtcNow.Add(TimeSpan.FromMinutes(authOptions.Lifetime)),
                signingCredentials: new SigningCredentials(
                    authOptions.GetSymmetricSecurityKey(),
                    SecurityAlgorithms.HmacSha256)
                );

            return new JwtSecurityTokenHandler().WriteToken(jwt);
        }
    }
}
