using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TwentyTwoSeven.Contracts.Mocks;
using TwentyTwoSeven.Data.DataContext;
using TwentyTwoSeven.Data.Dto;
using TwentyTwoSeven.Services.Mocks;
using Xunit;

namespace TwentyTwoSeven.Tests
{
    public class CustomerTest
    {
        #region

        private readonly IMockRepoBase<CustomerDto> _customerContext;
        private readonly MockRepoContext _repoContext;

        #endregion

        #region Constructor

        public CustomerTest()
        {
            _repoContext = new MockRepoContext();
            _customerContext = new MockCustomerService(_repoContext);
        }

        #endregion

        public async Task Setup()
        {
            var cust = new CustomerDto() { CustomerNr = "jp0123", Name = "J. Potgieter" };

            await _customerContext.CreateAsync(cust);

            cust = new CustomerDto() { CustomerNr = "cm001", Name = "J. Smith" };
        }

        [Fact]
        public async Task AddCustomerToMockDatabase_DbNotEmpty()
        {
            var cust = new CustomerDto() { CustomerNr = "jp0123", Name = "J. Potgieter" };

            await _customerContext.CreateAsync(cust);

            var custCount = await _customerContext.FindAllAsync();

            Assert.True(custCount.ToList().Count > 0);
        }

        //Valid Id
        [Fact]
        public async Task AddCustomerToMockDatabase_ValidId()
        {
            var cust = new CustomerDto() { CustomerNr = "jp0123", Name = "J. Potgieter" };

            await _customerContext.CreateAsync(cust);

            var customers = await _customerContext.FindAllAsync();
            var customer = customers.ToList().FirstOrDefault(x => x.CustomerNr.Equals(cust.CustomerNr));

            Assert.True(!string.IsNullOrEmpty(customer.CustomerNr));
        }

        [Fact]
        public async Task DeleteCustomerFromMockDatabase()
        {
            await Setup();
            await _customerContext.DeleteAsync(
                new CustomerDto { CustomerNr = "jp0123", Name = "J. Potgieter" });

            var customers = await _customerContext.FindAllAsync();
            var customer = customers.ToList().FirstOrDefault(x => x.CustomerNr.Equals("jp0123"));

            Assert.True(customer == null);
        }
    }
}
