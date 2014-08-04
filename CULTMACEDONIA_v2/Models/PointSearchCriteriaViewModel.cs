using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CULTMACEDONIA_v2.Models
{
    public class PointSearchCriteriaViewModel
    {
        public string title { get; set; }
        public int? catId { get; set; }
        public int? eraId { get; set; }
        public int? ethnId { get; set; }
        public string year {get;set;}
        public char yearWhen {get;set;}

    }
}