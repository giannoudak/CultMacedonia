using CULTMACEDONIA_v2.Models.CultMacedoniaModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CULTMACEDONIA_v2.Models
{
    public class PointFullViewModel : Point
    {
    
        public PointFullViewModel()
            : base(){

        }
        public bool isUserFavorite { get; set; }
    }
}