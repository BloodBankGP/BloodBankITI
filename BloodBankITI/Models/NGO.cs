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
    
    public partial class NGO
    {
        [Required(ErrorMessage = "��� ����� �����")]
        public int NID { get; set; }
        [Required(ErrorMessage = "��� ����� �����")]
        public string Name { get; set; }
        [Required(ErrorMessage = "��� ����� �����")]
        public int CID { get; set; }
        [Required(ErrorMessage = "��� ����� �����")]
        [Phone(ErrorMessage = "�� ���� ���� ��� ���� ����")]
        public string Phone { get; set; }
        [Required(ErrorMessage = "��� ����� �����")]
        public bool Approved { get; set; }
        public bool Status { get; set; }
        public string Address { get; set; }
        [Required(ErrorMessage = "��� ����� �����")]
        [Url(ErrorMessage="��� ������ ��� ����")]
        public string Fb { get; set; }
        [Required(ErrorMessage = "��� ����� �����")]
        [Url(ErrorMessage = "��� ������ ��� ����")]
        public string Website { get; set; }
    
        public virtual City City { get; set; }
    }
}
