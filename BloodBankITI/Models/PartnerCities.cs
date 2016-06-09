using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BloodBankITI.Models
{
    public class PartnerCities
    {
        public Partners_select_Result partnersSelectResult { get; set; }
        public List<Cities_SelectAll_Result> cities { get; set; }

    }
}