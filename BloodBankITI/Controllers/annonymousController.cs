using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using BloodBankITI.Models;

namespace BloodBankITI.Controllers
{
    public class annonymousController : Controller
    {
        //
        // GET: /annonymous/
        [HttpGet]
        public ActionResult Donate()
        {

            List<Cities_SelectAll_Result> cities = new List<Cities_SelectAll_Result>();
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://www.bloodservice.somee.com/Home/");
            HttpResponseMessage response = client.GetAsync("ALLCities").Result;
            if (response.IsSuccessStatusCode)
            {
                cities = response.Content.ReadAsAsync<List<Cities_SelectAll_Result>>().Result;

            }


            List<Select_BloodTypes_Result> bloodTypes = new List<Select_BloodTypes_Result>();

            response = client.GetAsync("AllBloodTypes").Result;
            if (response.IsSuccessStatusCode)
            {
                bloodTypes = response.Content.ReadAsAsync<List<Select_BloodTypes_Result>>().Result;

            }

            donorinsertform donor = new donorinsertform()
            {
                BloodTypesResults = bloodTypes,
                Donor = null,
                CitiesSelectAllResults = cities,
                //LocationsSelectAllByCity = null
            };
            return View(donor);
        }


        [HttpPost]
        public ActionResult Donate(Donor donor)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://www.bloodservice.somee.com/Home/");
            HttpResponseMessage response = client.PostAsJsonAsync("donor_insert/donor", donor).Result;
            string result;

            if (response.IsSuccessStatusCode)
            {
                result = "Done";
            }
            else
                result = "Failed to insert Donor";

            return RedirectToAction("Index");
        }


    }
}