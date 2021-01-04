using System;
using System.Threading.Tasks;
using AspBusiness.Businesses.Patients;
using AspBusiness.Exceptions;
using AspBusiness.Models.Patients;
using Microsoft.AspNetCore.Mvc;

namespace AspCoreAPIStarter.Controllers.Visitors
{
    [Route("api/vistors/profile")]
    public class ProfileController: ControllerBase
    {
        private readonly IPatientBussiness patientBussiness;
        private readonly IRegisteredPatientBussiness registeredPatientBussiness;

        public ProfileController(IPatientBussiness patientBussiness, IRegisteredPatientBussiness registeredPatientBussiness)
        {
            this.patientBussiness = patientBussiness;
            this.registeredPatientBussiness = registeredPatientBussiness;
        }

        /// <summary>
        /// Xem thông tin theo mã QR
        /// </summary>
        /// <param name="qr"></param>
        /// <returns></returns>
        /// <exception cref="NotFoundException"></exception>
        [HttpGet]
        public async Task<ProfileInfo> GetProfile(Guid qr)
        {
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