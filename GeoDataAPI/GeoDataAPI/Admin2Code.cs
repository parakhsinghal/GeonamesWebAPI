using System;
using System.Collections.Generic;
using GeoDataAPI.Domain.Interfaces;

namespace GeoDataAPI.Domain
{
    public class Admin2Code : IVersionable
    {
        public string Admin2CodeId { get; set; }
        public string Admin2CodeName { get; set; }
        public string ASCIIName { get; set; }
        public byte[] RowId { get; set; }
    }
}
