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
    
    public partial class Property
    {
        public Property()
        {
            this.Point = new HashSet<Point>();
        }
    
        public int PropertyId { get; set; }
        public string PropertyName { get; set; }
        public string Lang { get; set; }
    
        public virtual ICollection<Point> Point { get; set; }
    }
}
