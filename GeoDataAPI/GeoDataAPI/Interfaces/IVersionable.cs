using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeoDataAPI.Domain.Interfaces
{
    interface IVersionable
    {
        byte[] RowId { get; set; }
    }
}
