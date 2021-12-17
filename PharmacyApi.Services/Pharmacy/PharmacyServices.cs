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
        public PharmacyServices(ICommonServices commonServices)
        {
            _commonServices = commonServices;
        }

        public IEnumerable<PharmacyData> GetPharmacyByCity(int cityId)
        {
            string cityNameShortCode = _commonServices.GetCityNameCode(cityId);
            if (cityNameShortCode == null)
                return Enumerable.Empty<PharmacyData>();

            return GetPharmacyByCityCode(cityNameShortCode);
        }

        public IEnumerable<PharmacyData> GetPharmacyByCity(string cityNameShortCode)
        {
            return GetPharmacyByCityCode(cityNameShortCode);
        }

        private IEnumerable<PharmacyData> GetPharmacyByCityCode(string cityNameShortCode)
        {
            var url = $"https://www.eczaneler.gen.tr/nobetci-{cityNameShortCode}";
            var web = new HtmlWeb();
            var doc = web.Load(url);

            if(web.StatusCode == System.Net.HttpStatusCode.NotFound)
                return Enumerable.Empty<PharmacyData>();

            HtmlNodeCollection items = doc.DocumentNode.SelectNodes("//div[@id='nav-bugun']//tr");
            items.RemoveAt(0);
            List<PharmacyData> result = new();

            foreach (var item in items)
            {
                string name = item.SelectSingleNode(".//td/div/div[1]/a/span").InnerText;
                string address = item.SelectSingleNode(".//td/div/div[2]").InnerText;
                string district = item.SelectSingleNode(".//td/div/div[2]/div/span[1]").InnerText;
                string contact = item.SelectSingleNode(".//td/div/div[3]").InnerText;
                result.Add(new PharmacyData
                {
                    Address = address,
                    ContactNumber = contact,
                    District = district,
                    Name = name,
                });
            }

            return result;
        }
    }
}
