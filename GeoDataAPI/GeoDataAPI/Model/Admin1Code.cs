using System;
using System.Collections.Generic;
using GeoDataAPI.Domain.Interfaces;

namespace GeoDataAPI.Domain
{
    public class Admin1Code:IVersionable
    {
        public string Admin1CodeId { get; set; }
        public string Admin1CodeName { get; set; }
        public string ASCIIName { get; set; }
        public byte[] RowId { get; set; }
    }
}
