using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WebLibrary.Entities;
using WebLibrary.Entities.DTO;
using WebLibrary.Services.Interfaces;

namespace WebLibrary.Services {
    public class TokenServiceHMAC : ITokenService {

        private readonly UserManager<User> _userManager;
        private readonly byte[] _key = Encoding.ASCII.GetBytes(Key.SecretKey);

        public TokenServiceHMAC(UserManager<User> userManager) {
            _userManager = userManager;
        }

        public async Task<TokenDTO> GenerateToken(User user) {
            if (user == null) return null;
            var tokenConfig = new SecurityTokenDescriptor {
                IssuedAt = DateTime.Now,
                Issuer = "WebLibrary",
                Subject = new ClaimsIdentity(new Claim[] {
                    new Claim("UserEmail", user.Email)
                }),
                Expires = DateTime.Now.AddHours(5),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(_key), SecurityAlgorithms.HmacSha256)
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenConfig);
            var tokenString = tokenHandler.WriteToken(token);
            return new TokenDTO { Token = tokenString };
        }

        public async Task<TokenResult> ValidateToken(string token) {
            try {
                var tokenHandler = new JwtSecurityTokenHandler();

                var validation = new TokenValidationParameters {
                    ValidateIssuer = true,
                    ValidIssuer = "WebLibrary",

                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero,

                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(_key)
                };
                var principal = tokenHandler.ValidateToken(token, validation, out SecurityToken validatedToken);
                var email = principal.FindFirst("UserEmail")?.Value;
                var user = await _userManager.FindByEmailAsync(email);
                string role = ((await _userManager.GetRolesAsync(user)).Contains("Admin")) ? "Admin" : "User";
                return new TokenResult {
                    IsValid = true,
                    Email = email,
                    Role = role
                };

            } catch (Exception e) {
                return new TokenResult {
                    IsValid = false
                };
            }
        }

    }
}
