using System;
using System.Threading.Tasks;
using AspBusiness.Businesses.Patients;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AspCoreAPIStarter.Controllers.Assistants
{
    [Route("api/assistants/profile")]
    [Authorize(Roles = "Assistant,Admin")]
    public class ProfileController
    {
        private readonly IPatientBussiness patientBussiness;

        public ProfileController(IPatientBussiness patientBussiness)
        {
            this.patientBussiness = patientBussiness;
        }

        /// <summary>
        /// Xác nhận tài khoản bởi Assistant
        /// </summary>
        /// <param name="qr">qr code</param>
        /// <returns></returns>
        [HttpPost]
        public async Task Approval(Guid qr) => await patientBussiness.Approval(qr);
    }
}