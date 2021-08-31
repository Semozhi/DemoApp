using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using API.Entities;
using API.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace API.Services
{
    public class TokenService : ITokenService

    {
        private readonly SymmetricSecurityKey _Key;

        public TokenService(IConfiguration Config)

        {
            _Key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Config["TokenKey"]));
        } 

        public string CreateToken(AppUser user)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.NameId,user.UserName),

            };
            var creds = new SigningCredentials(_Key, SecurityAlgorithms.HmacSha256Signature);

            var tokendescriptior = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = System.DateTime.Now.AddDays(7),
                SigningCredentials = creds
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokendescriptior);
            return tokenHandler.WriteToken(token);
        }
    }
}