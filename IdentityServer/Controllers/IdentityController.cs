using IdentityServer.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IdentityServer.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class IdentityController : ControllerBase
    {
        private readonly ILogger<IdentityController> _logger;
        private readonly ITokenService _tokenService;

        public IdentityController(ILogger<IdentityController> logger,
            ITokenService tokenService)
        {
            _logger = logger;
            _tokenService = tokenService;
        }

        [AllowAnonymous]
        [HttpGet]
        public IActionResult GetToken(string userName)
        {
            IActionResult unauthorizedResponse = Unauthorized();             

            if (!string.IsNullOrEmpty(userName))
            {
                //Admin is default user who will have access to get the token.
                //This logic can be impemented with custom store like databse but this is 
                // currently out of scope.
                if (userName.ToUpperInvariant().Equals("ADMIN"))
                {
                    var token = _tokenService.GetJwtToken(userName);
                    return Ok(token);
                }
                return unauthorizedResponse;
            }

            return unauthorizedResponse;
        }
    }
}