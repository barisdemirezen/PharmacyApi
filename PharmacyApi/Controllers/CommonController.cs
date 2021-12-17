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

        [HttpGet]
        [Route("get-district-list-by-city/{cityId}")]
        public IActionResult GetDistrictListByCity([FromRoute] int cityId)
        {
            if (cityId.GetType() != typeof(int))
            {
                return BadRequest("Lütfen geçerli bir plaka kodu giriniz");
            }
            try
            {
                var result = _commonServices.GetDistrictListByCity(cityId);
                if (result == null || result.Count() == 0)
                    return NotFound("Bu il kodu ile eşleşen ilçe bulunamadı.");
                return Ok(result);
            }
            catch
            {
                return StatusCode(500);
            }
        }
    }
}
