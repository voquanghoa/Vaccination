using System.Threading.Tasks;
using AspBusiness.Businesses;
using AspBusiness.Models;
using AspBusiness.Providers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AspCoreAPIStarter.Controllers
{
    [Route("api/auth")]
    public class AuthController: ControllerBase
    {
        private readonly IAuthBusiness authBusiness;
        private readonly ITokenGenerator tokenGenerator;
        private readonly IAuthenInfo authenInfo;

        public AuthController(IAuthBusiness authBusiness, ITokenGenerator tokenGenerator, IAuthenInfo authenInfo)
        {
            this.authBusiness = authBusiness;
            this.tokenGenerator = tokenGenerator;
            this.authenInfo = authenInfo;
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

        /// <summary>
        /// Lấy quyền thông tin người dùng hiện tại (cho nurse, admin,assistant)
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Authorize(Roles = "Assistant,Nurse,Admin")]
        public LoggedInUser GetMe()
        {
            return authenInfo.Get();
        }
    }
}