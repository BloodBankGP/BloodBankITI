//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace BloodBankService.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class NGO
    {
        public int NID { get; set; }
        public string Name { get; set; }
        public int CID { get; set; }
        public string Phone { get; set; }
        public bool Approved { get; set; }
        public bool Status { get; set; }
        public string Address { get; set; }
        public string Fb { get; set; }
        public string Website { get; set; }
    
        public virtual City City { get; set; }
    }
}
