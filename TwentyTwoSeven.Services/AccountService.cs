using System;
using System.Linq;
using System.Threading.Tasks;
using TwentyTwoSeven.Common.Models;
using TwentyTwoSeven.Contracts;
using TwentyTwoSeven.Data.DataContext;
using TwentyTwoSeven.Data.Dto;

namespace TwentyTwoSeven.Services
{
    public class AccountService : RepositoryBase<AccountDto>, IAccountService
    {
        public AccountService(RepoContext context) : base(context)
        {
           
        }

        public async Task<decimal> GetBalance(string accNr)
        {
            var result = await this.FindAllbyList();

            if (result.Count == 0)
                throw new ArgumentException("No accounts registered");

            var balance = result.FirstOrDefault(x=>x.AccNumber.Equals(accNr)).Balance;

            return balance;
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
                await UpdateAsync(result.SourceAccount);
                await UpdateAsync(result.DestinationAccount);
            }
        }
    }
}
