using System;
using System.Threading.Tasks;
using AspBusiness.AutoConfig;
using AspBusiness.Exceptions;
using AspDataModel;
using AspDataModel.Models;
using Microsoft.EntityFrameworkCore;

namespace AspBusiness.Businesses
{
    [ImplementBy(typeof(NurseBusiness))]
    public interface INurseBusiness
    {
        Task SetFirst(Guid qr);
        Task SetSecond(Guid qr);
    }

    public class NurseBusiness: GenericBusiness<Patient>, INurseBusiness
    {
        public NurseBusiness(DataContext context) : base(context)
        {
        }

        private async Task<Patient> GetByQr(Guid qr)
        {
            return await Entries.FirstOrDefaultAsync(x => x.QRCode == qr)
                   ?? throw new NotFoundException($"No patient with QR code {qr} can be found");
        }

        public async Task SetFirst(Guid qr)
        {
            var patient = await GetByQr(qr);
            if (patient.FirstVaccinateDateTime != null)
            {
                throw new BadRequestException("This patient has already finish the first vaccinatation");
            }
            patient.FirstVaccinateDateTime = DateTime.Now;
            UpdateEntry(patient);
            await SaveChangesAsync();
        }

        public async Task SetSecond(Guid qr)
        {
            var patient = await GetByQr(qr);
            if (patient.SecondVaccinateDateTime != null)
            {
                throw new BadRequestException("This patient has already finished the second vaccinatation");
            }

            if (patient.FirstVaccinateDateTime == null)
            {
                throw new BadRequestException("This patient has not been finish the first vaccinatation");
            }

            patient.SecondVaccinateDateTime = DateTime.Now;
            UpdateEntry(patient);
            await SaveChangesAsync();
        }
    }
}