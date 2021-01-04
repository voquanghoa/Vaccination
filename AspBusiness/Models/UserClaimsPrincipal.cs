using System;
using System.Security.Claims;
using AspDataModel.Contracts;

namespace AspBusiness.Models
{
    public class UserClaimsPrincipal : ClaimsPrincipal
    {
        public UserClaimsPrincipal(ClaimsIdentity claimsIdentity) : base(claimsIdentity)
        {
            Username = claimsIdentity.Name;
            Role = Enum.Parse<Role>(claimsIdentity.FindFirst(x => x.Type == ClaimTypes.Role).Value);
        }

        public string Username { get; }

        public Role Role { get; }
    }
}