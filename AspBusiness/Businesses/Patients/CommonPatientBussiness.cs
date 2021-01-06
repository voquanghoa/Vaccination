using System;
using System.Threading.Tasks;
using AspBusiness.AutoConfig;
using AspBusiness.Exceptions;
using AspBusiness.Models;

namespace AspBusiness.Businesses.Patients
{
    [ImplementBy(typeof(CommonPatientBussiness))]
    public interface ICommonPatientBussiness
    {
        Task<T> Get<T>(Guid qr);

        Task<Guid> Login(LoginForm loginForm);
    }

    public class CommonPatientBussiness: ICommonPatientBussiness
    {
        private readonly IRegisteredPatientBussiness registeredPatientBussiness;
        private readonly IPatientBussiness patientBussiness;

        public CommonPatientBussiness(IRegisteredPatientBussiness registeredPatientBussiness, IPatientBussiness patientBussiness)
        {
            this.registeredPatientBussiness = registeredPatientBussiness;
            this.patientBussiness = patientBussiness;
        }

        public async Task<T> Get<T>(Guid qr)
        {
            var profile = await patientBussiness.Get(qr);

            if (profile != null)
            {
                return profile.ConvertTo<T>();
            }

            var profile2 = await registeredPatientBussiness.Get(qr);

            if (profile2 != null)
            {
                return profile2.ConvertTo<T>();
            }

            throw new NotFoundException("No patient found");
        }

        public async Task<Guid> Login(LoginForm loginForm)
        {
            return (await patientBussiness.Login(loginForm)) ?? (await registeredPatientBussiness.Login(loginForm))
                ?? throw new BadRequestException("Bad credentical");
        }
    }
}