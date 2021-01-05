using System;
using System.Threading.Tasks;
using AspBusiness.AutoConfig;
using AspBusiness.Exceptions;
using AspBusiness.Models;
using AspBusiness.Models.Patients;
using AspDataModel;
using AspDataModel.Models;
using AspDataModel.Utils;
using Microsoft.EntityFrameworkCore;

namespace AspBusiness.Businesses.Patients
{
    [ImplementBy(typeof(PatientBussiness))]
    public interface IPatientBussiness
    {
        Task<Patient> Login(LoginForm loginForm);

        Task<Patient> Get(Guid qr);

        Task Approval(Guid qr);
    }

    public class PatientBussiness: GenericBusiness<Patient>, IPatientBussiness
    {
        public PatientBussiness(DataContext context) : base(context)
        {
        }

        public async Task<Patient> Login(LoginForm loginForm)
        {
            return await Entries.FirstOrDefaultAsync(x =>
                              (x.Phone == loginForm.Username
                               || x.PersonalId == loginForm.Username
                               || x.Email == loginForm.Username) && x.Password == loginForm.Password.Encode())

                          ?? throw new BadRequestException("Bad user credentication or your account has not been approval");

        }

        public async Task<Patient> Get(Guid qr)
        {
            return (await Entries.FirstOrDefaultAsync(x => x.QRCode == qr));
        }

        public async Task Approval(Guid qr)
        {
            var existing = await Entries.FirstOrDefaultAsync(x => x.QRCode == qr);

            if (existing != null)
            {
                throw new BadRequestException("The profile has already created.");
            }

            var registered = await Context.RegisteredPatients.FirstOrDefaultAsync(x => x.QRCode == qr)
                             ?? throw new BadRequestException("Register profile could not be found.");

            existing = await Entries.FirstOrDefaultAsync(x => x.Email == registered.Email || x.Phone == registered.Phone || x.PersonalId == registered.PersonalId);

            if (existing != null)
            {
                if (existing.PersonalId == registered.PersonalId)
                {
                    throw new BadRequestException($"The personal id {existing.PersonalId} has been registered.");
                }

                if (existing.Email == registered.Email)
                {
                    throw new BadRequestException($"The email {existing.Email} has been registered.");
                }

                throw new BadRequestException($"The phone number {existing.Phone} has been registered.");
            }

            var newPatient = registered.ConvertTo<Patient>();
            newPatient.Id = 0;
            newPatient.ApprovalDate = DateTime.Now;

            AddEntry(newPatient);
            DeleteEntry(registered);

            await SaveChangesAsync();
        }
    }
}