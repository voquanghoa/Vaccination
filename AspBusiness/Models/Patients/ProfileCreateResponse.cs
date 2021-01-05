using System;

namespace AspBusiness.Models.Patients
{
    public class ProfileCreateResponse
    {
        public string Token { get; set; }

        public Guid QRCode { get; set; }
    }
}