using BloodBankITI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace BloodBankITI.Controllers
{
    public class PartnerController : Controller
    {
        // GET: Partner
        public ActionResult Index(int id)
        {
            List<GetPartnersDonor_Result> Part_donor = new List<GetPartnersDonor_Result>();
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://www.bloodservice.somee.com/Home/");
            HttpResponseMessage response = client.GetAsync("getPartnar_Donors/" + id).Result;
            if (response.IsSuccessStatusCode)
            {
                Part_donor = response.Content.ReadAsAsync<List<GetPartnersDonor_Result>>().Result;

            }
            return View(Part_donor);
        }
    }
}