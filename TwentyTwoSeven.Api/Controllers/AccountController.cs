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
        public async Task<IActionResult> GetById(int id)
        {
            return null;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return null;
        }

        [HttpPost]
        public async Task<IActionResult> Post(AccountRequest.V1.Add request)
        {
            return null;
        }

        [HttpPut]
        public async Task<IActionResult> Update(AccountRequest.V1.Update request)
        {
            return null;
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            return null;
        }

        #endregion
    }
}
