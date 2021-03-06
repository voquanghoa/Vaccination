using System;
using AspBusiness.AutoConfig;
using AspDataModel.Models;

namespace AspBusiness.Models.Patients
{
    [ConvertTo(typeof(Patient))]
    [ConvertTo(typeof(RegisteredPatient))]
    public class ProfileCreate
    {
        public string FullName { get; set; }

        public string PersonalId { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }

        public string Password { get; set; }

        public string ProfileUrl { get; set; }

        public Gender Sex { get; set; }

        public DateTime? Birthday { get; set; }

        public string Address { get; set; }
    }
}