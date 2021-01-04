using System.Threading.Tasks;
using AspBusiness.Providers;
using AspDataModel.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AspCoreAPIStarter.Controllers
{
    //[Route("api/auth")]
    public class AuthController: ControllerBase
    {
        private readonly ITokenGenerator tokenGenerator;
        private readonly HttpContext httpContext;

        public AuthController(ITokenGenerator tokenGenerator, HttpContext httpContext)
        {
            this.tokenGenerator = tokenGenerator;
            this.httpContext = httpContext;
        }

        /*
        /// <summary>
        /// Đăng nhập người dùng
        /// </summary>
        /// <param name="userLogin">Chứa thông tin đăng nhập</param>
        /// <returns>Thông tin token nếu đăng nhập thành công</returns>
        //[HttpPost]
        public async Task<TokenResponse> Login([FromBody] UserLogin userLogin)
        {
            return await Task.FromResult(null);//await tokenGenerator.GenerateToken(userLogin.Username, Role.Admin));
        }
        */
    }
}