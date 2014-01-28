using CULTMACEDONIA_v2.Models.CultMacedoniaModel;
using CultResources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CULTMACEDONIA_v2.Models
{
    public class PointEditViewModel : Point
    {

        public PointEditViewModel()
            : base()
        {

        }

        public bool isEnabled { get; set; }

        [Required(ErrorMessageResourceName = "ProtectionRequired", ErrorMessageResourceType = typeof(View))]
        public int PointProtectionId { get; set; }

        [Required(ErrorMessageResourceName = "CategoryRequired", ErrorMessageResourceType = typeof(View))]
        public int PointCategoryId { get; set; }

        [Required(ErrorMessageResourceName = "EraRequired", ErrorMessageResourceType = typeof(View))]
        public int PointEraId { get; set; }

        [Required(ErrorMessageResourceName = "PropertyRequired", ErrorMessageResourceType = typeof(View))]
        public int PointPropertyId { get; set; }

        [Required(ErrorMessageResourceName = "EthnologicalRequired", ErrorMessageResourceType = typeof(View))]
        public int PointEthnologicalId { get; set; }

        [Required(ErrorMessageResourceName = "ReligionRequired", ErrorMessageResourceType = typeof(View))]
        public int PointReligionId { get; set; }
        
    }
}