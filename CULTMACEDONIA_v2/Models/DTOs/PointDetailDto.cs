using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace CULTMACEDONIA_v2.Models.DTOs
{
    public class PointDetailDto
    {
        public int PointId {get;set;}
        public string PointName { get; set; }
        public decimal? PointX { get; set; }
        public decimal? PointY { get; set; }
        public int? PointYear { get; set; }

        [DefaultValue("----")]
        public string PointPlaceNomos { get; set; }
        public string PointDescription {get;set;}
        public string PointFoto { get; set; }
        public string PointProtectionId { get; set; }
        public string PointCategoryId { get; set; }
        public string PointEraId { get; set; }
        public string PointPropertyId { get; set; }
        public string PointEthnologicalId { get; set; }
        public string PointReligionId { get; set; }
        public string PointVideo { get; set; }

        public string PointAddress{get;set;}
        public string PointPhone { get; set; }
        public string PointEmail { get; set; }

        public string PointWeb { get; set; }
        public string PointUser { get; set; }

        public string PointLocalization { get; set; }
        

        
    }
}