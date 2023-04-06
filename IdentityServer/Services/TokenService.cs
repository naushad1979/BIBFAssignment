using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace IdentityServer.Services
{
    public class TokenService : ITokenService
    {
        private readonly IOptions<JWTOptions> _jwtOptions;

        public TokenService(IOptions<JWTOptions> jwtOptions)
        {
            _jwtOptions = jwtOptions;
        }
        public string GetJwtToken(string userName)
        {
            var issuer = _jwtOptions.Value.ValidIssuer;
            var audience = _jwtOptions.Value.ValidAudience;
            var key = Encoding.UTF8.GetBytes(_jwtOptions.Value.Secret);
            var expires = DateTime.Now.AddMinutes(_jwtOptions.Value.ExpiryTime);
            var signingCredentials = new SigningCredentials(
                new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha512Signature
            );

            var subject = new ClaimsIdentity(new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, userName),
                new Claim(JwtRegisteredClaimNames.Email, userName)
            });

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = subject,
                Expires = expires,
                Issuer = issuer,
                Audience = audience,
                SigningCredentials = signingCredentials
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var jwtToken = tokenHandler.WriteToken(token);

            return jwtToken;
        }
    }
}
