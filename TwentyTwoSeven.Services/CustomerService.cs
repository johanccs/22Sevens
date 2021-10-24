using TwentyTwoSeven.Contracts;
using TwentyTwoSeven.Data.DataContext;
using TwentyTwoSeven.Data.Dto;

namespace TwentyTwoSeven.Services
{
    public class CustomerService:RepositoryBase<CustomerDto>, ICustomerService
    {
        public CustomerService(RepoContext context):base(context)
        {

        }
    }
}
