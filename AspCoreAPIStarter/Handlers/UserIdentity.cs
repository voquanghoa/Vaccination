using System.Security.Claims;
using System.Security.Principal;
using AspDataModel.Contracts;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace AspCoreAPIStarter.Handlers
{
    public class UserIdentity : IIdentity
    {
        public IUser Account { get; }

        public UserIdentity(IUser account)
        {
            Account = account;
        }

        public string Name => Account.Username;

        public string AuthenticationType => JwtBearerDefaults.AuthenticationScheme;

        public bool IsAuthenticated => true;
    }

    public class UserClaimsPrincipal : ClaimsPrincipal
    {
        public UserIdentity UserIdentity { get; set; }

        public UserClaimsPrincipal(UserIdentity userIdentity) : base(userIdentity)
        {
            UserIdentity = userIdentity;
        }
    }
}