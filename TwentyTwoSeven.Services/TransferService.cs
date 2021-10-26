using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TwentyTwoSeven.Contracts;
using TwentyTwoSeven.Data.DataContext;
using TwentyTwoSeven.Data.Entities;

namespace TwentyTwoSeven.Services
{
    public class TransferService : ITransferService
    {
        #region Readonly Fields

        private readonly RepoContext _repoContext;

        #endregion

        #region Constructor

        public TransferService(RepoContext repoContext)
        {
            _repoContext = repoContext;
        }

        #endregion

        #region Methods

        public async Task Create(TransferDto entity)
        {
            await _repoContext.Transfers.AddAsync(entity);
        }

        public async Task<IList<TransferDto>> GetTransfers()
        {
            return await Task.FromResult(_repoContext.Transfers.ToList());
        }

        #endregion
    }
}
