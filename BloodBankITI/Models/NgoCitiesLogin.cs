using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BloodBankITI.Models
{
    public class NgoCitiesLogin
    {
        public NGO ngo { get; set; }
        public Login Login { get; set; }
        public List<Cities_SelectAll_Result> CitiesSelectAllResults { get; set; }
    }
}