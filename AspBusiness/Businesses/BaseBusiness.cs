using System.Threading.Tasks;
using AspDataModel;
using Microsoft.EntityFrameworkCore;

namespace AspBusiness.Businesses
{
    public abstract class BaseBusiness
    {
        protected DataContext Context { get; }

        protected BaseBusiness(DataContext context)
        {
            Context = context;
        }

        protected void AddEntry<T>(T entry)
        {
            Context.Entry(entry).State = EntityState.Added;
        }

        protected void UpdateEntry<T>(T entry)
        {
            Context.Entry(entry).State = EntityState.Modified;
        }

        protected void DeleteEntry<T>(T entry)
        {
            Context.Entry(entry).State = EntityState.Deleted;
        }

        protected async Task SaveChangesAsync()
        {
            await Context.SaveChangesAsync();
        }
    }
}