using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace IdentityServer.Services
{
    public interface ITokenService
    {
        string GetJwtToken(string userName);
    }
}
