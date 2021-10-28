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

                _logger.LogInfo($"{result.CustomerNr} found");
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
        public async Task<ActionResult<CustomerDto>>Post(CustomerRequest.V1.Add request)
        {
            try
            {
                var result = await CreateCustomerAsync(request);

                return Ok($"Id: {result}");
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
                _logger.LogError(ex.Message);
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

        private async Task<int> CreateCustomerAsync(CustomerRequest.V1.Add custRequest)
        {
            await _repoWrapper.Customer.CreateAsync(CustomerRequest.V1.Add.Map(custRequest));

            await _repoWrapper.SaveAsync();

            return await GetCreatedCustomerId(custRequest);
        }

        private async Task<int>GetCreatedCustomerId(CustomerRequest.V1.Add request)
        {
            var returnQuery = await _repoWrapper.Customer
                               .FindByConditionAsync(x => x.CustomerNr.Equals(request.CustId));

            var cust = returnQuery.Select(x => x.Id).ToList();

            if (cust == null || cust.Count() == 0)
                throw new ArgumentException("Create customer procedure failed", nameof(cust));

            return cust[0];
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

        #endregion
    }
}
