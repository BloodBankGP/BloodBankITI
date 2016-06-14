using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BloodBankITI.Models
{
    public class NgoUpdate
    {
        public List<Cities_SelectAll_Result> CitiesSelectAllResults { get; set; }

        public NGO_selectByID_Result ngo { get; set; }
    }
}