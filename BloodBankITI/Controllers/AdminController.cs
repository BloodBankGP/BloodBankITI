using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BloodBankITI.Models;

namespace BloodBankITI.Controllers
{
    public class AdminController : Controller
    {
        Models.BloodBankDBITIEntities db = new BloodBankDBITIEntities();

        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }

        //View Profile
        [HttpGet]
        public ActionResult ViewProfile(int id)
        {
            return View(db.Admins_select(id));
        }

        [HttpPost]
        public ActionResult UpdateProfile(int id, string fname, string lname, string username, string pw)
        {
            db.Admins_update(id, fname, lname);
            return View("Index", new {id = id});
        }

        //Admins
        [HttpGet]
        public ActionResult AdminsView()
        {
            return View(db.Admins_selectt().ToList());
        }

        
        public ActionResult AdminDelete(int id)
        {
            db.Admins_delete(id);
            return RedirectToAction("AdminsView");
        }

        [HttpGet]
        public ActionResult AdminInsert()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AdminInsert(Admin admin , Login login)
        {
            db.Admins_insert(admin.Fname, admin.Lname, login.UserName, login.Password);
            return RedirectToAction("AdminsView");
        }

        public ActionResult BloodTypes()
        {
            return View();
        }


        public ActionResult Cities()
        {
            return View();
        }

        public ActionResult PostsComments()
        {
            return View();
        }

        public ActionResult Days()
        {
            return View();
        }

        public ActionResult Donors()
        {
            return View();
        }

        public ActionResult Emergency()
        {
            return View();
        }

        public ActionResult Hospitals()
        {
            return View();
        }

        public ActionResult Locations()
        {
            return View();
        }

        public ActionResult Login()
        {
            return View();
        }

        public ActionResult Needers()
        {
            return View();
        }

        public ActionResult NeederDonor()
        {
            return View();
        }

        public ActionResult Ngo()
        {
            return View();
        }

        public ActionResult Partners()
        {
            return View();
        }

        public ActionResult PartnersStatestics()
        {
            return View();
        }

        public ActionResult UserTypes()
        {
            return View();
        }
    }
}