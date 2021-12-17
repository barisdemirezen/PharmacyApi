using PharmacyApi.Data.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyApi.Services.Pharmacy
{
    public interface IPharmacyServices
    {
        public IEnumerable<PharmacyData> GetPharmacyByCity(int cityId);
        public IEnumerable<PharmacyData> GetPharmacyByCity(string cityNameShortCode);
        public IEnumerable<PharmacyData> GetPharmacyByDistrict(int districtId);
        public IEnumerable<PharmacyData> GetPharmacyByDistrict(string cityNameShortCode, string districtNameShortCode);
    }
}
