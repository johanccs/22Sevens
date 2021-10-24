using System;
using TwentyTwoSeven.Domain.Entities;

namespace TwentyTwoSeven.Domain.ValueObjects
{
    public class TransferObject
    {
        #region Private readonly fields

        private readonly decimal _transferAmount;

        #endregion

        #region Properties
        public AccountEntity SourceAccount { get; private set; }

        public AccountEntity DestinationAccount { get; private set; }

        public bool IsSaveable { get; private set; }

        #endregion

        #region Constructor

        public TransferObject(AccountEntity srcAcc, AccountEntity destAcc, decimal transferAmount)
        {
            SourceAccount = srcAcc;
            DestinationAccount = destAcc;

            _transferAmount = transferAmount;

            IsSaveable = false;

            Validate();
        }

        public TransferObject(AccountEntity srcAcc, AccountEntity destAcc)
        {
            SourceAccount = srcAcc;
            DestinationAccount = destAcc;
        }

        #endregion

        #region Public Methods

        public void Validate()
        {
            try
            {
                if (ValidateTransferAmount())
                    throw new Exception("Transfer amount should be more than R0.00");

                if (ValidateAccounts(SourceAccount))
                    throw new Exception("Please provide a valid source account");

                if (ValidateAccounts(DestinationAccount))
                    throw new Exception("Please provide a valid destination account");

                if (ValidateSufficientFunds())
                    throw new Exception("Source account does not have sufficient funds");

                IsSaveable = true;
            }
            catch (Exception)
            {
                IsSaveable = false;
                throw;
            }
        }

        public TransferObject Transfer()
        {
            try
            {
                SourceAccount.DecreaseBalance(_transferAmount);
                DestinationAccount.IncreaseBalance(_transferAmount);

                return new TransferObject(SourceAccount, DestinationAccount);
            }
            catch (Exception)
            {
                Rollback();
                throw;
            }
        }

        #endregion

        #region Private Methods

        private TransferObject Rollback()
        {
            try
            {
                SourceAccount.IncreaseBalance(_transferAmount);
                DestinationAccount.DecreaseBalance(_transferAmount);

                return new TransferObject(SourceAccount, DestinationAccount);
            }
            catch (Exception)
            {
                throw;
            }
        }
        private bool ValidateTransferAmount()
        {
            return _transferAmount > 0;
        }

        private bool ValidateAccounts(AccountEntity acc)
        {
            return acc != null;
        }

        private bool ValidateSufficientFunds()
        {
            return SourceAccount.GetBalance() > _transferAmount;
        }

        #endregion
    }
}
