
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
    
public partial class AspNetUsers
{

    public AspNetUsers()
    {

        this.UserFavorites = new HashSet<UserFavorites>();

        this.PointOfUser = new HashSet<PointOfUser>();

    }


    public string Id { get; set; }

    public string UserName { get; set; }

    public string PasswordHash { get; set; }

    public string SecurityStamp { get; set; }

    public string UserEmail { get; set; }

    public string Discriminator { get; set; }



    public virtual ICollection<UserFavorites> UserFavorites { get; set; }

    public virtual ICollection<PointOfUser> PointOfUser { get; set; }

}

}
