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
    
    public partial class Partner
    {
        public Partner()
        {
            this.PartnersStatestics = new HashSet<PartnersStatestic>();
            this.Donors = new HashSet<Donor>();
        }

        [Required]
        public int PAID { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Address { get; set; }
        public Nullable<bool> Status { get; set; }
        [Required]
        public Nullable<int> CID { get; set; }
    
        public virtual ICollection<PartnersStatestic> PartnersStatestics { get; set; }
        public virtual City City { get; set; }
        public virtual ICollection<Donor> Donors { get; set; }
    }
}
