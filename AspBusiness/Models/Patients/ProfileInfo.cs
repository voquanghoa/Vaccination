using System;
using AspBusiness.AutoConfig;
using AspDataModel.Models;

namespace AspBusiness.Models.Patients
{
    [ConvertFrom(typeof(Patient))]
    [ConvertFrom(typeof(RegisteredPatient))]
    public class ProfileInfo: IdBase
    {
        public string FullName { get; set; }

        public string PersonalId { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }

        public string Password { get; set; }

        public Guid QRCode { get; set; }

        public string ProfileUrl { get; set; }

        public Gender Sex { get; set; }

        public DateTime? Birthday { get; set; }

        public string Address { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime? ApprovalDate { get; set; }

        public bool Valid { get; set; }
    }
}