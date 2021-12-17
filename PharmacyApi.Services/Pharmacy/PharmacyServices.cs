using HtmlAgilityPack;
using PharmacyApi.Data.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyApi.Services.Pharmacy
{
    public class PharmacyServices : IPharmacyServices
    {
        public IEnumerable<PharmacyData> GetPharmacyDatas()
        {
            var url = "https://www.eczaneler.gen.tr/nobetci-izmir";
            var web = new HtmlWeb();
            var doc = web.Load(url);

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
