using System;
using System.Collections.Generic;
using GeoDataAPI.Domain.Interfaces;

namespace GeoDataAPI.Domain
{
    public class LanguageCode : IVersionable
    {
        public string ISO6393 { get; set; }
        public string ISO6392 { get; set; }
        public string ISO6391 { get; set; }
        public string Language { get; set; }
        public byte[] RowId { get; set; }
    }
}
