using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CULTMACEDONIA_v2.Models
{


    public class PointMapViewModel
    {
        public string PlaceName { get; set; }
        public decimal? GeoLat { get; set; }
        public decimal? GeoLong { get; set; }
        public string PlaceAddress { get; set; }

        public string Category { get; set; }
        public int PlaceId { get; set; }
        // ...
        // ...
    }
}