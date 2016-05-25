using BloodBankITI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace BloodBankITI.Controllers
{
    public class DonorController : Controller
    {
        // GET: Donor
        public ActionResult Index(int id)
        {
            NGO_selectByID_Result Donor = new NGO_selectByID_Result();
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://www.bloodservice.somee.com/Home/");
            HttpResponseMessage response = client.GetAsync("NgoByID/" + id).Result;
            if (response.IsSuccessStatusCode)
            {
                Donor = response.Content.ReadAsAsync<NGO_selectByID_Result>().Result;

            }
            return View(Donor);
        }
    }
}