using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using new_job_challenge.carrefour.api.Models;
using new_job_challenge.carrefour.domain.Interfaces;
using System.Net;

namespace new_job_challenge.carrefour.api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TokenController : Controller
    {
        private readonly ITokenService _tokenService;

        public TokenController(ITokenService securityService)
        {
            _tokenService = securityService;
        }

        [HttpPost]
        public IActionResult Login([FromBody] User loginDetails)
        {
            string userName = !string.IsNullOrEmpty(loginDetails.UserName) ? loginDetails.UserName : string.Empty;
            string password = !string.IsNullOrEmpty(loginDetails.Password) ? loginDetails.Password : string.Empty;

            if (userName == string.Empty || password == string.Empty)
            {
                string message = "Para login e geração do token, favor informar usuário e senha.";
                return Ok(message);
            }

            bool logged = _tokenService.Login(userName, password).Result;

            if (logged)
            {
                var tokenString = _tokenService.GenerateToken().Result;
                return Ok(new { token = tokenString });
            }
            else
                return Unauthorized();
        }
    }
}
