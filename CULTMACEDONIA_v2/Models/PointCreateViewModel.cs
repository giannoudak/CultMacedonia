using CULTMACEDONIA_v2.Models.CultMacedoniaModel;
using CultResources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;




namespace CULTMACEDONIA_v2.Models
{
    public class PointCreateViewModel
    {

        [Required(ErrorMessageResourceName = "PointNameRequired", ErrorMessageResourceType = typeof(View))]
        [Display(Name="LabelPointName",ResourceType = typeof(View))]
        public string PointName { get; set; }

        [Required(ErrorMessageResourceName = "PointDescrRequired", ErrorMessageResourceType = typeof(View))]
        [Display(Name = "LabelPointDescription", ResourceType = typeof(View))]
        public string PointDescription { get; set; }

        //[DisplayFormat(DataFormatString = "{0:0.##}", ApplyFormatInEditMode = true)]
        public string PointX { get; set; }
       // [DisplayFormat(DataFormatString = "{0:0.##}", ApplyFormatInEditMode = true)]
        public string PointY { get; set; }
        public string PointYear { get; set; }
        public string PointYearDescription { get; set; }
        public string PointPlaceNomos { get; set; }
        public string PointAddress { get; set; }
        public string PointEmail { get; set; }
        public string PointPhone { get; set; }



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



        public string PointWeb { get; set; }
        public string PointLocalization { get; set; }

        public virtual Category Category { get; set; }
        public virtual Era Era { get; set; }
        public virtual Ethnological Ethnological { get; set; }
        public virtual Property Property { get; set; }
        public virtual ProtectionLevel ProtectionLevel { get; set; }
        public virtual Religion Religion { get; set; }

        public string images { get; set; }
    }
}