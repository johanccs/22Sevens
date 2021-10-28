using System;
using TwentyTwoSeven.Common.Models;
using TwentyTwoSeven.Data.Dto;
using Xunit;

namespace TwentyTwoSeven.Tests.Transfers
{
    public class TransferTests
    {
        [Fact]
        public void TestCorrectTransferAmount()
        {
            var srcAcc = new AccountDto() { AccNumber = "19200001", Balance = 500 };
            var destAcc = new AccountDto() { AccNumber = "19400001", Balance = 300 };

            var to = new TransferObject(srcAcc, destAcc, 140M);
            var result = to.Transfer();

            decimal srcExpectedBalance = 360;
            decimal destExpectedBalance = 440;
            Assert.Equal(srcExpectedBalance, result.SourceAccount.Balance);
            Assert.Equal(destExpectedBalance, result.DestinationAccount.Balance);
        }

        [Fact]
        public void TestReturnObjectType()
        {
            var srcAcc = new AccountDto() { AccNumber = "19200001", Balance = 500 };
            var destAcc = new AccountDto() { AccNumber = "19400001", Balance = 300 };

            var to = new TransferObject(srcAcc, destAcc, 140M);
            var result = to.Transfer();

            Assert.True(result.GetType() == typeof(TransferObject));
        }

        [Fact]
        public void TestSourceAccountValidationFailed()
        {
            try
            {
                var srcAcc = new AccountDto() { AccNumber = "", Balance = 500 };
                var destAcc = new AccountDto() { AccNumber = "19400001", Balance = 300 };

                Assert.Throws<Exception>(() => new TransferObject(srcAcc, destAcc, 140M));
                
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
