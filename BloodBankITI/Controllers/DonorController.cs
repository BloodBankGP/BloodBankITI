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
        //ViewProfile
        [HttpGet]
        public ActionResult Index(int id)
        {
            donor_SelectByDID_Result Donor = new donor_SelectByDID_Result();
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://www.bloodservice.somee.com/Home/");
            HttpResponseMessage response = client.GetAsync("ViewProfile/" + id).Result;
            if (response.IsSuccessStatusCode)
            {
                Donor = response.Content.ReadAsAsync<donor_SelectByDID_Result>().Result;
            }
            return View(Donor);
        }

        //UpdateProfile
        [HttpGet]
        public ActionResult UpdateProfile(int id)
        {
            donorCityBlood donor = new donorCityBlood();
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://www.bloodservice.somee.com/Home/");

            //Get Cities
            HttpResponseMessage response = client.GetAsync("ALLCities").Result;
            if (response.IsSuccessStatusCode)
            {
                donor.CitiesSelectAllResults = response.Content.ReadAsAsync<List<Cities_SelectAll_Result>>().Result;
            }

            //Get Blood Types
            response = client.GetAsync("AllBloodTypes").Result;
            if (response.IsSuccessStatusCode)
            {
                donor.BloodTypesResults = response.Content.ReadAsAsync<List<Select_BloodTypes_Result>>().Result;
            }

            //Get Donor
            response = client.GetAsync("ViewProfile/" + id).Result;
            if (response.IsSuccessStatusCode)
            {
                donor.Donor = response.Content.ReadAsAsync<donor_SelectByDID_Result>().Result;
            }

            return View(donor);
        }


        [HttpPost]
        public ActionResult UpdateProfile(Donor donor)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://www.bloodservice.somee.com/Home/");
            HttpResponseMessage response = client.PostAsJsonAsync("donorupdate/donor", donor).Result;
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index", new {id = donor.DID});
            }
            else
                return RedirectToAction("UpdateProfile", donor.DID);
        }

    }
}