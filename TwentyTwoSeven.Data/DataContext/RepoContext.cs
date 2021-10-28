using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using TwentyTwoSeven.Data.Dto;
using TwentyTwoSeven.Data.Entities;

namespace TwentyTwoSeven.Data.DataContext
{
    public class RepoContext: DbContext
    {
        #region Constructor

        public RepoContext(DbContextOptions options):base(options)
        {
            PreAddRecords();
        }

        #endregion

        #region Properties

        public DbSet<AccountDto> Accounts { get; set; }

        public DbSet<CustomerDto>Customers { get; set; }

        public DbSet<TransferDto>Transfers { get; set; }

        #endregion

        #region Methods

        private void PreAddRecords()
        {
           if(this.Customers.Count() > 0 || this.Accounts.Count() > 0)
            {
                return;
            }

            var accounts = new List<AccountDto>()
            {
                new AccountDto{AccNumber = "19200001", AccType = 1, StatusId = 1, Balance = 100},
                new AccountDto{AccNumber = "19400001", AccType = 1, StatusId = 1, Balance = 200},
                new AccountDto{AccNumber = "19550001", AccType = 2, StatusId = 1, Balance = 400},
                new AccountDto{AccNumber = "19990001", AccType = 2, StatusId = 1, Balance = 100}
            };

            var customers = new List<CustomerDto>
            {
                new CustomerDto
                {
                    Id = 1,
                    CustomerNr = "ab01",
                    Name = "Arisha Barron",
                    Accounts = new List<AccountDto>
                    {
                        new AccountDto{AccNumber = "19200001", AccType=1, Balance = 100, StatusId = 1, CustId = 1}
                    }
                },
                new CustomerDto
                {
                    Id = 2,
                    CustomerNr = "bg01",
                    Name = "Branden Gibson",
                    Accounts = new List<AccountDto>
                    {
                        new AccountDto{AccNumber = "19400001", AccType=1, Balance = 200, StatusId = 1, CustId = 2}
                    }
                },
                new CustomerDto
                {
                    Id = 3,
                    CustomerNr = "rc01",
                    Name = "Rhonda Church",
                    Accounts = new List<AccountDto>
                    {
                        new AccountDto{AccNumber = "19550001", AccType=2, Balance = 400, StatusId = 1, CustId = 3}
                    }
                },
                new CustomerDto
                {
                    Id= 4,
                    CustomerNr = "gh01",
                    Name = "Georgina Hazel",
                    Accounts = new List<AccountDto>
                    {
                        new AccountDto{AccNumber = "19990001", AccType=2, Balance = 100, StatusId = 1, CustId = 4}
                    }
                },
            };

            this.Customers.AddRange(customers);
            //this.Accounts.AddRange(accounts);
            this.SaveChanges();
        }

        #endregion
    }
}
