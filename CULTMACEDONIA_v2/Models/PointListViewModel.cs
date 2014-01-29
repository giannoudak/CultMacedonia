using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CULTMACEDONIA_v2.Models
{
    public class PointListViewModel
    {
        public int PointId { get; set; }
        public string PointName { get; set; }
        public string PointAddress { get; set; }
        public decimal? PointX { get; set; }
        public decimal? PointY { get; set; }

        public string PointCategory { get; set; }
        public List<PointImageViewModel> pointImages { get; set; }

    }

    public class PointImageViewModel
    {
        public int ImageId { get; set; }
        public string ImageName { get; set; }
        public string ImagePath { get; set; }


    }
}