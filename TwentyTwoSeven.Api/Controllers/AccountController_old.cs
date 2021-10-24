using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TwentyTwoSeven.Common.CustomExceptions;
using TwentyTwoSeven.Common.RequestModels;
using TwentyTwoSeven.Contracts;
using TwentyTwoSeven.Data.Dto;

namespace TwentyTwoSeven.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController_old : ControllerBase
    {
        #region Readonly Fields

        private readonly IRepoWrapper _repoWrapper;

        #endregion

        #region Constructor

        public AccountController_old(IRepoWrapper repoWrapper)
        {
            _repoWrapper = repoWrapper;
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

                return Ok("Success");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<string>> Delete(AccountRequest.V1.Delete request)
        {
            try
            {
                await DeleteAccountAsync(request);

                return Ok("Success");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        #endregion

        #region Private Methods

        private async Task<IList<AccountDto>>GetAllAccounts()
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

        private async Task UpdateEntity(AccountRequest.V1.Update request)
        {
            await _repoWrapper.Account.UpdateAsync(
                AccountRequest.V1.Update.Map(request));

            await _repoWrapper.SaveAsync();
        }

        private async Task CreateEntity(AccountRequest.V1.Add request)
        {
            await _repoWrapper.Account.CreateAsync(
                AccountRequest.V1.Add.Map(request));

            await _repoWrapper.SaveAsync();
        }

        private async Task DeleteAccountAsync(AccountRequest.V1.Delete request)
        {
            var entity = await GetAccountById(request.Id);

            if (entity == null)
                throw new EntityNotFoundException("Entity not found. Delete procedure aborted");
           
            await _repoWrapper.Account.DeleteAsync(entity);
        }

        #endregion
    }
}
