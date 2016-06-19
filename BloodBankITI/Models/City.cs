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
    
    public partial class City
    {
        public City()
        {
            this.Donors = new HashSet<Donor>();
            this.Emergencies = new HashSet<Emergency>();
            this.Hospitals = new HashSet<Hospital>();
            this.Locations = new HashSet<Location>();
            this.Needers = new HashSet<Needer>();
            this.NGOes = new HashSet<NGO>();
            this.Posts = new HashSet<Post>();
            this.Partners = new HashSet<Partner>();
            this.Needer_Donor = new HashSet<Needer_Donor>();
        }
    
        public int CID { get; set; }
        public string CityName { get; set; }
        public string Logo { get; set; }
    
        public virtual ICollection<Donor> Donors { get; set; }
        public virtual ICollection<Emergency> Emergencies { get; set; }
        public virtual ICollection<Hospital> Hospitals { get; set; }
        public virtual ICollection<Location> Locations { get; set; }
        public virtual ICollection<Needer> Needers { get; set; }
        public virtual ICollection<NGO> NGOes { get; set; }
        public virtual ICollection<Post> Posts { get; set; }
        public virtual ICollection<Partner> Partners { get; set; }
        public virtual ICollection<Needer_Donor> Needer_Donor { get; set; }
    }
}
