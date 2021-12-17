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
        public CommonServices()
        {
            _cityList = JsonConvert.DeserializeObject<List<City>>(File.ReadAllText(Path.Combine(Directory.GetCurrentDirectory(), "../PharmacyApi.Data/Utils/City.json")));
        }

        public IEnumerable<City> GetCityList()
        {
            return _cityList;
        }
        public string GetCityNameCode(int cityId)
        {
            City city = _cityList.FirstOrDefault(e => e.Id == cityId);
            if (city == null)
                return String.Empty;
            return city.NameCode;
        }
    }
}


