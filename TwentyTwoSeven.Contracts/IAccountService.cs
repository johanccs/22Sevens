using System.Threading.Tasks;
using TwentyTwoSeven.Data.Dto;
using TwentyTwoSeven.Domain.ValueObjects;

namespace TwentyTwoSeven.Contracts
{
    public interface IAccountService:IRepositoryBase<AccountDto>
    {
        Task Transfer(TransferObject to);
    }
}
