using CULTMACEDONIA_v2.Models.CultMacedoniaModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CULTMACEDONIA_v2.Models
{


    public class PointGridViewModel
    {

        public int PointId { get; set; }
        public string PlaceName { get; set; }
        public int? year { get; set; }
        public string PlaceAddress { get; set; }
        public string PlaceNomos { get; set; }

        public string OneImageName { get; set; }
        public string OneImagePath { get; set; }


        public string category { get; set; }
        public string protection { get; set; }
        public string property { get; set; }
        public string ethnological { get; set; }
        public string religion { get; set; }
        public string era { get; set; }






        public int PointProtectionId { get; set; }
        public int PointCategoryId { get; set; }
        public int PointEraId { get; set; }
        public int PointPropertyId { get; set; }
        public int PointEthnologicalId { get; set; }
        public int PointReligionId { get; set; }


        // properties για activation info!
        public byte isActivate { get; set; }
        public DateTime? dateActivated { get; set; }
        public DateTime? dateAdded { get; set; }
        public string UserName { get; set; }
        

    }
}