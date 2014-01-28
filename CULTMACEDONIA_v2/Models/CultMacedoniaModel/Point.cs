//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CULTMACEDONIA_v2.Models.CultMacedoniaModel
{
    using System;
    using System.Collections.Generic;
    
    public partial class Point
    {
        public Point()
        {
            this.UserFavorites = new HashSet<UserFavorites>();
            this.PointImage = new HashSet<PointImage>();
            this.PointOfUser = new HashSet<PointOfUser>();
            this.PointVideo = new HashSet<PointVideo>();
        }
    
        public int PointId { get; set; }
        public string PointName { get; set; }
        public string PointDescription { get; set; }
        public Nullable<decimal> PointX { get; set; }
        public Nullable<decimal> PointY { get; set; }
        public Nullable<int> PointYear { get; set; }
        public string PointPlaceNomos { get; set; }
        public string PointAddress { get; set; }
        public string PointEmail { get; set; }
        public string PointPhone { get; set; }
        public int PointProtectionId { get; set; }
        public int PointCategoryId { get; set; }
        public int PointEraId { get; set; }
        public int PointPropertyId { get; set; }
        public int PointEthnologicalId { get; set; }
        public int PointReligionId { get; set; }
        public string PointWeb { get; set; }
        public string PointLocalization { get; set; }
    
        public virtual ICollection<UserFavorites> UserFavorites { get; set; }
        public virtual ICollection<PointImage> PointImage { get; set; }
        public virtual Category Category { get; set; }
        public virtual Era Era { get; set; }
        public virtual Ethnological Ethnological { get; set; }
        public virtual ICollection<PointOfUser> PointOfUser { get; set; }
        public virtual Property Property { get; set; }
        public virtual ProtectionLevel ProtectionLevel { get; set; }
        public virtual Religion Religion { get; set; }
        public virtual ICollection<PointVideo> PointVideo { get; set; }
    }
}
