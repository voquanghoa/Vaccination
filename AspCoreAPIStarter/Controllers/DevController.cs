using System;
using AspBusiness.Businesses;
using AspBusiness.Models;
using AspBusiness.Providers;
using AspDataModel.Contracts;
using AspDataModel.Utils;
using Microsoft.AspNetCore.Mvc;

namespace AspCoreAPIStarter.Controllers
{
    [Route("api/dev")]
    public class DevController: ControllerBase
    {
        private readonly ITokenGenerator tokenGenerator;
        private readonly IAuthenInfo authenInfo;

        public DevController(ITokenGenerator tokenGenerator, IAuthenInfo authenInfo)
        {
            this.tokenGenerator = tokenGenerator;
            this.authenInfo = authenInfo;
        }

        /// <summary>
        /// Test generate token
        /// </summary>
        /// <param name="loginForm"></param>
        /// <returns></returns>
        [HttpPost("token")]
        public string Login([FromBody] LoginForm loginForm)
        {
            return tokenGenerator.GenerateToken(loginForm.Username, Role.Assistant).Token;
        }

        /// <summary>
        /// Recheck generated token
        /// </summary>
        /// <returns></returns>
        [HttpGet("token")]
        public LoggedInUser GetMe()
        {
            return authenInfo.Get();
        }

        /// <summary>
        /// Kiểm tra mã QR, ví dụ 644e1dd7-2a7f-18fb-b8ed-ed78c3f92c2b
        /// </summary>
        /// <param name="qr"></param>
        /// <returns></returns>
        [HttpGet("qr")]
        public string TryGuid(Guid qr)
        {
            return "Mã QR là " + qr.FormatAsString();
        }
    }
}