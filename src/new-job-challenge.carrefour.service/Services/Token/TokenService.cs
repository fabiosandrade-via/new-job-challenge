using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using new_job_challenge.carrefour.application.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace new_job_challenge.carrefour.infrastructure.security.Services.Token
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration _config;
        public TokenService(IConfiguration configuration)
        {
            _config = configuration;
        }

        public async Task<bool> Login(string userName, string password)
        {
            return await Task.FromResult(ValidateUser(userName, password));
        }

        private bool ValidateUser(string userName, string password)
        {
            if (userName == "avalia" && password == "teste")
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<string> GenerateToken()
        {
            var issuer = _config["Jwt:Issuer"];
            var audience = _config["Jwt:Audience"];
            var expiry = DateTime.Now.AddMinutes(60);
            var keyAssign = _config["Jwt:Key"];

            if (string.IsNullOrEmpty(keyAssign))
            {
                keyAssign = Guid.NewGuid().ToString();
            }

            var key = Encoding.UTF8.GetBytes(keyAssign);

            var securityKey = new SymmetricSecurityKey
                              (key);
            var credentials = new SigningCredentials
                              (securityKey, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(issuer: issuer,
                                             audience: audience,
                                             expires: expiry,
                                             signingCredentials: credentials);
            var tokenHandler = new JwtSecurityTokenHandler();
            var stringToken = tokenHandler.WriteToken(token);
            return await Task.FromResult(stringToken);
        }
    }
}