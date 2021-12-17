using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PharmacyApi.Services.Common;

namespace PharmacyApi.Controllers
{
    [Route("common")]
    [ApiController]
    public class CommonController : ControllerBase
    {
        private readonly ICommonServices _commonServices;

        public CommonController(ICommonServices commonServices)
        {
            _commonServices = commonServices;
        }

        [HttpGet]
        [Route("get-city-list")]
        public IActionResult GetCityList()
        {
            return Ok(_commonServices.GetCityList());
        }
    }
}
