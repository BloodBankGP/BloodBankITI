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
    
    public partial class Emergency
    {
        [Required(ErrorMessage = "��� ����� �����")]
        public int CID { get; set; }
        [Required(ErrorMessage = "��� ����� �����")]
        public int DayID { get; set; }
        [Required(ErrorMessage = "��� ����� �����")]
        [RegularExpression("null", ErrorMessage = "��� ����� �����")]
        public Nullable<int> HID { get; set; }
    
        public virtual City City { get; set; }
    }
}
