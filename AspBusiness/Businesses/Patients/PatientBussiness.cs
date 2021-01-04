using System.Threading.Tasks;
using AspBusiness.AutoConfig;
using AspBusiness.Exceptions;
using AspBusiness.Models;
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

                          ?? throw new BadRequestException("Tên đăng nhập hoặc mật khẩu không đúng hoặc tài khoản chưa được chứng thực");

        }
    }
}