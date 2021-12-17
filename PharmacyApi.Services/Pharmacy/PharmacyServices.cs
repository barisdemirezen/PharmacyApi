using HtmlAgilityPack;
using PharmacyApi.Data.Core;
using PharmacyApi.Services.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyApi.Services.Pharmacy
{
    public class PharmacyServices : IPharmacyServices
    {
        private readonly ICommonServices _commonServices;
        const string baseUrl = "https://www.eczaneler.gen.tr/nobetci";

        public PharmacyServices(ICommonServices commonServices)
        {
            _commonServices = commonServices;
        }

        public IEnumerable<PharmacyData> GetPharmacyByCity(int cityId)
        {
            string cityNameShortCode = _commonServices.GetCityNameCode(cityId);
            if (cityNameShortCode == null)
                return Enumerable.Empty<PharmacyData>();

            string scrapingUrl = GetScrapingUrl(cityNameShortCode);

            return GetPharmacy(scrapingUrl);
        }

        public IEnumerable<PharmacyData> GetPharmacyByDistrict(int districtId)
        {
            string districtNameShortCode = _commonServices.GetDistrictNameCode(districtId);
            if (districtNameShortCode == null)
                return Enumerable.Empty<PharmacyData>();

            string cityNameShortCode = _commonServices.GetCityNameCodeByDistrictId(districtId);
            if (cityNameShortCode == null)
                return Enumerable.Empty<PharmacyData>();

            string scrapingUrl = GetScrapingUrl(cityNameShortCode, districtNameShortCode);
            return GetPharmacy(scrapingUrl);
        }

        public IEnumerable<PharmacyData> GetPharmacyByDistrict(string cityNameShortCode, string districtNameShortCode)
        {
            string scrapingUrl = GetScrapingUrl(cityNameShortCode, districtNameShortCode);
            return GetPharmacy(scrapingUrl);
        }

        public IEnumerable<PharmacyData> GetPharmacyByCity(string cityNameShortCode)
        {
            string scrapingUrl = GetScrapingUrl(cityNameShortCode);
            return GetPharmacy(scrapingUrl);
        }

        private string GetScrapingUrl(string cityNameShortCode)
        {
            return $"{baseUrl}-{cityNameShortCode}";
        }
        private string GetScrapingUrl(string cityNameShortCode, string districtNameShortCode)
        {
            return $"{baseUrl}-{cityNameShortCode}-{districtNameShortCode}";
        }

        private IEnumerable<PharmacyData> GetPharmacy(string scrapingUrl)
        {
            var url = scrapingUrl;
            var web = new HtmlWeb();
            var doc = web.Load(url);

            string[] splittedAddress;
            string name, commonAddress, address, district, contact, description = String.Empty;

            if (web.StatusCode == System.Net.HttpStatusCode.NotFound)
                return Enumerable.Empty<PharmacyData>();

            HtmlNodeCollection items = doc.DocumentNode.SelectNodes("//div[@id='nav-bugun']//tr");
            items.RemoveAt(0);
            List<PharmacyData> result = new();

            foreach (var item in items)
            {
                name = item.SelectSingleNode(".//td/div/div[1]/a/span")?.InnerText;

                if (name == null)
                    name = item.SelectSingleNode(".//td/div/div[1]/span[1]")?.InnerText;

                commonAddress = item.SelectSingleNode(".//td/div/div[2]")?.InnerText;
                address = commonAddress;
                if (commonAddress != null && commonAddress.Contains('('))
                {
                    splittedAddress = commonAddress.Split('(');
                    address = splittedAddress[0];
                    description = splittedAddress[1].Replace(")", String.Empty);
                }

                district = item.SelectSingleNode(".//td/div/div[2]/div/span[1]")?.InnerText;
                contact = item.SelectSingleNode(".//td/div/div[3]")?.InnerText;

                result.Add(new PharmacyData
                {
                    Address = address,
                    ContactNumber = contact,
                    District = district,
                    Name = name,
                    AddressDescription = description,
                });
            }

            return result;
        }

    }
}
