using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AspBusiness.AutoConfig;
using AspBusiness.Exceptions;
using AspBusiness.Models;
using AspBusiness.Providers;
using AspDataModel;
using AspDataModel.Models;
using AspDataModel.Utils;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace AspBusiness.Businesses
{
    [ImplementBy(typeof(AuthBusiness))]
    public interface IAuthBusiness
    {
        Task<User> Login(string username);
        Task<User> Login(UserLogin userLogin);
    }

    public class AuthBusiness: GenericBusiness<User>, IAuthBusiness
    {
        public AuthBusiness(DataContext context) : base(context)
        {
        }

        private async Task<User> Login(Expression<Func<User, bool>> predicate)
        {
            var account = await Entries.FirstOrDefaultAsync(predicate)
                          ?? throw new BadRequestException("Wrong username or password");

            if (!account.Valid)
            {
                throw new ForbiddenException("You account is invalid.");
            }

            return account;
        }

        public async Task<User> Login(string username)
        {
            return await Login(x => x.Username == username);
        }

        public async Task<User> Login(UserLogin userLogin)
        {
            return await Login(x => x.Username == userLogin.Username && x.Password == userLogin.Password.Encode());
        }
    }
}