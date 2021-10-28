using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using TwentyTwoSeven.Common.CustomExceptions;
using TwentyTwoSeven.Common.Enums;
using TwentyTwoSeven.Common.Models;
using TwentyTwoSeven.Common.RequestModels;
using TwentyTwoSeven.Contracts;
using TwentyTwoSeven.Data.Dto;
using TwentyTwoSeven.Data.Entities;

namespace TwentyTwoSeven.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        #region Readonly Fields

        private readonly IRepoWrapper _repoWrapper;
        private readonly ILoggerManager _logger;

        #endregion

        #region Constructor

        public AccountController(IRepoWrapper repoWrapper, ILoggerManager logger)
        {
            _repoWrapper = repoWrapper;
            _logger = logger;
        }

        #endregion

        #region Public Methods

        [HttpGet("{id}")]
        public async Task<ActionResult<string>> GetById(AccountRequest.V1.GetById request)
        {
            try
            {
                var result = await GetAccountById(request.Id);

                if (result == null)
                {
                    return NotFound($"Account with Id {request.Id} not found");
                }

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public async Task<ActionResult<IList<AccountDto>>> GetAll()
        {
            try
            {
                var result = await GetAllAccounts();

                if (result == null)
                {
                    return NotFound("No accounts found");
                }

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult<string>> Post(AccountRequest.V1.Add request)
        {
            try
            {
                await CreateEntity(request);
                await _repoWrapper.SaveAsync();

                return Ok("Success");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        public async Task<ActionResult<string>> Update(AccountRequest.V1.Update request)
        {
            try
            {
                await UpdateEntity(request);
                await _repoWrapper.SaveAsync();

                return Ok("Success");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        [Route("Close/{accountNr}")]
        public async Task<ActionResult<string>> Close(AccountRequest.V1.CloseAccount accountNr)
        {
            try
            {
                await DeactivateAccountAsync(accountNr);

                return Ok("Success");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet()]
        [Route("GetBalance/{id}")]
        public async Task<ActionResult<string>> GetBalance(string id)
        {
            try
            {
               var balance = await _repoWrapper.Account.GetBalance(id);
               var nfi = (NumberFormatInfo)CultureInfo.InvariantCulture.NumberFormat.Clone();
               var result = balance.ToString("#,0.00", nfi);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        [Route("Transfer/{srcAccNr}/{destAccNr}/{transferAmt}")]
        public async Task<ActionResult<string>> Transfer(AccountRequest.V1.Transfer transfer)
        {
            try
            {
                var result = await InternalTransfer(transfer);

                if (result.Status == AppEnum.TRANSFERSUCCESS)
                {
                    return Ok("Success");
                }
                else
                {
                    _logger.LogError($"Transfer failed from {transfer.SourceAccNumber} to {transfer.DestinationAccNumber}");
                    return BadRequest($"Transfer failed from {transfer.SourceAccNumber} to {transfer.DestinationAccNumber}");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("GetTransfers")]
        public async Task<ActionResult<List<TransferDto>>>GetTransfers()
        {
            try
            {
                var result = await GetInternalTransfers();

                if (result.Count == 0)
                    return NotFound("No transfers were found");

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        #endregion

        #region Private Methods

        private async Task<IList<TransferDto>>GetInternalTransfers()
        {
           return await _repoWrapper.Transfer.GetTransfers();
        }

        private async Task<IList<AccountDto>> GetAllAccounts()
        {
            var result = await _repoWrapper.Account.FindAllAsync();

            if (result.Count() == 0)
                return null;
            else
                return result.ToList();
        }

        private async Task<AccountDto> GetAccountById(int id)
        {
            var result = await _repoWrapper.Account.FindByConditionAsync(x => x.Id == id);

            if (result == null)
                return null;
            else
                return (AccountDto)result;
        }

        private async Task<AccountDto> GetAccountByAccNr(string accNr)
        {
            var result = await _repoWrapper.Account.FindByConditionAsync(x => x.AccNumber.Equals(accNr));
            var accountList = result.ToList();

            if (accountList.Count == 0)
                return null;
            else
            {
                return accountList[0];
            }
        }

        private async Task UpdateEntity(AccountRequest.V1.Update request)
        {
            await _repoWrapper.Account.UpdateAsync(
                AccountRequest.V1.Update.Map(request));
        }

        private async Task UpdateEntity(AccountDto account)
        {
            await _repoWrapper.Account.UpdateAsync(account);
        }

        private async Task CreateEntity(AccountRequest.V1.Add request)
        {
            var customerTask = await _repoWrapper.Customer.FindByConditionAsync(x => x.CustomerNr.Equals(request.CustomerId));
            var customerList = customerTask?.ToList();
            
            if(customerList.Count() == 0)
            {
               throw new ArgumentException("Please add customer first", nameof(request.CustomerId));
            }

            await _repoWrapper.Account.CreateAsync(
                AccountRequest.V1.Add.Map(request));
        }

        private async Task DeactivateAccountAsync(AccountRequest.V1.CloseAccount request)
        {
            var entity = await GetAccountByAccNr(request.AccountNr);

            if (entity == null)
                throw new EntityNotFoundException("Entity not found. Delete procedure aborted");

            entity.StatusId = AccountStatus.Inactive;

            await _repoWrapper.Account.UpdateAsync(entity);
            //await _repoWrapper.SaveAsync();
        }

        private async Task<TransferObject> InternalTransfer(AccountRequest.V1.Transfer transfer)
        {
            //1. Get source Account
            var sourceAccount = await GetAccountEntityByAccNr(transfer.SourceAccNumber);
            var destinationAccount = await GetAccountEntityByAccNr(transfer.DestinationAccNumber);

            ValidateStatus(sourceAccount, destinationAccount);

            var to = new TransferObject(sourceAccount, destinationAccount, transfer.TransferAmount);

            var result = to.Transfer();

            await SaveTransfer(transfer, to);
            await UpdateEntity(to.SourceAccount);
            await UpdateEntity(to.DestinationAccount);

            await _repoWrapper.SaveAsync();

            return await Task.FromResult(result);
        }

        private async Task SaveTransfer(AccountRequest.V1.Transfer transfer, TransferObject to)
        {
            await _repoWrapper.Transfer.Create(
                new Data.Entities.TransferDto
                {
                    DateTransfered = DateTime.Now,
                    SourceAccount = to.SourceAccount.AccNumber,
                    DestinationAccount = to.DestinationAccount.AccNumber,
                    TransferAmount = transfer.TransferAmount
                });
        }

        private void ValidateStatus(AccountDto sourceAccount, AccountDto destinationAccount)
        {
            if (CheckAccountStatus(sourceAccount)) throw new ArgumentException("Account is inactive", nameof(sourceAccount));
            if (CheckAccountStatus(destinationAccount)) throw new ArgumentException("Account is inactive", nameof(destinationAccount));
        }

        private bool CheckAccountStatus(AccountDto acc)
        {
            return acc.StatusId == AccountStatus.Inactive;            
        }

        private async Task<AccountDto> GetAccountEntityByAccNr(string accNr)
        {
            var srcAccTaskResult = await _repoWrapper
                 .Account.FindByConditionAsync(x => x.AccNumber.Equals(accNr));

            var srcAccList = srcAccTaskResult.ToList();

            if (srcAccList.Count > 0)
                return srcAccList[0];
            else
                return null;
        }

        #endregion
    }
}
