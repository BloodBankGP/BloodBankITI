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
        BloodBankDBITIEntities db = new BloodBankDBITIEntities();

        // GET: Login
        [HttpGet]
        public ActionResult Index()
        {



            return View();
        }

        [HttpPost]
        public ActionResult Index(string UserName, string Password)
        {
            if (ModelState.IsValid)
            {
                CheckLogin_Result login = new CheckLogin_Result();
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri("http://www.bloodservice.somee.com/Home/");
                HttpResponseMessage response = client.GetAsync("CheckLogin/" + UserName + "/" + Password).Result;
                if (response.IsSuccessStatusCode)
                {
                    login = response.Content.ReadAsAsync<CheckLogin_Result>().Result;

                }

                if (login != null)
                {
                    Session["UserId"] = login.UID;
                    Session["UserName"] = login.UserName;
                    Session["UserType"] = login.UTID;

                    switch (login.UTID)
                    {
                        case 1:
                            Session["Fname"] = db.Admins.Where(a => a.AID == login.UID).FirstOrDefault().Fname;
                            Session["Lname"] = db.Admins.Where(a => a.AID == login.UID).FirstOrDefault().Lname;
                            break;
                        case 2:
                            Session["Fname"] = db.Donors.Where(a => a.DID == login.UID).FirstOrDefault().Fname;
                            Session["Lname"] = db.Donors.Where(a => a.DID == login.UID).FirstOrDefault().Lname;
                            break;
                        case 3:
                            Session["Fname"] = db.NGOes.Where(a => a.NID == login.UID).FirstOrDefault().Name;
                            break;
                        case 4:
                            Session["Fname"] = db.Partners.Where(a => a.PAID == login.UID).FirstOrDefault().Name;
                            break;
                    }


                    return RedirectToAction("Index", "Home");
                }

                return RedirectToAction("Index");
            }
            else
            {
                return RedirectToAction("Index");
            }

        }
    }
}