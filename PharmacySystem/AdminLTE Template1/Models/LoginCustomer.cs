using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AdminLTE_Template1.Models
{
    public class LoginCustomer
    {
        //[Display(Name = "Email ID")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Email ID required")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password required")]
        //[DataType(DataType.Password)]
        public string Password { get; set; }

        //[Display(Name = "Remember Me")]
        public bool RememberMe { get; set; }
    }
}