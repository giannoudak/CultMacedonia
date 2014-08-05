using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CULTMACEDONIA_v2.Models
{
    public class PointUserFavoriteViewModel
    {
        public int PointId { get; set; }
        public string PointName { get; set; }
        public string OneImageName { get; set; }
        public string OneImagePath { get; set; }
        public string UserId { get; set; }
        public string UserName { get; set; }
    }
}