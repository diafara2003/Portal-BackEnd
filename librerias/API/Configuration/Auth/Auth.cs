using Code.Repository.Model.Entity;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace API.Configuration.Auth
{
    public class Auth : IJwtAuth
    {
        private readonly string _key;
        public Auth(string key)
        {
            this._key = key;
        }
        public string Authentication(Usuario user)
        {
            var jwtTokenHandler = new JwtSecurityTokenHandler();

            var _keyByte = Encoding.ASCII.GetBytes(this._key);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim("Id", user.UserId.ToString()),
                    new Claim(JwtRegisteredClaimNames.GivenName, user.UserCorreo),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(_keyByte), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = jwtTokenHandler.CreateToken(tokenDescriptor);
            var jwtToken = jwtTokenHandler.WriteToken(token);

            return jwtToken;
        }
    }
}
