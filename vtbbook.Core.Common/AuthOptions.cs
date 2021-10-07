using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace vtbbook.Core.Common
{
    public class AuthOptions
    {
        public string SecretKey;
        public string Issuer;
        public string Audience;
        public int Lifetime;
        public SymmetricSecurityKey GetSymmetricSecurityKey()
        {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(SecretKey));
        }
    }
}
