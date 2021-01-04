using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AspBusiness.AutoConfig;
using AspBusiness.Contracts;
using AspDataModel.Contracts;
using AspDataModel.Models;
using Microsoft.IdentityModel.Tokens;

namespace AspBusiness.Providers
{
    [ImplementBy(typeof(PatientTokenGenerator))]
    public interface IPatientTokenGenerator
    {
        TokenResponse GenerateToken(Patient patient);
    }

    public class PatientTokenGenerator: IPatientTokenGenerator
    {
        private readonly SecuritySettings securitySettings;

        public readonly SigningCredentials SigningCredentials;

        public PatientTokenGenerator(SecuritySettings securitySettings)
        {
            this.securitySettings = securitySettings;

            var signingKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(this.securitySettings.Secret));
            SigningCredentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256);
        }

        private static Claim CreateClaim(string type, string value)
        {
            return new Claim(type, value, null, ClaimsIdentity.DefaultIssuer, "Provider");
        }

        public TokenResponse GenerateToken(Patient patient)
        {
            var claims = new List<Claim>
            {
                CreateClaim(ClaimTypes.NameIdentifier, patient.QRCode.ToString()),
                CreateClaim(ClaimTypes.Name, patient.FullName),
                CreateClaim(ClaimTypes.GivenName, patient.FullName),
                CreateClaim(ClaimTypes.Role, Role.Patient.ToString())
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