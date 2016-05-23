using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using BloodBankITI.Models;

namespace BloodBankITI.Controllers
{
    public class NGOController : Controller
    {
        // GET: NGO
        public ActionResult Index(int id)
        {
            NGO_selectByID_Result ngo = new NGO_selectByID_Result() ;
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://www.bloodservice.somee.com/Home/");
            HttpResponseMessage response = client.GetAsync("NgoByID/"+id).Result;
            if (response.IsSuccessStatusCode)
            {
                ngo = response.Content.ReadAsAsync<NGO_selectByID_Result>().Result;
                
            }
            return View(ngo);
        }
    }
}