using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TwentyTwoSeven.Common.RequestModels;

namespace TwentyTwoSeven.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        #region Readonly Fields


        #endregion

        #region Constructor

        public AccountController()
        {

        }

        #endregion

        #region Public Methods

        [HttpGet("{id}")]
        public async Task<ActionResult<string>> GetById(AccountRequest.V1.GetById id)
        {
            return await Task.FromResult("Empty");
        }

        [HttpGet]
        public async Task<ActionResult<string>> GetAll()
        {
            return await Task.FromResult("empty");
        }

        [HttpPost]
        public async Task<ActionResult<bool>> Post(AccountRequest.V1.Add request)
        {
            return await Task.FromResult(true);
        }

        [HttpPut]
        public async Task<ActionResult<bool>> Update(AccountRequest.V1.Update request)
        {
            return await Task.FromResult(true);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> Delete(int id)
        {
            return await Task.FromResult(true);
        }

        #endregion
    }
}
