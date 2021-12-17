using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyApi.Data.Common
{
    public class District
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? NameCode { get; set; }
        public int CityId { get; set; }
    }
}
