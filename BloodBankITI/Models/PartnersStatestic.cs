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
    
    public partial class PartnersStatestic
    {
        public int SID { get; set; }
        public Nullable<int> BID { get; set; }
        public Nullable<System.DateTime> Insert_Date { get; set; }
        public int PID { get; set; }
        public int DID { get; set; }
    
        public virtual Donor Donor { get; set; }
        public virtual Partner Partner { get; set; }
        public virtual BloodType BloodType { get; set; }
    }
}
