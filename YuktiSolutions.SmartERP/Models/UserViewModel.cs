using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using YuktiSolutions.SmartERP.Models.Database;

namespace YuktiSolutions.SmartERP.Models
{
    public class UserViewModel
    {
        /// <summary>
        /// Id of the user.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Name of the user.
        /// </summary>
        [Required]
        [Display(Name = "Username")]
        public string UserName { get; set; }

        /// <summary>
        /// Email of the user.
        /// </summary>
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }


        /// <summary>
        /// Password of the user.
        /// </summary>
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [RegularExpression("^((?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[^a-zA-Z0-9])).{6,}$", ErrorMessage = "Password must be atleast 6 characters, must have one upper case(A-Z),one lower case (a-z), one number(0-9) & one special character (*,&,^,$,#,@)")]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        /// <summary>
        /// Phone number of the user.
        /// </summary>
        [Required]
        [Phone]
        [Display(Name = "Phone Number")]
        public string Number { get; set; }

        /// <summary>
        /// Sets if password of the user to be changed or not.
        /// </summary>
        [Display(Name ="Set New Password")]
        public bool SetPass { get; set; }

        /// <summary>
        /// Saves the user.
        /// </summary>
        public void Save()
        {
            UserManager.SaveUser(this);
        }
    }

    
}