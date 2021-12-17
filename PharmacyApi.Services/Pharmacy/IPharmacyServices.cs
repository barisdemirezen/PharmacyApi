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
        public IEnumerable<PharmacyData> GetPharmacyDatas();
    }
}
