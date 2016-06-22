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
        public ActionResult Index()
        {
            if (Session["UserId"] == null && Session["UserName"] == null)
            {
                return RedirectToAction("Index", "Login");
            }

            NGO_selectByID_Result ngo = new NGO_selectByID_Result();
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://www.bloodservice.somee.com/Home/");
            HttpResponseMessage response = client.GetAsync("NgoByID/" + Int32.Parse(Session["UserId"].ToString())).Result;
            if (response.IsSuccessStatusCode)
            {
                ngo = response.Content.ReadAsAsync<NGO_selectByID_Result>().Result;

            }
            return View(ngo);
        }

        [HttpGet]

        public ActionResult Edit()
        {
            if (Session["UserId"] == null && Session["UserName"] == null)
            {
                return RedirectToAction("Index", "Login");
            }

            NGO_selectByID_Result ng = new NGO_selectByID_Result();
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://www.bloodservice.somee.com/Home/");
            HttpResponseMessage response = client.GetAsync("NgoByID/" + Int32.Parse(Session["UserId"].ToString())).Result;
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
                    new NGO_selectByID_Result()
                    {
                        Address = ng.Address,
                        Approved = ng.Approved,
                        CID = ng.CID,
                        NID = ng.NID,
                        Phone = ng.Phone,
                        Name = ng.Name, 
                        Status = ng.Status,
                        Username = ng.Username,
                        Password = ng.Password
                    }
            };
            return View(ngoUpdate);
        }


        [HttpPost]
        public ActionResult Edit(NGO_selectByID_Result ngo)
        {
            if (ModelState.IsValid)
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
            else
            {
                return View();
            }
        }
    }
}