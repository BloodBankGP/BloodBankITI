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
    
    public partial class Needer_Donor
    {
        public int NID { get; set; }
        public int BID { get; set; }
        public int CID { get; set; }
        public int DID { get; set; }
        public System.DateTime AskDate { get; set; }
        public bool Accepted { get; set; }
    
        public virtual BloodType BloodType { get; set; }
    }
}
