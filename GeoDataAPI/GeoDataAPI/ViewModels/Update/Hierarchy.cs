using System;
using System.Collections.Generic;
using GeoDataAPI.Domain.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace GeoDataAPI.Domain.ViewModels.Update
{
    public class Hierarchy : IVersionable
    {
        [Required(ErrorMessageResourceName = "Hierarchy_ParentId_Required",
                  ErrorMessageResourceType = typeof(GeoDataAPI.ErrorMessages.ErrorMessages_US_en))]
        public long ParentId { get; set; }

        [Required(ErrorMessageResourceName = "Hierarchy_ChildId_Required",
                  ErrorMessageResourceType = typeof(GeoDataAPI.ErrorMessages.ErrorMessages_US_en))]
        public long ChildId { get; set; }

         [StringLength(50, MinimumLength = 1, ErrorMessageResourceName = "Length_Error",
                ErrorMessageResourceType = typeof(GeoDataAPI.ErrorMessages.ErrorMessages_US_en))]
        public string Type { get; set; }

        [Required(ErrorMessageResourceName = "Hierarchy_RowId_Required",
                  ErrorMessageResourceType = typeof(GeoDataAPI.ErrorMessages.ErrorMessages_US_en))]
        public byte[] RowId { get; set; }
    }
}
