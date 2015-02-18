using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeoDataAPI.Domain.Interfaces
{
    public interface IRawPostalRepository
    {
        IEnumerable<RawPostal> GetPostalInfo(string isoCountryCode = null, string countryName = null, string postalCode = null, int? pageNumber = null, int? pageSize = null);        
    }
}
