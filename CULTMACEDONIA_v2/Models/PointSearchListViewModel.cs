using CULTMACEDONIA_v2.Models.CultMacedoniaModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CULTMACEDONIA_v2.Models
{
    public class PointSearchListViewModel
    {
        public int PointId { get; set; }
        public string PointName { get; set; }
        public string PointShortDescription { get; set; }
        public string PointAddress { get; set; }
        public decimal? PointX { get; set; }
        public decimal? PointY { get; set; }
        public int PointCategoryId { get; set; }
        public string PointCategory { get; set; }
        public PointImage pointSingleImage { get; set; }

    }
}