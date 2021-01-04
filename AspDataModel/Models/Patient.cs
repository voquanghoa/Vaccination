using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace AspDataModel.Models
{
    [Table("Patients")]
    public class Patient: IdBase
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

        public DateTime? RemindFirstVaccinateDateTime { get; set; }
        public DateTime? FirstVaccinateDateTime { get; set; }
        public string FirstVaccinateDescription { get; set; }

        public DateTime? RemindSecondVaccinateDateTime { get; set; }
        public DateTime? SecondVaccinateDateTime { get; set; }
        public string SecondVaccinateDescription { get; set; }
    }
}