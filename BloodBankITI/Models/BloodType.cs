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
    
    public partial class BloodType
    {
        public BloodType()
        {
            this.Donors = new HashSet<Donor>();
            this.Needers = new HashSet<Needer>();
            this.PartnersStatestics = new HashSet<PartnersStatestic>();
            this.Posts = new HashSet<Post>();
            this.PartnersStatestics1 = new HashSet<PartnersStatestic>();
            this.Needer_Donor = new HashSet<Needer_Donor>();
        }
    
        public int BID { get; set; }
        public string Type { get; set; }
        public string Logo { get; set; }
    
        public virtual ICollection<Donor> Donors { get; set; }
        public virtual ICollection<Needer> Needers { get; set; }
        public virtual ICollection<PartnersStatestic> PartnersStatestics { get; set; }
        public virtual ICollection<Post> Posts { get; set; }
        public virtual ICollection<PartnersStatestic> PartnersStatestics1 { get; set; }
        public virtual ICollection<Needer_Donor> Needer_Donor { get; set; }
    }
}
