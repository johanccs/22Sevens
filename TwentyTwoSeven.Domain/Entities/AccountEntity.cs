using System;
using TwentyTwoSeven.Domain.ValueObjects;

namespace TwentyTwoSeven.Domain.Entities
{
    public class AccountEntity
    {
        #region Fields

        public AccountId _id;

        public string _accNumber;
        
        public int _accType;
        
        public int _statusId;
        
        public decimal _balance;

        public int _customerId;

        #endregion

        #region Constructor

        public AccountEntity(AccountId id)
        {
            _id = id;

            _balance = 100;

            _statusId = 1;
        }

        #endregion

        #region Methods

        public void SetAccNumber(string accNumber)
        {
            if (string.IsNullOrEmpty(accNumber))
                throw new ArgumentNullException(
                    nameof(accNumber), 
                    "Account number must have one or more characters");

            _accNumber = accNumber;
        }

        public string GetAccNumber()
        {
            return _accNumber;
        }

        public void SetAccountType(int accType)
        {
            if (accType < 0)
                throw new ArgumentNullException(
                    nameof(accType), "Account type must be valid");

            _accType = accType;
        }

        public int GetAccountType()
        {
            //0 Savings Acc | 1 Checque Acc | 2 Credit Card
            return _accType;
        }

        public void SetStatusId(int statusId)
        {
            if (statusId < 0)
                throw new ArgumentException(nameof(statusId), "Status must have a positive value");

            _statusId = statusId;
        }

        public int GetStatusId()
        {
            return _statusId;
        }

        public void SetBalance(decimal balance)
        {
            if (_accType == 0 && balance < 0)
                throw new ArgumentException(
                    nameof(balance), 
                    "Balance for a savings account cannot be less than 0.00");

            _balance = balance;
        }

        public decimal GetBalance()
        {
            return _balance;
        }

        public void IncreaseBalance(decimal transferAmt)
        {
            this._balance += transferAmt;
        }

        public void DecreaseBalance(decimal transferAmt)
        {
            this._balance -= transferAmt;
        }
        
        public void SetCustomerId(int customerId)
        {
            if (customerId < 0)
                throw new ArgumentException(
                    nameof(customerId), 
                    "Customer number must be larger than 0");

            _customerId = customerId;
        }

        public int GetCustomerId()
        {
            return _customerId;
        }

        public AccountId GetId()
        {
            return _id;
        }

        #endregion
    }
}
