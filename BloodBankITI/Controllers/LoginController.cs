using BloodBankITI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using Microsoft.Ajax.Utilities;

namespace BloodBankITI.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        [HttpGet]
        public ActionResult Index()
        {


           
            return View();
        }

        [HttpPost]
        public ActionResult Index(string UserName , string Password)
        {
            CheckLogin_Result login = new CheckLogin_Result();
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://www.bloodservice.somee.com/Home/");
            HttpResponseMessage response = client.GetAsync("CheckLogin/" + UserName+"/"+Password).Result;
            if (response.IsSuccessStatusCode)
            {
                login = response.Content.ReadAsAsync<CheckLogin_Result>().Result;

            }

            if (login.UserName != null && login.Password != null)
            {
                Session["UserId"] = login.UID;
                Session["UserName"] = login.UserName;
                Session["UserType"] = login.UTID;


                return RedirectToAction("Index", "NGO",new {id=login.UID});
            }

            return RedirectToAction("Index");
        }

    }
}