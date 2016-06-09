using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BloodBankITI.Models
{
    public class donorpartner
    {
        public donor_SelectByDID_Result donor { get; set; }
        public List<Partner_SelectByCity_Result> partnerSelectByCity { get; set; }
    }
}