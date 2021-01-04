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


        public ProfileController(IRegisteredPatientBussiness registeredPatientBussiness, ITokenGenerator tokenGenerator, IPatientBussiness patientBussiness, IAuthenInfo authenInfo)
        {
            this.registeredPatientBussiness = registeredPatientBussiness;
            this.tokenGenerator = tokenGenerator;
            this.patientBussiness = patientBussiness;
            this.authenInfo = authenInfo;
        }

        /// <summary>
        /// Submit form đăng ký
        /// </summary>
        /// <param name="profileCreate"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<TokenResponse> Create([FromBody]ProfileCreate profileCreate)
        {
            var patient = await registeredPatientBussiness.Create(profileCreate);
            return tokenGenerator.GenerateToken(patient.QRCode.FormatAsString(), Role.Patient);
        }

        /// <summary>
        /// Lấy thông tin của tôi
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotFoundException"></exception>
        [Authorize(Roles = "Patient")]
        [HttpGet]
        public async Task<ProfileInfo> GetMe()
        {
            var qr = authenInfo.Get().Username.FromString();

            var profile = await patientBussiness.Get(qr);

            if (profile != null)
            {
                profile.Valid = true;
                return profile;
            }

            profile = await registeredPatientBussiness.Get(qr);

            if (profile != null)
            {
                profile.Valid = false;
                return profile;
            }

            throw new NotFoundException("No patient found");
        }
    }
}