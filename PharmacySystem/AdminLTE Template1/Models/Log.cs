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
    
    public partial class Log
    {
        public int Id { get; set; }
        public System.DateTime EventDateTime { get; set; }
        public string EventLevel { get; set; }
        public string UserName { get; set; }
        public string MachineName { get; set; }
        public string EventMessage { get; set; }
        public string ErrorSource { get; set; }
        public string ErrorClass { get; set; }
        public string ErrorMethod { get; set; }
        public string ErrorMessage { get; set; }
        public string InnerErrorMessage { get; set; }
    }
}
