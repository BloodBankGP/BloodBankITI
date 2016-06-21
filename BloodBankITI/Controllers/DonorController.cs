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
        public ActionResult Index()
        {
            if (Session["UserId"] == null && Session["UserName"] == null)
            {
                return RedirectToAction("Index", "Login");
            }
            
            return View();
        }

        [HttpGet]
        public ActionResult ViewProfile()
        {
            if (Session["UserId"] == null && Session["UserName"] == null)
            {
                return RedirectToAction("Index", "Login");
            }

            donor_SelectByDID_Result Donor = new donor_SelectByDID_Result();
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://www.bloodservice.somee.com/Home/");
            HttpResponseMessage response = client.GetAsync("ViewProfile/" + Int32.Parse(Session["UserId"].ToString())).Result;
            if (response.IsSuccessStatusCode)
            {
                Donor = response.Content.ReadAsAsync<donor_SelectByDID_Result>().Result;
            }
            return View(Donor);
        }

        //UpdateProfile
        [HttpGet]
        public ActionResult UpdateProfile()
        {
            if (Session["UserId"] == null && Session["UserName"] == null)
            {
              return  RedirectToAction("Index", "Login");
            }
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
            response = client.GetAsync("ViewProfile/" + Int32.Parse(Session["UserId"].ToString())).Result;
            if (response.IsSuccessStatusCode)
            {
                donor.Donor = response.Content.ReadAsAsync<donor_SelectByDID_Result>().Result;
            }

            //Get Locations
            response = client.GetAsync("LocationByCID/" + donor.Donor.CID).Result;
            if (response.IsSuccessStatusCode)
            {
                donor.Locations = response.Content.ReadAsAsync<List<Locations_SelectAllByCityID_Result>>().Result;
            }

            return View(donor);
        }


        [HttpPost]
        public ActionResult UpdateProfile(donor_SelectByDID_Result donor)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://www.bloodservice.somee.com/Home/");
            HttpResponseMessage response = client.PostAsJsonAsync("donorupdate/donor", donor).Result;
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            else
                return RedirectToAction("UpdateProfile");
        }

        [HttpPost]
        public ActionResult UpdateDonationDate(Donor donor)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://www.bloodservice.somee.com/Home/");
            HttpResponseMessage response = client.PostAsJsonAsync("updatePending/donor", donor).Result;
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            else
                return RedirectToAction("UpdateProfile");
        }

        [HttpGet]
        public ActionResult Requests ()
        {
            if (Session["UserId"] == null && Session["UserName"] == null)
            {
                return RedirectToAction("Index", "Login");
            }

            return View(Int32.Parse(Session["UserId"].ToString()));
        }

        [HttpGet]
        public ActionResult AcceptRequest(int id)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://www.bloodservice.somee.com/Home/");
            HttpResponseMessage response = client.PostAsJsonAsync("DonorAcceptRequest/" + Session["UserId"].ToString() + "/" + id, "").Result;
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Requests");
            }
            else
                return RedirectToAction("Requests");
        }

        [HttpGet]
        public ActionResult CancelRequest(int id)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://www.bloodservice.somee.com/Home/");
            HttpResponseMessage response = client.PostAsJsonAsync("DonorCancelRequest/" + Session["UserId"].ToString() + "/" + id, "").Result;
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Requests");
            }
            else
                return RedirectToAction("Requests");
        }

    }
}