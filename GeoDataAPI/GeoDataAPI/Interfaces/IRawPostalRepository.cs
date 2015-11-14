using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Upd_VM = GeoDataAPI.Domain.ViewModels.Update;
using Ins_VM = GeoDataAPI.Domain.ViewModels.Insert;

namespace GeoDataAPI.Domain.Interfaces
{
    public interface IRawPostalRepository
    {
        IEnumerable<RawPostal> GetPostalInfo(string isoCountryCode = null, string countryName = null, string postalCode = null, int? pageNumber = null, int? pageSize = null);

        IEnumerable<RawPostal> UpdatePostalInfo(IEnumerable<Upd_VM.RawPostal> postalInfo);

        IEnumerable<RawPostal> InsertPostalInfo(IEnumerable<Ins_VM.RawPostal> postalInfo);

        int DeletePostalInfo(RawPostal postalInfo);
    }
}
