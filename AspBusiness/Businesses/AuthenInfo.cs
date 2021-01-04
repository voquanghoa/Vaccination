using AspBusiness.AutoConfig;
using AspBusiness.Models;
using Microsoft.AspNetCore.Http;

namespace AspBusiness.Businesses
{
    [ImplementBy(typeof(AuthenInfo))]
    public interface IAuthenInfo
    {
        LoggedInUser Get();
    }
    public class AuthenInfo: IAuthenInfo
    {
        private readonly IHttpContextAccessor context;

        public AuthenInfo(IHttpContextAccessor context)
        {
            this.context = context;
        }

        public LoggedInUser Get()
        {
            if (context.HttpContext.User is UserClaimsPrincipal user)
            {
                return new LoggedInUser
                {
                    Username = user.Username,
                    Role = user.Role
                };
            }

            return null;
        }
    }
}