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
    
    public partial class donor_SelectByDID_Result
    {
        public int DID { get; set; }
        public string Fname { get; set; }
        public string Lname { get; set; }
        public string DonorGender { get; set; }
        public string Phone { get; set; }
        public Nullable<int> BID { get; set; }
        public int CID { get; set; }
        public int LID { get; set; }
        public bool Status { get; set; }
        public bool Pending { get; set; }
        public Nullable<System.DateTime> DonationDate { get; set; }
        public Nullable<int> PAID { get; set; }
        public Nullable<bool> PhoneStatus { get; set; }
        public string Type { get; set; }
        public string CityName { get; set; }
        public string LocationName { get; set; }
    }
}
