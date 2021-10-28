using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using TwentyTwoSeven.Data.Dto;

namespace TwentyTwoSeven.Data.DataContext
{
    public class MockRepoContext
    {
        private static IList<CustomerDto> _customers;

        private static IList<AccountDto> _accounts;

        public MockRepoContext()
        {
            _customers = new List<CustomerDto>();

            _accounts = new List<AccountDto>();
        }

        #region Customer Mock Methods

        public async Task<CustomerDto> GetCustomerById(string id)
        {
            var cust = _customers.FirstOrDefault(x => x.CustomerNr == id);

            return await Task.FromResult(cust);
        }

        public async Task<IQueryable<CustomerDto>> GetAllCustomers()
        {
            return await Task.FromResult(_customers.AsQueryable());
        }

        public async Task<IList<CustomerDto>> GetAllCustomersByList()
        {
            return await Task.FromResult(_customers);
        }

        public async Task<IQueryable<CustomerDto>> FindCustomerByConditionAsync(Expression<Func<CustomerDto, bool>> expression)
        {
            //var result = _customers.Where(expression).AsQueryable();

            return await Task.FromResult(new List<CustomerDto>().AsQueryable());
        }

        public async Task<CustomerDto> InsertCustomer(CustomerDto entity)
        {
            if (_customers == null)
                _customers = new List<CustomerDto>();

            if (_customers.Count == 0)
                entity.Id = 1;
            else
            {
                var maxId = _customers.Max(x => x.Id);
                entity.Id = maxId++;
            }

            _customers.Add(entity);

            return await Task.FromResult(await GetCustomerById(entity.CustomerNr));
        }

        public async Task<CustomerDto> UpdateCustomer(CustomerDto entity)
        {
            if (_customers == null)
                throw new Exception("Customer List Empty");

            var foundCust = await GetCustomerById(entity.CustomerNr);

            if (foundCust == null)
                throw new ArgumentNullException(nameof(entity.CustomerNr), "Customer not found");

            foundCust.Accounts.Clear();
            foundCust.Accounts = entity.Accounts;
            foundCust.Name = entity.Name;

            return await Task.FromResult(await GetCustomerById(entity.CustomerNr)); 
        }

        public async Task<bool> DeleteCustomer(CustomerDto entity)
        {
            if (_customers == null)
                throw new ArgumentException("Customer List empty");

            if(_customers.Any(x=>x.CustomerNr.Equals(entity.CustomerNr)))
            {
                _customers.Remove(entity);
            }

            return await Task.FromResult(_customers.Any(x=>x.CustomerNr.Equals(entity.CustomerNr)));
        }

        #endregion
    }
}
