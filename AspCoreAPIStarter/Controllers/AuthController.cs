using System.Threading.Tasks;
using AspBusiness.Businesses;
using AspBusiness.Providers;
using Microsoft.AspNetCore.Mvc;

namespace AspCoreAPIStarter.Controllers
{
    [Route("api/auth")]
    public class AuthController: ControllerBase
    {
        private readonly IAuthBusiness authBusiness;
        private readonly ITokenGenerator tokenGenerator;

        public AuthController(IAuthBusiness authBusiness, ITokenGenerator tokenGenerator)
        {
            this.authBusiness = authBusiness;
            this.tokenGenerator = tokenGenerator;
        }

        /// <summary>
        /// Đăng nhập cho assistant, doctor, admin
        /// </summary>
        /// <param name="userLogin">Thông tin đăng nhập</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<TokenResponse> Login([FromBody] UserLogin userLogin)
        {
            var user = await authBusiness.Login(userLogin);
            return tokenGenerator.GenerateToken(user.Username, user.Role);
        }
    }
}