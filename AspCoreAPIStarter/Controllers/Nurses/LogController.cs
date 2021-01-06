using System;
using System.Threading.Tasks;
using AspBusiness.Businesses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AspCoreAPIStarter.Controllers.Nurses
{
    [Route("api/nurses/log")]
    [Authorize(Roles = "Nurse,Admin")]
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
        public async Task LogFirst(Guid qr) => await nurseBusiness.SetFirst(qr);


        /// <summary>
        /// Xác nhận tiêm lần 2
        /// </summary>
        /// <param name="qr"></param>
        [HttpPost("second")]
        public async Task LogSecond(Guid qr) => await nurseBusiness.SetSecond(qr);
    }
}