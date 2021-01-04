using System.Threading.Tasks;
using AspBusiness.AutoConfig;
using AspBusiness.Models.Patients;
using AspDataModel;
using AspDataModel.Models;

namespace AspBusiness.Businesses.Patients
{
    [ImplementBy(typeof(RegisteredPatientBussiness))]
    public interface IRegisteredPatientBussiness
    {
        Task<RegisteredPatient> Create(ProfileCreate profileCreate);
    }

    public class RegisteredPatientBussiness: GenericBusiness<RegisteredPatient>, IRegisteredPatientBussiness
    {
        public async Task<RegisteredPatient> Create(ProfileCreate profileCreate)
        {
            var registeredPatient = profileCreate.ConvertTo<RegisteredPatient>();
            AddEntry(registeredPatient);
            await SaveChangesAsync();

            return registeredPatient;
        }

        public RegisteredPatientBussiness(DataContext context) : base(context)
        {
        }
    }
}