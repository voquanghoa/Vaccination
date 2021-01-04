using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace AspDataModel
{
    public class DataContextFactory: IDesignTimeDbContextFactory<DataContext>
    {
        public DataContext CreateDbContext(string[] args)
        {
            Console.WriteLine("Load config from appsettings.json");
            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();

            var optionsBuilder = new DbContextOptionsBuilder<DataContext>();

            var sqlConnection = configuration.GetConnectionString("DefaultConnection");
            Console.WriteLine($"Connection string = {sqlConnection}");
            optionsBuilder.UseSqlServer(sqlConnection);

            return new DataContext(optionsBuilder.Options);
        }
    }
}