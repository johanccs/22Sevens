using System;
using System.Threading.Tasks;
using TwentyTwoSeven.Contracts;
using TwentyTwoSeven.Data.DataContext;
using TwentyTwoSeven.Data.Dto;
using TwentyTwoSeven.Domain.Entities;
using TwentyTwoSeven.Domain.ValueObjects;

namespace TwentyTwoSeven.Services
{
    public class AccountService : RepositoryBase<AccountDto>, IAccountService
    {
        public AccountService(RepoContext context) : base(context)
        {

        }

        public async Task Transfer(TransferObject to)
        {
            TransferObject result = default;

            try
            {
                if (to.IsSaveable)
                {
                    result = to.Transfer();
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                await UpdateAsync(Map(result.SourceAccount));
                await UpdateAsync(Map(result.DestinationAccount));
            }
        }

        #region Private methods

        private static AccountDto Map(AccountEntity entity)
        {
            var accDto = new AccountDto
            {
                AccNumber = entity.GetAccNumber(),
                AccType = entity.GetAccountType(),
                Balance = entity.GetBalance(),
                CustomerId = entity.GetCustomerId(),
                Id = entity.GetId().Id,
                StatusId = entity.GetStatusId()
            };

            return accDto;
        } 

        #endregion
    }
}
