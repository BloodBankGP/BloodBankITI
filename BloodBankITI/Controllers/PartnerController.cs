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
        public ActionResult Index()
        {
            //GetPartnersDonor_Result ngo = new GetPartnersDonor_Result();
            //HttpClient client = new HttpClient();
            //client.BaseAddress = new Uri("http://www.bloodservice.somee.com/Home/");
            //HttpResponseMessage response = client.GetAsync("NgoByID/" + id).Result;
            //if (response.IsSuccessStatusCode)
            //{
            //    ngo = response.Content.ReadAsAsync<NGO_selectByID_Result>().Result;

            //}
            //return View(ngo);
            return View();
        }
    }
}