using System;
using System.Collections.Generic;
using TwentyTwoSeven.Common.Enums;
using TwentyTwoSeven.Data.Dto;

namespace TwentyTwoSeven.Common.Models
{
    public class TransferObject
    {
        #region Private readonly fields

        private readonly decimal _transferAmount;

        #endregion

        #region Properties
        public AccountDto SourceAccount { get; private set; }

        public AccountDto DestinationAccount { get; private set; }

        public DateTime DateTransfered { get; private set; }

        public bool IsSaveable { get; private set; }

        public int Status { get; set; }

        #endregion

        #region Constructor

        public TransferObject(AccountDto srcAcc, AccountDto destAcc, decimal transferAmount)
        {
            SourceAccount = srcAcc;
            DestinationAccount = destAcc;
            DateTransfered = DateTime.Now;

            _transferAmount = transferAmount;

            IsSaveable = false;

            Validate();
        }

        public TransferObject(AccountDto srcAcc, AccountDto destAcc, int status)
        {
            SourceAccount = srcAcc;
            
            DestinationAccount = destAcc;

            Status = status;
        }

        #endregion

        #region Public Methods

        public void Validate()
        {
            try
            {
                if (!ValidateTransferAmount())
                    throw new Exception("Transfer amount should be more than R0.00");

                if (ValidateAccount(SourceAccount))
                    throw new Exception("Please provide a valid source account");

                if (ValidateAccount(DestinationAccount))
                    throw new Exception("Please provide a valid destination account");

                if (!ValidateSufficientFunds())
                    throw new Exception("Source account does not have sufficient funds");

                if (!ValidateTransferDate())
                    throw new Exception("Transfer date cannot be in the past");

                IsSaveable = true;
                Status = AppEnum.TRANSFERSUCCESS;
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
                DecreaseBalance(SourceAccount);
                IncreaseBalance(DestinationAccount);
              
                return new TransferObject(SourceAccount, DestinationAccount, AppEnum.TRANSFERSUCCESS);
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
                IncreaseBalance(SourceAccount);
                DecreaseBalance(DestinationAccount);

                return new TransferObject(SourceAccount, DestinationAccount, AppEnum.TRANSFERFAILED);
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

        private bool ValidateAccount(AccountDto acc)
        {
            var result = string.IsNullOrEmpty(acc.AccNumber);

            return result; 
        }

        private bool ValidateSufficientFunds()
        {
            return SourceAccount.Balance > _transferAmount;
        }

        private bool ValidateTransferDate()
        {
            Decimal currentYearMonthDay = Convert.ToDecimal($"{DateTime.Now.Year}{DateTime.Now.Month}{DateTime.Now.Day}");
            Decimal transferedYearMonthDay = Convert.ToDecimal($"{DateTransfered.Year}{DateTransfered.Month}{DateTransfered.Day}");

            return transferedYearMonthDay - currentYearMonthDay == 0;
        }

        private void IncreaseBalance(AccountDto account)
        {
            account.Balance += _transferAmount;
        }

        private void DecreaseBalance(AccountDto account)
        {
            account.Balance -= _transferAmount;
        }

        #endregion
    }
}
