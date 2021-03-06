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
        private readonly ICommonPatientBussiness commonPatientBussiness;

        public ProfileController(ICommonPatientBussiness commonPatientBussiness)
        {
            this.commonPatientBussiness = commonPatientBussiness;
        }

        /// <summary>
        /// Xem thông tin theo mã QR
        /// </summary>
        /// <param name="qr"></param>
        /// <returns></returns>
        /// <exception cref="NotFoundException"></exception>
        [HttpGet]
        public async Task<VisitorProfileInfo> GetProfile(Guid qr)
        {
            return await commonPatientBussiness.Get<VisitorProfileInfo>(qr);
        }
    }
}