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
    
    public partial class Donor
    {
        public Donor()
        {
            this.PartnersStatestics = new HashSet<PartnersStatestic>();
            this.Needer_Donor = new HashSet<Needer_Donor>();
        }

        [Required(ErrorMessage = "��� ����� �����")]
        public int DID { get; set; }
        [Required(ErrorMessage = "��� ����� �����")]
        public string Fname { get; set; }
        [Required(ErrorMessage = "��� ����� �����")]
        public string Lname { get; set; }
        [Required(ErrorMessage = "��� ����� �����")]
        public string DonorGender { get; set; }
        [Required(ErrorMessage = "��� ����� �����")]
        [Phone(ErrorMessage="�� ���� ���� ��� ���� ����")]
        public string Phone { get; set; }
        public Nullable<int> BID { get; set; }
        [Required(ErrorMessage = "��� ����� �����")]
        public int CID { get; set; }
        [Required(ErrorMessage = "��� ����� �����")]
        public int LID { get; set; }
        public bool Status { get; set; }
        public bool Pending { get; set; }
        public Nullable<System.DateTime> DonationDate { get; set; }
        public Nullable<int> PAID { get; set; }
    
        public virtual BloodType BloodType { get; set; }
        public virtual City City { get; set; }
        public virtual Location Location { get; set; }
        public virtual Partner Partner { get; set; }
        public virtual ICollection<PartnersStatestic> PartnersStatestics { get; set; }
        public virtual ICollection<Needer_Donor> Needer_Donor { get; set; }
    }
}
