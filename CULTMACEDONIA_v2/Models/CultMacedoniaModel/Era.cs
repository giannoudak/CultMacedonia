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
    
    public partial class Era
    {
        public Era()
        {
            this.Point = new HashSet<Point>();
        }
    
        public int EraId { get; set; }
        public string EraName { get; set; }
        public string Lang { get; set; }
    
        public virtual ICollection<Point> Point { get; set; }
    }
}
