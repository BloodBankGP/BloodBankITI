using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BloodBankITI.Models
{
    public class HospitalsCity
    {
        public List<Cities_SelectAll_Result> CitiesSelectAllResults { get; set; }

        public Hospitals_SelectByID_Result Hospital { get; set; }
    }
}