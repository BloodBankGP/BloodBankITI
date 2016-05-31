using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using BloodBankITI.Models;
using System.Net.Mail;
using System.Text;

namespace BloodBankITI.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Info()
        {
            return View();
        }
        //Register
        public ActionResult Register()
        {
            return View();
        }

      
        [HttpGet]
        public ActionResult Contact()
        {
            return View();
        }

       
    
 
  ////////////////////////////////////////////
  
    //Ask For Blood
    [HttpGet]
        public ActionResult AskForBlood()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AskForBlood(Needer n)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://www.bloodservice.somee.com/Home/");
            HttpResponseMessage response = client.PostAsJsonAsync("insertNeeder", n).Result;
            string result;
            
            if (response.IsSuccessStatusCode)
            {
                result = "Done";
            }
            else
                result = "Failed to insert Needer";
            return RedirectToAction("Index");
        }
    }
}