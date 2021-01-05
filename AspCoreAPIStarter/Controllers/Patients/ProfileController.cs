using System.Threading.Tasks;
using AspBusiness.Businesses;
using AspBusiness.Businesses.Patients;
using AspBusiness.Exceptions;
using AspBusiness.Models.Patients;
using AspBusiness.Providers;
using AspDataModel.Contracts;
using AspDataModel.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AspCoreAPIStarter.Controllers.Patients
{
    [Route("api/patients/profile")]
    public class ProfileController: ControllerBase
    {
        private readonly IRegisteredPatientBussiness registeredPatientBussiness;
        private readonly ITokenGenerator tokenGenerator;
        private readonly IAuthenInfo authenInfo;
        private readonly IPatientBussiness patientBussiness;
        private readonly ICommonPatientBussiness commonPatientBussiness;


        public ProfileController(IRegisteredPatientBussiness registeredPatientBussiness, ITokenGenerator tokenGenerator, IPatientBussiness patientBussiness, IAuthenInfo authenInfo, ICommonPatientBussiness commonPatientBussiness)
        {
            this.registeredPatientBussiness = registeredPatientBussiness;
            this.tokenGenerator = tokenGenerator;
            this.patientBussiness = patientBussiness;
            this.authenInfo = authenInfo;
            this.commonPatientBussiness = commonPatientBussiness;
        }

        /// <summary>
        /// Submit form đăng ký
        /// </summary>
        /// <param name="profileCreate"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ProfileCreateResponse> Create([FromBody]ProfileCreate profileCreate)
        {
            var patient = await registeredPatientBussiness.Create(profileCreate);
            var token = tokenGenerator.GenerateToken(patient.QRCode.FormatAsString(), Role.Patient).Token;
            return new ProfileCreateResponse()
            {
                QRCode = patient.QRCode,
                Token = token
            };
        }

        /// <summary>
        /// Lấy thông tin của tôi
        /// </summary>
        /// <returns></returns>
        [Authorize(Roles = "Patient")]
        [HttpGet]
        public async Task<ProfileInfo> GetMe()
        {
            var qr = authenInfo.Get().Username.FromString();
            return await commonPatientBussiness.Get<ProfileInfo>(qr);
        }
    }
}