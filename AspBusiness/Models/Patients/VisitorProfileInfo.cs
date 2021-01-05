using System;
using AspBusiness.AutoConfig;
using AspDataModel.Models;

namespace AspBusiness.Models.Patients
{
    [ConvertFrom(typeof(Patient))]
    [ConvertFrom(typeof(RegisteredPatient))]
    public class VisitorProfileInfo
    {
        public string FullName { get; set; }

        public string PersonalId { get; set; }

        public Guid QRCode { get; set; }

        public string ProfileUrl { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime? ApprovalDate { get; set; }

        public bool Valid { get; set; }

        public DateTime? RemindFirstVaccinateDateTime { get; set; }
        public DateTime? FirstVaccinateDateTime { get; set; }
        public string FirstVaccinateDescription { get; set; }

        public DateTime? RemindSecondVaccinateDateTime { get; set; }
        public DateTime? SecondVaccinateDateTime { get; set; }
        public string SecondVaccinateDescription { get; set; }

        public ProfileStatus Status
        {
            get
            {
                if (Valid == false)
                {
                    return ProfileStatus.Registered;
                }

                if (FirstVaccinateDateTime == null)
                {
                    return ProfileStatus.Approval;
                }

                if (SecondVaccinateDateTime == null)
                {
                    return ProfileStatus.FinishFirstTime;
                }

                return ProfileStatus.Done;
            }

        }
    }
}