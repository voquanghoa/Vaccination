using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AspBusiness.Contracts;
using AspBusiness.Exceptions;
using AspBusiness.Providers;
using AspDataModel;
using AspDataModel.Contracts;
using AspDataModel.Models;
using AspDataModel.Utils;
using Microsoft.EntityFrameworkCore;

namespace AspBusiness.Businesses
{
    public class AuthBusiness: GenericBusiness<User>, IAuthBusiness
    {
        public AuthBusiness(DataContext context) : base(context)
        {
        }

        private async Task<User> Login(Expression<Func<User, bool>> predicate)
        {
            var account = await Entries.FirstOrDefaultAsync(predicate)
                          ?? throw new BadRequestException("Sai tên đăng nhập");

            if (!account.Valid)
            {
                throw new ForbiddenException("Người dùng không hợp lệ.");
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