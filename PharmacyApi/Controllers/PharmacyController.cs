using Microsoft.AspNetCore.Mvc;
using PharmacyApi.Data.Core;
using PharmacyApi.Services.Pharmacy;

namespace PharmacyApi.Controllers
{
    [ApiController]
    [Route("pharmacy")]
    public class PharmacyController : ControllerBase
    {

        private readonly IPharmacyServices _pharmacyServices;

        public PharmacyController(IPharmacyServices pharmacyServices)
        {
            _pharmacyServices = pharmacyServices;
        }

        [HttpGet]
        [Route("get-pharmacy-by-city-id/{cityId}")]
        public IActionResult GetPharmacyByCityId([FromRoute] int cityId)
        {
            if (cityId.GetType() != typeof(int))
            {
                return BadRequest("Lütfen şehir plaka kodu giriniz");
            }
            try
            {
                var result = _pharmacyServices.GetPharmacyByCity(cityId);
                if (result == null || result.Count() == 0)
                    return NotFound("Bu plaka kodu ile eşleşen şehir bulunamadı.");
                return Ok(result);
            }
            catch
            {
                return StatusCode(500);
            }
        }

        [HttpGet]
        [Route("get-pharmacy-by-city-name/{cityNameShortCode}")]
        public IActionResult GetPharmacyByCityName([FromRoute] string cityNameShortCode)
        {
            try
            {
                var result = _pharmacyServices.GetPharmacyByCity(cityNameShortCode);
                if (result == null || result.Count() == 0)
                    return NotFound("Bu şehir kısa kodu ile eşleşen kayıt bulunamadı.");
                return Ok(result);
            }
            catch
            {
                return StatusCode(500);
            }
        }
    }
}

