using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BloodBankITI.Models
{
    public class NeederCityBlood
    {
        public List<Cities_SelectAll_Result> CitiesSelectAllResults { get; set; }
        public List<Select_BloodTypes_Result> BloodTypesResults { get; set; }
        public select_NeederID_Result Needer { get; set; }
    }
}