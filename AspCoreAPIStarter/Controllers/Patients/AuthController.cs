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
        private readonly IPatientBussiness patientBussiness;
        private readonly ITokenGenerator tokenGenerator;

        public AuthController(ITokenGenerator tokenGenerator, IPatientBussiness patientBussiness)
        {
            this.tokenGenerator = tokenGenerator;
            this.patientBussiness = patientBussiness;
        }

        /// <summary>
        /// Đăng nhập bởi bệnh nhân
        /// </summary>
        /// <param name="loginForm"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<TokenResponse> Login([FromBody] LoginForm loginForm)
        {
            var patient = await patientBussiness.Login(loginForm);
            return tokenGenerator.GenerateToken(patient.QRCode.FormatAsString(), Role.Patient);
        }
    }
}