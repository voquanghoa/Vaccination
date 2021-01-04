using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AspBusiness.AutoConfig;
using AspDataModel.Contracts;
using Microsoft.IdentityModel.Tokens;

namespace AspBusiness.Providers
{
    [ImplementBy(typeof(TokenGenerator))]
    public interface ITokenGenerator
    {
        TokenResponse GenerateToken(string username, Role role);
    }

    public class TokenGenerator : ITokenGenerator
    {
        private readonly SecuritySettings securitySettings;

        public readonly SigningCredentials SigningCredentials;

        public TokenGenerator(SecuritySettings securitySettings)
        {
            this.securitySettings = securitySettings;

            var signingKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(this.securitySettings.Secret));
            SigningCredentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256);
        }

        private static Claim CreateClaim(string type, string value)
        {
            return new Claim(type, value, null, ClaimsIdentity.DefaultIssuer, "Provider");
        }

        public TokenResponse GenerateToken(string username, Role role)
        {
            var claims = new List<Claim>
            {
                CreateClaim(ClaimTypes.NameIdentifier, username),
                CreateClaim(ClaimTypes.Name, username),
                CreateClaim(ClaimTypes.Role, role.ToString())
            };

            var identity = new ClaimsIdentity(claims, "Bearer");

            var now = DateTime.UtcNow;

            var jwt = new JwtSecurityToken(
                claims: identity.Claims,
                notBefore: now,
                expires: now.AddSeconds(securitySettings.Expiration),
                signingCredentials: SigningCredentials);

            var token = new JwtSecurityTokenHandler().WriteToken(jwt);

            return new TokenResponse(token);
        }
    }
}