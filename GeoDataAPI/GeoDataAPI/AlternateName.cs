using System;
using System.Collections.Generic;
using GeoDataAPI.Domain.Interfaces;

namespace GeoDataAPI.Domain
{
    public class AlternateName : IVersionable
    {
        public int AlternateNameId { get; set; }
        public string ISO6393LanguageCode { get; set; }
        public string AlternateName1 { get; set; }
        public bool? IsPreferredName { get; set; }
        public bool? IsShortName { get; set; }
        public bool? IsColloquial { get; set; }
        public bool? IsHistoric { get; set; }
        public byte[] RowId { get; set; }
    }
}
