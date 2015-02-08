using System;
using System.Collections.Generic;
using GeoDataAPI.Domain.Interfaces;

namespace GeoDataAPI.Domain
{
    public class Hierarchy : IVersionable
    {
        public long ParentId { get; set; }
        public long ChildId { get; set; }
        public string Type { get; set; }
        public byte[] RowId { get; set; }
    }
}
