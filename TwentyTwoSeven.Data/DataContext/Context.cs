using Microsoft.EntityFrameworkCore;
using TwentyTwoSeven.Data.Entities;

namespace TwentyTwoSeven.Data.DataContext
{
    public class Context: DbContext
    {
        #region Constructor

        public Context(DbContextOptions options):base(options)
        {
                    
        }

        #endregion

        #region Properties

        public DbSet<AccountEntity> Accounts { get; set; }

        public DbSet<CustomerEntity>Customers { get; set; }

        #endregion
    }
}
