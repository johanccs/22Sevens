using System.Threading.Tasks;
using TwentyTwoSeven.Contracts;
using TwentyTwoSeven.Data.DataContext;

namespace TwentyTwoSeven.Services
{
    public class RepoWrapper : IRepoWrapper
    {
        private RepoContext _repoCtx;
        private IAccountService _account;
        private ICustomerService _customer;
        private ITransferService _transfer;

        #region Properties

        public IAccountService Account
        {
            get
            {
                if (_account == null)
                {
                    _account = new AccountService(_repoCtx);
                }

                return _account;
            }
        }

        public ICustomerService Customer
        {
            get
            {
                if(_customer == null)
                {
                    _customer = new CustomerService(_repoCtx);
                }

                return _customer;
            }
        }

        public ITransferService Transfer
        {
            get
            {
                if (_transfer == null)
                {
                    _transfer = new TransferService(_repoCtx);
                }

                return _transfer;
            }
        }

        #endregion

        #region Constructor

        public RepoWrapper(RepoContext repoCtx)
        {
            _repoCtx = repoCtx;
        }

        #endregion

        #region Methods

        public async Task SaveAsync()
        {
            await _repoCtx.SaveChangesAsync();
        }

        #endregion
    }
}
