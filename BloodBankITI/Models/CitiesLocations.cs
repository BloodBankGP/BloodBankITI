using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BloodBankITI.Models
{
    public class CitiesLocations
    {
        public Cities_SelectAll_Result city  { get; set; }

        public List<SelectCityLocations_Result> locations  { get; set; }

    }
}