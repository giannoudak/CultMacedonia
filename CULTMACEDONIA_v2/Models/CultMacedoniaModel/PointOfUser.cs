
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
    
public partial class PointOfUser
{

    public int PointId { get; set; }

    public byte isActivated { get; set; }

    public System.DateTime DateAdded { get; set; }

    public Nullable<System.DateTime> DateActivated { get; set; }

    public string Id { get; set; }

    public int PointOfUserId { get; set; }



    public virtual AspNetUsers AspNetUsers { get; set; }

    public virtual Point Point { get; set; }

}

}
