using CultResources;

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;



namespace CULTMACEDONIA_v2.Models
{
    public class ContactFormViewModel
    {

        [Required(ErrorMessageResourceName = "ContactNameRequired", ErrorMessageResourceType = typeof(Shared))]
        public string Name { get; set; }


        [Required(ErrorMessageResourceName = "ContactEmailRequired", ErrorMessageResourceType = typeof(Shared))]
        [DataType(DataType.EmailAddress, ErrorMessageResourceName = "ContactEmailaNotValid", ErrorMessageResourceType = typeof(Shared))]
        public string Email { get; set; }


        [Required(ErrorMessageResourceName = "ContactMessageRequired", ErrorMessageResourceType = typeof(Shared))]
        public string Message { get; set; }
        
    }
}