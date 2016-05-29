﻿using System;
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