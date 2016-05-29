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
        public ActionResult UpdateProfile(int did)
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

            donorCityBlood donor = new donorCityBlood();
            HttpClient client1 = new HttpClient();
            client.BaseAddress = new Uri("http://www.bloodservice.somee.com/Home/");
            HttpResponseMessage response1 = client.GetAsync("ViewProfile/" + did).Result;
            if (response.IsSuccessStatusCode)
            {
                donor.Donor = response.Content.ReadAsAsync<donor_SelectByDID_Result>().Result;
            }

            return View(donor);
        }


        [HttpPost]
        public ActionResult UpdateProfile(donorCityBlood donor)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://www.bloodservice.somee.com/Home/");
            HttpResponseMessage response = client.PostAsJsonAsync("donorupdate/donor", donor).Result;
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            else
                return View();
        }

    }
}