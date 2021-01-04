using System.Threading.Tasks;
using AspBusiness.Businesses.Patients;
using AspBusiness.Models.Patients;
using AspBusiness.Providers;
using AspDataModel.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace AspCoreAPIStarter.Controllers.Patients
{
    [Route("api/patients/profile")]
    public class ProfileController: ControllerBase
    {
        private readonly IRegisteredPatientBussiness registeredPatientBussiness;
        private readonly ITokenGenerator tokenGenerator;


        public ProfileController(IRegisteredPatientBussiness registeredPatientBussiness, ITokenGenerator tokenGenerator)
        {
            this.registeredPatientBussiness = registeredPatientBussiness;
            this.tokenGenerator = tokenGenerator;
        }

        [HttpPost]
        public async Task<TokenResponse> Create([FromBody]ProfileCreate profileCreate)
        {
            var patient = await registeredPatientBussiness.Create(profileCreate);
            return tokenGenerator.GenerateToken(patient.QRCode.ToString("N"), Role.Patient);
        }
    }
}