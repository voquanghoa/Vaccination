using AspDataModel;
using AspDataModel.Models;
using Microsoft.EntityFrameworkCore;

namespace AspBusiness.Businesses
{
    public class GenericBusiness<T>: BaseBusiness where T: IdBase
    {
        protected DbSet<T> Entries { get; }

        public GenericBusiness(DataContext context) : base(context)
        {
            Entries = context.Set<T>();
        }
    }
}