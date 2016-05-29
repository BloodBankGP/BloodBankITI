using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BloodBankITI.Models
{
    public class donorCityBlood
    {
        public List<Cities_SelectAll_Result> CitiesSelectAllResults { get; set; }
        //public List<Locations_SelectAllByCityID_Result> LocationsSelectAllByCity { get; set; }
        public List<Select_BloodTypes_Result> BloodTypesResults { get; set; }
        public donor_SelectByDID_Result Donor { get; set; }
    }
}