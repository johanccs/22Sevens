using System.Collections.Generic;
using System.Threading.Tasks;
using TwentyTwoSeven.Data.Entities;

namespace TwentyTwoSeven.Contracts
{
    public interface ITransferService
    {
        Task Create(TransferDto entity);
        Task<IList<TransferDto>> GetTransfers();        
    }
}
