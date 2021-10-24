using Microsoft.EntityFrameworkCore;
using TwentyTwoSeven.Data.Dto;

namespace TwentyTwoSeven.Data.DataContext
{
    public class RepoContext: DbContext
    {
        #region Constructor

        public RepoContext(DbContextOptions options):base(options)
        {
                    
        }

        #endregion

        #region Properties

        public DbSet<AccountDto> Accounts { get; set; }

        public DbSet<CustomerDto>Customers { get; set; }

        #endregion
    }
}
