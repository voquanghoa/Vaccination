using System.Threading.Tasks;
using AspBusiness.Businesses.Patients;
using AspBusiness.Models;
using AspBusiness.Providers;
using AspDataModel.Contracts;
using AspDataModel.Utils;
using Microsoft.AspNetCore.Mvc;

namespace AspCoreAPIStarter.Controllers.Patients
{
    [Route("api/patients/auth")]
    public class AuthController: ControllerBase
    {
        private readonly ICommonPatientBussiness commonPatientBussiness;
        private readonly ITokenGenerator tokenGenerator;

        public AuthController(ITokenGenerator tokenGenerator, ICommonPatientBussiness commonPatientBussiness)
        {
            this.tokenGenerator = tokenGenerator;
            this.commonPatientBussiness = commonPatientBussiness;
        }

        /// <summary>
        /// Đăng nhập bởi bệnh nhân
        /// </summary>
        /// <param name="loginForm"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<TokenResponse> Login([FromBody] LoginForm loginForm)
        {
            var qrCode = await commonPatientBussiness.Login(loginForm);
            return tokenGenerator.GenerateToken(qrCode.FormatAsString(), Role.Patient);
        }
    }
}