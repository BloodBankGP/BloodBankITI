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
    
    public partial class Hospital
    {
        public int HID { get; set; }
        public string Name { get; set; }
        public Nullable<int> CID { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public Nullable<int> DayID { get; set; }
    
        public virtual City City { get; set; }
        public virtual Day Day { get; set; }
    }
}
