﻿using CultResources;
using System.ComponentModel.DataAnnotations;

namespace CULTMACEDONIA_v2.Models
{
    public class ExternalLoginConfirmationViewModel
    {
        [Required]
        [Display(Name = "User name")]
        public string UserName { get; set; }

        [Required]
        public string UserEmail { get; set; }
    }

    public class ManageUserViewModel
    {
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Current password")]
        public string OldPassword { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "New password")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm new password")]
        [Compare("NewPassword", ErrorMessage = "The new password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }

    public class LoginViewModel
    {
        [Required(ErrorMessageResourceName = "UserNameRequired", ErrorMessageResourceType = typeof(Admin))]
        [Display(Name = "User name")]
        public string UserName { get; set; }

        
        [Required(ErrorMessageResourceName = "PasswordRequired", ErrorMessageResourceType = typeof(Admin))]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }
    }

    public class RegisterViewModel
    {
        [Required(ErrorMessageResourceName = "UserNameRequired", ErrorMessageResourceType = typeof(Admin))]
        [Display(Name = "User name")]
        public string UserName { get; set; }

        [Required(ErrorMessageResourceName = "PasswordRequired", ErrorMessageResourceType = typeof(Admin))]
        [DataType(DataType.Password)]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessageResourceName = "PasswordConfirmFail", ErrorMessageResourceType = typeof(Admin))]
        public string ConfirmPassword { get; set; }

        /// <summary>
        /// Προσθέτουμε και το email του χρήστη για το Registration
        /// To email ειναι υποχρεωτικό και δεν επιτρέπεται το κενό string
        /// </summary>
        [Required(ErrorMessageResourceName = "EmailRequired", ErrorMessageResourceType = typeof(Admin), AllowEmptyStrings = false)]
        public string UserEmail { get; set; }
    }
}
