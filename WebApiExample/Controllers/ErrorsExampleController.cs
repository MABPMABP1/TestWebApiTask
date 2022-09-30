using Microsoft.AspNetCore.Mvc;

namespace WebApiExample.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    /// <summary>
    /// Test controller. The only purpose is endpoint for demonstrating common exceptions handling implementation
    /// </summary>
    public class ErrorsExampleController : ControllerBase
    {
        [HttpGet]
        /// <summary>
        /// Throws an exception
        /// </summary>
        public IActionResult RaiseError()
        {
            throw new Exception("Just server exception");
        }
    }
}
