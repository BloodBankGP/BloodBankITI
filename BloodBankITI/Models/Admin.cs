//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace BloodBankITI.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    
    public partial class Admin
    {
        [Required(ErrorMessage="��� ����� �����")]
        public int AID { get; set; }
        [Required(ErrorMessage = "��� ����� �����")]
        public string Fname { get; set; }
        [Required(ErrorMessage = "��� ����� �����")]
        public string Lname { get; set; }
        [Required(ErrorMessage = "��� ����� �����")]
        public bool Status { get; set; }
        public string Picture { get; set; }
    }
}
