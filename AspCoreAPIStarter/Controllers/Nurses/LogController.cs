using System;
using AspBusiness.Businesses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AspCoreAPIStarter.Controllers.Nurses
{
    [Route("api/nurses/log")]
    [Authorize(Roles = "Nurse")]
    public class LogController: ControllerBase
    {
        private readonly INurseBusiness nurseBusiness;

        public LogController(INurseBusiness nurseBusiness)
        {
            this.nurseBusiness = nurseBusiness;
        }

        /// <summary>
        /// Xác nhận tiêm lần 1
        /// </summary>
        /// <param name="qr"></param>
        [HttpPost("first")]
        public void LogFirst(Guid qr) => nurseBusiness.SetFirst(qr);


        /// <summary>
        /// Xác nhận tiêm lần 2
        /// </summary>
        /// <param name="qr"></param>
        [HttpPost("second")]
        public void LogSecond(Guid qr) => nurseBusiness.SetSecond(qr);
    }
}