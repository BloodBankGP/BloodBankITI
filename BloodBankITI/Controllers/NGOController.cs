using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BloodBankITI.Models;
using System.Net.Http;

namespace BloodBankITI.Controllers
{
    public class NGOController : Controller
    {
        // GET: NGO
        public ActionResult Index(int id)
        {
            NGO_selectByID_Result ngo = new NGO_selectByID_Result();
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://www.bloodservice.somee.com/Home/");
            HttpResponseMessage response = client.GetAsync("NgoByID/" + id).Result;
            if (response.IsSuccessStatusCode)
            {
                ngo = response.Content.ReadAsAsync<NGO_selectByID_Result>().Result;

            }
            return View(ngo);
        }

        [HttpGet]

        public ActionResult Edit(int id)
        {

            NGO_selectByID_Result ng = new NGO_selectByID_Result();
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://www.bloodservice.somee.com/Home/");
            HttpResponseMessage response = client.GetAsync("NgoByID/" + id).Result;
            if (response.IsSuccessStatusCode)
            {
                ng = response.Content.ReadAsAsync<NGO_selectByID_Result>().Result;

            }


            

            List<Cities_SelectAll_Result> cities = new List<Cities_SelectAll_Result>();
            
       
             response = client.GetAsync("ALLCities").Result;
            if (response.IsSuccessStatusCode)
            {
                cities = response.Content.ReadAsAsync<List<Cities_SelectAll_Result>>().Result;

            }

            NgoUpdate ngoUpdate = new NgoUpdate()
            {
                CitiesSelectAllResults = cities,
                ngo =
                    new NGO()
                    {
                        Address = ng.Address,
                        Approved = ng.Approved,
                        CID = ng.CID,
                        NID = ng.NID,
                        Phone = ng.Phone,
                        Name = ng.Name, 
                        Status = ng.Status
                    }
            };






            return View(ngoUpdate);
        }

        [HttpPost]
        public ActionResult Edit(NGO ngo)
        {

            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://www.bloodservice.somee.com/Home/");
            HttpResponseMessage response = client.PostAsJsonAsync("NGoUpdate/Ngo", ngo).Result;
            string result;

            if (response.IsSuccessStatusCode)
            {
                result = "Done";
            }
            else
                result = "Failed to insert Ngo";

            return RedirectToAction("Index");
        }
    }
}