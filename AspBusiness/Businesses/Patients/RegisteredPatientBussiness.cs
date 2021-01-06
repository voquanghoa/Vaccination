using System;
using System.Threading.Tasks;
using AspBusiness.AutoConfig;
using AspBusiness.Models;
using AspBusiness.Models.Patients;
using AspDataModel;
using AspDataModel.Models;
using AspDataModel.Utils;
using Microsoft.EntityFrameworkCore;

namespace AspBusiness.Businesses.Patients
{
    [ImplementBy(typeof(RegisteredPatientBussiness))]
    public interface IRegisteredPatientBussiness
    {
        Task<RegisteredPatient> Create(ProfileCreate profileCreate);

        Task<RegisteredPatient> Get(Guid qr);

        Task<Guid?> Login(LoginForm loginForm);
    }

    public class RegisteredPatientBussiness: GenericBusiness<RegisteredPatient>, IRegisteredPatientBussiness
    {
        public async Task<RegisteredPatient> Create(ProfileCreate profileCreate)
        {
            var registeredPatient = profileCreate.ConvertTo<RegisteredPatient>();

            registeredPatient.Password = registeredPatient.Password.Encode();
            registeredPatient.QRCode = Guid.NewGuid();
            registeredPatient.CreatedDate = DateTime.Now;

            AddEntry(registeredPatient);
            await SaveChangesAsync();

            return registeredPatient;
        }

        public async Task<Guid?> Login(LoginForm loginForm)
        {
            return (await Entries.FirstOrDefaultAsync(x =>
                (x.Phone == loginForm.Username || x.PersonalId == loginForm.Username || x.Email == loginForm.Username)
                && x.Password == loginForm.Password.Encode()))?.QRCode;

        }

        public async Task<RegisteredPatient> Get(Guid qr)
        {
            return await Entries.FirstOrDefaultAsync(x => x.QRCode == qr);
        }

        public RegisteredPatientBussiness(DataContext context) : base(context)
        {
        }
    }
}