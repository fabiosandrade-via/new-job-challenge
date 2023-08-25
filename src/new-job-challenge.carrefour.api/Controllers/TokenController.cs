using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using new_job_challenge.carrefour.application.Common.Models.DTOs;
using new_job_challenge.carrefour.domain.Interfaces;
using System.Net;

namespace new_job_challenge.carrefour.api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TokenController : Controller
    {
        private readonly ITokenService _tokenService;

        public TokenController(ITokenService tokenService)
        {
            _tokenService = tokenService;
        }

        [HttpPost]
        public IActionResult Login([FromBody] UserModelDTO userModelDTO)
        {
            string userName = !string.IsNullOrEmpty(userModelDTO.UserName) ? userModelDTO.UserName : string.Empty;
            string password = !string.IsNullOrEmpty(userModelDTO.Password) ? userModelDTO.Password : string.Empty;

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
