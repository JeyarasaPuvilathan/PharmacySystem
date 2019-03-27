using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AdminLTE_Template1.Models
{
    public class CustomAdminLogin
    {
        public int ID { get; set; }
        public string UserName { get; set; }
        [Required(ErrorMessage = "The password Required.")]
        //[RegularExpression(@"^(?=.*[A-Z])(?=.*[a-z])(?=.*[0-9]).{6,20}$", ErrorMessage = "Passwords must use be contain at least 6 character and at least one character of  lowercase letters, uppercase letters and numbers.")]
        public string Password { get; set; }
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
        public Nullable<System.DateTime> DateOfBirth { get; set; }
        public Nullable<int> MobileNo { get; set; }
        [Required(ErrorMessage = "The Email Required.")]
        [RegularExpression(@"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$", ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; }
        public string Image { get; set; }
        public System.DateTime CreatedOn { get; set; }
    }
}