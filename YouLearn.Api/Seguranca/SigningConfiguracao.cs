using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace YouLearn.Api.Seguranca
{
    public class SigningConfiguracao
    {
        private const string SECRET_KEY = "c1f51f42-5727-4d15-b787-c6bbbb645024";

        public SigningCredentials SigningCredentials { get; }
        private readonly SymmetricSecurityKey _signinKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(SECRET_KEY));

        public SigningConfiguracao()
        {
            SigningCredentials = new SigningCredentials(_signinKey, SecurityAlgorithms.HmacSha256);
        }

    }
}
