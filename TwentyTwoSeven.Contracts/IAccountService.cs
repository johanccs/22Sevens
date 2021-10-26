using System.Threading.Tasks;
using TwentyTwoSeven.Common.Models;
using TwentyTwoSeven.Data.Dto;

namespace TwentyTwoSeven.Contracts
{
    public interface IAccountService:IRepositoryBase<AccountDto>
    {
        Task Transfer(TransferObject to);
        Task<decimal> GetBalance(string accNr);
    }
}
