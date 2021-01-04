using System.Threading.Tasks;
using AspBusiness.AutoConfig;
using AspBusiness.Businesses;
using AspBusiness.Providers;
using AspDataModel.Models;

namespace AspBusiness.Contracts
{
    [ImplementBy(typeof(AuthBusiness))]
    public interface IAuthBusiness
    {
        Task<User> Login(string username);
        Task<User> Login(UserLogin userLogin);
    }
}