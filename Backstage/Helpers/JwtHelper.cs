using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace Backstage.Helpers
{
    public class JwtHelper
    {
        public string GenerateToken(string username)
        {
            //可放在組態
            var issuer = "RentWeb"; //發行者
            var signKey = "1Zl4h9703IzROidasgfegkK3@f4po1jkd"; //加密金鑰  自己可亂打，但長度有規定?搭配演算法，16位以上

            var claims = new List<Claim>();
            claims.Add(new Claim(JwtRegisteredClaimNames.Sub, username));
            claims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
            //設一些要寫在payload中的資訊

            var userClaimsIdentity = new ClaimsIdentity(claims);
            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(signKey));
            var signingCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256Signature);//演算法

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Expires = DateTime.UtcNow.AddHours(1),  //!過期時間
                Issuer = issuer,    //發行者
                SigningCredentials = signingCredentials,    //加密設定
                Subject = userClaimsIdentity
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var securityToken = tokenHandler.CreateToken(tokenDescriptor);
            var token = tokenHandler.WriteToken(securityToken); //負責產生token的類別?

            return token;
        }
    }
}
