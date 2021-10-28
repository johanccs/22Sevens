using System.Threading.Tasks;

namespace TwentyTwoSeven.Contracts
{
    public interface IRepoWrapper
    {
        IAccountService Account { get; }
        ICustomerService Customer { get; }
        ITransferService Transfer { get; }
        Task SaveAsync();
    }
}
