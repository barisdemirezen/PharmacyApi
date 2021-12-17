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
        [Route("get-pharmacy")]
        public IActionResult GetPharmacy()
        {
            return Ok(_pharmacyServices.GetPharmacyDatas());
        }
    }
}

