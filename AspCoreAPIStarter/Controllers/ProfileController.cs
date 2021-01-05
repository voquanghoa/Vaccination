using System;
using System.Threading.Tasks;
using AspBusiness.Businesses.Patients;
using AspBusiness.Exceptions;
using AspBusiness.Models.Patients;
using Microsoft.AspNetCore.Mvc;

namespace AspCoreAPIStarter.Controllers
{
    [Route("api/profile")]
    public class ProfileController: ControllerBase
    {
        private readonly ICommonPatientBussiness commonPatientBussiness;

        public ProfileController(ICommonPatientBussiness commonPatientBussiness)
        {
            this.commonPatientBussiness = commonPatientBussiness;
        }

        /// <summary>
        /// Xem thông tin bệnh nhân theo mã QR dành cho Assistant, Nurse, Admin
        /// </summary>
        /// <param name="qr"></param>
        /// <returns></returns>
        /// <exception cref="NotFoundException"></exception>
        [HttpGet]
        public async Task<ProfileInfo> GetProfile(Guid qr)
        {
            return await commonPatientBussiness.Get<ProfileInfo>(qr);
        }
    }
}