using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CULTMACEDONIA_v2.Models.DTOs
{
    public class PointCreateDto
    {

        public string PointName {get;set;}
        public string PointLocationX {get;set;}
        public string PointLocationY {get;set;}
        public string PointText {get;set;}
        public string PointUser {get;set;}
        public string PointImage {get;set;}

    }
}