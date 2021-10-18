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
        public async Task<IActionResult> GetById(int id)
        {
            return null;
        }

        [HttpGet]
        public async Task<IActionResult>GetAll()
        {
            return null;
        }

        [HttpPost]
        public async Task<IActionResult>Post(CustomerRequest.V1.Add request)
        {
            return null;
        }

        [HttpPut]
        public async Task<IActionResult>Update(CustomerRequest.V1.Update request)
        {
            return null;
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult>Delete(int id)
        {
            return null;
        }

        #endregion
    }
}
