//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace AdminLTE_Template1.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class AdminLogin
    {
        public int ID { get; set; }
        [Required(ErrorMessage = "The UserName Required.")]
        [RegularExpression(@"^(?:.*[a-z]){4,}$", ErrorMessage = "UserName  must be greater than or equal 4 characters.")]
        public string UserName { get; set; }
        public string Password { get; set; }
        [Required(ErrorMessage = "The DateOfBirth Required.")]
        public Nullable<System.DateTime> DateOfBirth { get; set; }
        [Required(ErrorMessage = "The MobileNo Required.")]
        [RegularExpression(@"^\d{9,10}$", ErrorMessage = "MobileNo should be 9 or 10 digits.")]
        public string MobileNo { get; set; }
        [Required(ErrorMessage = "The Email Required.")]
        [RegularExpression(@"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$", ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; }
        public string Image { get; set; }
        public System.DateTime CreatedOn { get; set; }
        public Nullable<bool> IsBlocked { get; set; }
    }
}
