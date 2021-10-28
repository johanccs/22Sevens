using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using TwentyTwoSeven.Contracts.Mocks;
using TwentyTwoSeven.Data.DataContext;
using TwentyTwoSeven.Data.Dto;

namespace TwentyTwoSeven.Services.Mocks
{
    public class MockCustomerService : IMockRepoBase<CustomerDto>
    {
        #region Private Readonly Fields

        private MockRepoContext _repoContext;

        #endregion

        #region Constructor

        public MockCustomerService(MockRepoContext repoContext)
        {
            _repoContext = repoContext;
        }

        #endregion

        #region Methods

        public async Task CreateAsync(CustomerDto entity)
        {
            await _repoContext.InsertCustomer(entity);
        }

        public async Task DeleteAsync(CustomerDto entity)
        {
            await _repoContext.DeleteCustomer(entity);
        }

        public async Task<IQueryable<CustomerDto>> FindAllAsync()
        {
            return await _repoContext.GetAllCustomers();
        }

        public async Task<IList<CustomerDto>> FindAllbyList()
        {
            return await _repoContext.GetAllCustomersByList();
        }

        public Task<IQueryable<CustomerDto>> FindByConditionAsync(
            Expression<Func<CustomerDto, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(CustomerDto entity)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
