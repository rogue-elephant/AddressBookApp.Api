using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace AddressBookApp.DataAccess.Tests
{
    public class DataAccessTestBase
    {
        protected AddressBookDataContext dbContext;
        public DataAccessTestBase(AddressBookDataContext dbContext = null) => this.dbContext = dbContext ?? GetInMemoryDBContext();

        protected AddressBookDataContext GetInMemoryDBContext()
        {
            var serviceProvider = new ServiceCollection()
                .AddEntityFrameworkInMemoryDatabase()
                .BuildServiceProvider();

            var builder = new DbContextOptionsBuilder<AddressBookDataContext>();
            var options = builder.UseInMemoryDatabase("dev_db").UseInternalServiceProvider(serviceProvider).Options;

            AddressBookDataContext dbContext = new AddressBookDataContext(options);
            dbContext.Database.EnsureDeleted();
            dbContext.Database.EnsureCreated();
            return dbContext;
        }
    }
}