using BloodBankITI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

namespace BloodBankITI.Controllers
{
    public class PartnerController : Controller
    {
        BloodBankDBITIEntities db = new BloodBankDBITIEntities();
        // GET: Partner
        public ActionResult Index()
        {
            if (Session["UserId"] == null && Session["UserName"] == null)
            {
                return RedirectToAction("Index", "Login");
            }

            List<GetPartnersDonor_Result> Part_donor = new List<GetPartnersDonor_Result>();
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://www.bloodservice.somee.com/Home/");
            HttpResponseMessage response = client.GetAsync("getPartnar_Donors/" + Int32.Parse(Session["UserId"].ToString())).Result;
            if (response.IsSuccessStatusCode)
            {
                Part_donor = response.Content.ReadAsAsync<List<GetPartnersDonor_Result>>().Result;

            }
            return View(Part_donor);
        }
        [System.Web.Mvc.HttpGet]
        public ActionResult Edit (int id)
        {
            if (Session["UserId"] == null && Session["UserName"] == null)
            {
                return RedirectToAction("Index", "Login");
            }

            donor_SelectByDID_Result donor = new donor_SelectByDID_Result();
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://www.bloodservice.somee.com/Home/");
            HttpResponseMessage response = client.GetAsync("ViewProfile/" + id).Result;
            if (response.IsSuccessStatusCode)
            {
                donor = response.Content.ReadAsAsync<donor_SelectByDID_Result>().Result;

            }

            List<Select_BloodTypes_Result> Bloodtypes = new List<Select_BloodTypes_Result>();
            response = client.GetAsync("AllBloodTypes").Result;
            if (response.IsSuccessStatusCode)
            {
                Bloodtypes = response.Content.ReadAsAsync<List<Select_BloodTypes_Result>>().Result;

            }

            donorinsertform don = new donorinsertform() { BloodTypesResults = Bloodtypes, Donor = new Donor() { Fname = donor.Fname, Lname = donor.Lname, DID = donor.DID }, CitiesSelectAllResults = null };

            return View(don);
        }

        [System.Web.Mvc.HttpPost]
        public ActionResult Edit(donor_SelectByDID_Result donor)
        {
           
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://www.bloodservice.somee.com/Home/");
            HttpResponseMessage response = client.PostAsJsonAsync("insertBloodType/"+donor.BID+"/"+donor.DID+"","").Result;

            if (response.IsSuccessStatusCode)
            {
              return  RedirectToAction("Index");

            }

            return RedirectToAction("Edit", new { id = donor.DID });
        }

        ////Statistics
        [System.Web.Mvc.HttpGet]
        public ActionResult TodayStatistics()
        {
            if (Session["UserId"] == null && Session["UserName"] == null)
            {
                return RedirectToAction("Index", "Login");
            }
            return View(db.GetTodayStatestics(Int32.Parse(Session["UserId"].ToString())).ToList());
        }

        [System.Web.Http.HttpGet]
        public ActionResult Statistics()
        {
            if (Session["UserId"] == null && Session["UserName"] == null)
            {
                return RedirectToAction("Index", "Login");
            }
            else
            {
                return View();
            }
        }

        [System.Web.Mvc.HttpGet]
        public ActionResult AllStatistics()
        {
            if (Session["UserId"] == null && Session["UserName"] == null)
            {
                return RedirectToAction("Index", "Login");
            }
            else
            {
                return View(db.GetAllLabStatestics(Int32.Parse(Session["UserId"].ToString())).ToList());
            }
        }

    }
}