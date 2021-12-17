using PharmacyApi.Data.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyApi.Services.Common
{
    public interface ICommonServices
    {
        public IEnumerable<City> GetCityList();
        public IEnumerable<District> GetDistrictListByCity(int cityId);
        public string GetCityNameCode(int cityId);
        public string GetDistrictNameCode(int districtId);
        public string GetCityNameCodeByDistrictId(int districtId);
    }
}
