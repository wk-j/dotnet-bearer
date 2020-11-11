using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace MyWeb.Controllers.Accounts {

    public static class TokenUtils {
        public static string Key { get; } = "b80ea3d811ee0c648e32d663fa44e24cff41b3de";

        public static SymmetricSecurityKey GetSymmetricKey() {
            var key = TokenUtils.Key;
            return new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
        }

        public static string GenerateToken(string username, int expireMinutes = 20) {
            var base64 = Convert.ToBase64String(Encoding.UTF8.GetBytes(Key));
            var symmetricKey = Convert.FromBase64String(base64);
            var tokenHandler = new JwtSecurityTokenHandler();

            var now = DateTime.UtcNow;
            var tokenDescriptor = new SecurityTokenDescriptor {
                Audience = "bcircle",
                Issuer = "bcircle",
                Subject = new ClaimsIdentity(new[] {
                    new Claim(ClaimTypes.Name, username),
                    new Claim(ClaimTypes.System, "API"),
                }),
                Expires = now.AddMinutes(Convert.ToInt32(expireMinutes)),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(symmetricKey),
                    SecurityAlgorithms.HmacSha256Signature)
            };

            var stoken = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(stoken);
        }
    }
}