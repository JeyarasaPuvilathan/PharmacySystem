using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AdminLTE_Template1.Models
{
    public class MailModel
    {
        [Required(ErrorMessage = "Enter Email Id")]
        [RegularExpression(@"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$", ErrorMessage = "Invalid Email Address")]
        public string To { get; set; }
        public string Subject { get; set; }
        [Required(ErrorMessage = " You cant leave blank")]
        public string Body { get; set; }
    }
}