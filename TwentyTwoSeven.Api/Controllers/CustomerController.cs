using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TwentyTwoSeven.Common.RequestModels;

namespace TwentyTwoSeven.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        #region Readonly Fields


        #endregion

        #region Constructor

        public CustomerController()
        {

        }

        #endregion

        #region Public Methods

        [HttpGet("{id}")]
        public async Task<ActionResult<string>> GetById(int id)
        {
            return await Task.FromResult("empty");
        }

        [HttpGet]
        public async Task<ActionResult<string>>GetAll()
        {
            return await Task.FromResult("empty");
        }

        [HttpPost]
        public async Task<ActionResult<bool>>Post(CustomerRequest.V1.Add request)
        {
            return await Task.FromResult(true);
        }

        [HttpPut]
        public async Task<ActionResult<bool>>Update(CustomerRequest.V1.Update request)
        {
            return await Task.FromResult(true);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>>Delete(CustomerRequest.V1.Delete request)
        {
            return await Task.FromResult(true);
        }

        #endregion
    }
}
