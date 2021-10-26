using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
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
            var accounts = new List<AccountDto>()
            {
                new AccountDto{AccNumber = "19200001", AccType = 1, StatusId = 1, Balance = 100, CustomerId = "ab01"},
                new AccountDto{AccNumber = "19400001", AccType = 1, StatusId = 1, Balance = 200, CustomerId = "bg01"},
                new AccountDto{AccNumber = "19550001", AccType = 2, StatusId = 1, Balance = 400, CustomerId = "rc01"},
                new AccountDto{AccNumber = "19990001", AccType = 2, StatusId = 1, Balance = 100, CustomerId = "gh01"}
            };

            var customers = new List<CustomerDto>
            {
                new CustomerDto
                {
                    CustId = "ab01",
                    Name = "Arisha Barron",
                    Accounts = new List<AccountDto>
                    {
                        new AccountDto{AccNumber = "19200001", AccType=1, Balance = 100, CustomerId = "ab01", StatusId = 1}
                    }
                },
                new CustomerDto
                {
                    CustId = "bg01",
                    Name = "Branden Gibson",
                    Accounts = new List<AccountDto>
                    {
                        new AccountDto{AccNumber = "19400001", AccType=1, Balance = 200, CustomerId = "bg01", StatusId = 1}
                    }
                },
                new CustomerDto
                {
                    CustId = "rc01",
                    Name = "Rhonda Church",
                    Accounts = new List<AccountDto>
                    {
                        new AccountDto{AccNumber = "19550001", AccType=2, Balance = 400, CustomerId = "rc01", StatusId = 1}
                    }
                },
                new CustomerDto
                {
                    CustId = "gh01",
                    Name = "Georgina Hazel",
                    Accounts = new List<AccountDto>
                    {
                        new AccountDto{AccNumber = "19990001", AccType=2, Balance = 100, CustomerId = "gh01", StatusId = 1}
                    }
                },
            };

            this.Accounts.AddRange(accounts);
            this.Customers.AddRange(customers);
            this.SaveChanges();
        }

        #endregion
    }
}
