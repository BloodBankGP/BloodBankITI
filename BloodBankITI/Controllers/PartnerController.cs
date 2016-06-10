using BloodBankITI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace BloodBankITI.Controllers
{
    public class PartnerController : Controller
    {
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
        [HttpGet]
        public ActionResult Edit (int id)
        {
            if (Session["UserId"] == null && Session["UserName"] == null)
            {
                return RedirectToAction("Index", "Login");
            }

            donor_SelectByDID_Result donor = new donor_SelectByDID_Result();
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://www.bloodservice.somee.com/Home/");
            HttpResponseMessage response = client.GetAsync("SelectDonorByDID/" + id).Result;
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

        [HttpPost]
        public ActionResult Edit(int BID , int DID)
        {
           var content = new FormUrlEncodedContent(
                new List<KeyValuePair<string, string>>
                {
                    new KeyValuePair<string, string>("bid", BID.ToString()),
                    new KeyValuePair<string, string>("did", DID.ToString())
                }
                );
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://www.bloodservice.somee.com/Home/");
            HttpResponseMessage response = client.PutAsync("insertBloodType/"+BID+"/"+DID+"",content).Result;
            if (response.IsSuccessStatusCode)
            {
              return  RedirectToAction("Index");

            }

            return RedirectToAction("Edit", new { id = DID });
        }

    }
}