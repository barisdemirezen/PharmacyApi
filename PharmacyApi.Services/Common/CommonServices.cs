using Newtonsoft.Json;
using PharmacyApi.Data.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyApi.Services.Common
{
    public class CommonServices : ICommonServices
    {
        private readonly List<City> _cityList;
        private readonly List<District> _districtList;
        public CommonServices()
        {
            _cityList = JsonConvert.DeserializeObject<List<City>>(File.ReadAllText(Path.Combine(Directory.GetCurrentDirectory(), "../PharmacyApi.Data/Utils/City.json")));
            _districtList = JsonConvert.DeserializeObject<List<District>>(File.ReadAllText(Path.Combine(Directory.GetCurrentDirectory(), "../PharmacyApi.Data/Utils/District.json")));
        }

        public IEnumerable<City> GetCityList()
        {
            return _cityList;
        }
        public IEnumerable<District> GetDistrictListByCity(int cityId)
        {
            return _districtList.Where(e => e.CityId == cityId);
        }
        public string GetCityNameCode(int cityId)
        {
            City city = _cityList.FirstOrDefault(e => e.Id == cityId);
            if (city == null)
                return String.Empty;
            return city.NameCode;
        }
        public string GetDistrictNameCode(int districtId)
        {
            District district = _districtList.FirstOrDefault(e => e.Id == districtId);
            if (district == null)
                return String.Empty;
            return district.NameCode;
        }
        public string GetCityNameCodeByDistrictId(int districtId)
        {
            District district = _districtList.FirstOrDefault(e => e.Id == districtId);
            if (district == null)
                return String.Empty;

            City city = _cityList.FirstOrDefault(e => e.Id == district.CityId);
            if (city == null)
                return String.Empty;

            return city.NameCode;
        }

        
    }
}


