using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TwentyTwoSeven.Common.RequestModels;
using TwentyTwoSeven.Contracts;
using TwentyTwoSeven.Data.Dto;

namespace TwentyTwoSeven.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        #region Readonly Fields

        private readonly IRepoWrapper _repoWrapper;
        private readonly ILoggerManager _logger;

        #endregion

        #region Constructor

        public CustomerController(IRepoWrapper repoWrapper, ILoggerManager logger)
        {
            _repoWrapper = repoWrapper;
            _logger = logger;
        }

        #endregion

        #region Public Methods

        [HttpGet("{id}")]
        public async Task<ActionResult<string>> GetById(CustomerRequest.V1.GetById request)
        {
            try
            {
                var result = await GetCustomerById(request.Id);

                if (result == null)
                {
                    return NotFound($"Customer with Id {request.Id} not found");
                }

                _logger.LogInfo($"{result.CustId} found");
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public async Task<ActionResult<string>>GetAll()
        {
            try
            {
                var result = await GetAllCustomers();

                if (result == null)
                {
                    return NotFound("No customers found");
                }

                _logger.LogInfo($"{result.Count} record/s found");
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult<bool>>Post(CustomerRequest.V1.Add request, string accNr, int accType)
        {
            try
            {
                await CreateCustomerAsync(request, accNr, accType);

                return Ok("Success");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        public async Task<ActionResult<string>>Update(CustomerRequest.V1.Update request)
        {
            try
            {
               await UpdateCustomerAsync(request);

                return Ok("Success");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<string>>Delete(CustomerRequest.V1.Delete request)
        {
            try
            {
                await DeleteCustomerAsync(request);

                return Ok("Success");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        [Route("Transfer/{srcAccNr}/{destAccNr}/{transferAmt}")]
        public async Task<ActionResult<string>>Transfer(AccountRequest.V1.Transfer transfer, decimal transferAmount)
        {
            try
            {
                await InternalTransfer(transfer, transferAmount);

                return Ok("Success");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);                
            }
        }

        #endregion

        #region Private Methods

        private async Task<IList<CustomerDto>>GetAllCustomers()
        {
            var result = await _repoWrapper.Customer.FindAllAsync();

            if (result.Count() == 0)
                return null;
            else
                return result.ToList();
        }

        private async Task<CustomerDto> GetCustomerById(int id)
        {
            var result = await _repoWrapper.Customer.FindByConditionAsync(x => x.Id == id);

            if (result == null)
                return null;
            else
                return (CustomerDto)result;
        }

        private async Task CreateCustomerAsync(CustomerRequest.V1.Add custRequest, 
                                               string accNr, int accType)
        {
            var accRequest = new AccountRequest.V1.Add
            {
                AccNumber = accNr,
                AccType = accType,
                Balance = 130,
                CustomerId = custRequest.CustId,
                StatusId = 1
            };

            await _repoWrapper.Account.CreateAsync(AccountRequest.V1.Add.Map(accRequest));
            await _repoWrapper.Customer.CreateAsync(CustomerRequest.V1.Add.Map(custRequest));

            await _repoWrapper.SaveAsync();
        }

        private async Task UpdateCustomerAsync(CustomerRequest.V1.Update request)
        {
            await _repoWrapper.Customer.UpdateAsync(CustomerRequest.V1.Update.Map(request));

            await _repoWrapper.SaveAsync();
        }

        private async Task DeleteCustomerAsync(CustomerRequest.V1.Delete request)
        {
            var customerToBeDeleted = await GetCustomerById(request.Id);

            var result = customerToBeDeleted.Accounts.Select(x => x.Balance > 0) as CustomerDto;
        }

        private async Task InternalTransfer(AccountRequest.V1.Transfer transfer, decimal transferAmount)
        {
            await GetAccountByCustByAccNr(transfer);
        }

        private async Task GetAccountByCustByAccNr(AccountRequest.V1.Transfer transfer)
        {
            var srcAcc = await _repoWrapper
                 .Customer
                 .FindByConditionAsync(
                         x => x.Accounts.Select(
                         x => x.AccNumber == transfer.SourceAccNumber).FirstOrDefault());
            var res = srcAcc.ToList();
        }

        #endregion
    }
}
