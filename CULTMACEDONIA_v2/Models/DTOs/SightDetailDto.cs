using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CULTMACEDONIA_v2.Models.DTOs
{
    public class SightDetailDto
    {
        public string sightId {get;set;}
        public string sightDescription {get;set;}
        public string sightAddress{get;set;}
        public string sightPlace {get;set;}

        public string sightCategory { get; set; }
        public string sightProtecion { get; set; }
        public string sightEra { get; set; }
        public string sightProperty { get; set; }
        public string sightEthnology { get; set; }
        public string sightReligion { get; set; }


    }
}