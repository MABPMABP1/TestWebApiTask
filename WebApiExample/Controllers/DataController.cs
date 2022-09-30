using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApiExample.Filters;
using WebApiExample.Requests.Data;
using WebApiExample.Responses.Data;
using WebApiExample.Services.DataServices;

namespace WebApiExample.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [TokenAuthenticationFilter]
    public class DataController : ControllerBase
    {
        private readonly IDataService _dataService;

        public DataController(IDataService dataService)
        {
            _dataService = dataService;
        }

        [HttpPost("getData")]
        public async Task<IActionResult> GetData(DataRequest request)
        {
            Task dataLoadingTask1 = _dataService.GetData(request.Delay1);
            Task dataLoadingTask2 = _dataService.GetData(request.Delay2);
            await Task.WhenAll(dataLoadingTask1, dataLoadingTask2);

            return Ok(new DataResponse(request.Delay1, request.Delay2));
        }
    }
}
