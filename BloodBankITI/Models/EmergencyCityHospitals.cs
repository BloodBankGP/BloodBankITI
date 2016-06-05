using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BloodBankITI.Models
{
    public class EmergencyCityHospitals
    {
        public EmergencySelectCityDay_Result emergency { get; set; }

        public List<Hospitals_SelectByCity_Result> Hospitals { get; set; }
    }
}