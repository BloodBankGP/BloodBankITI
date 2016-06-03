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
            return View(db.Admins_select(id).FirstOrDefault());
        }

        [HttpGet]
        public ActionResult UpdateProfile(int id)
        {
            return View(db.Admins_select(id).FirstOrDefault());
        }

        [HttpPost]
        public ActionResult UpdateProfile(Admin admin, Login login)
        {
            db.Admins_update(admin.AID, admin.Fname, admin.Lname, login.UserName, login.Password);
            return RedirectToAction("ViewProfile", new {id = admin.AID});
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


        //Blood Types
        [HttpGet]
        public ActionResult BloodTypes()
        {
            return View(db.Select_BloodTypes().ToList());
        }

        [HttpGet]
        public ActionResult BloodTypesEdit(int id)
        {
            return View(db.BloodTypeSelectByID(id).FirstOrDefault());
        }

        [HttpPost]
        public ActionResult BloodTypesEdit(BloodType bloodType)
        {
            db.BloodTypesEdit(bloodType.BID, bloodType.Type);
            return View("BloodTypes", db.Select_BloodTypes().ToList());
        }

        [HttpGet]
        public ActionResult BloodTypesInsert()
        {
            return View();
        }

        [HttpPost]
        public ActionResult BloodTypesInsert(BloodType bloodType)
        {
            db.BloodTypesInsert(bloodType.Type);
            return View("BloodTypes", db.Select_BloodTypes().ToList());
        }

        //Cities
        [HttpGet]
        public ActionResult Cities()
        {
            List<CitiesLocations> citiesLocations = new List<CitiesLocations>();
            List<Cities_SelectAll_Result> c = db.Cities_SelectAll().ToList();
            List<SelectCityLocations_Result> l = new List<SelectCityLocations_Result>();

            foreach (Cities_SelectAll_Result item in c)
            {
                l = db.SelectCityLocations(item.CID).ToList();
                citiesLocations.Add(new CitiesLocations() {city = item, locations = l});
            }

            return View(citiesLocations);
        }

        [HttpGet]
        public ActionResult InsertCity()
        {
            return View();
        }

        [HttpPost]
        public ActionResult InsertCity(City City)
        {
            db.Cities_InsertCity(City.CityName);
            return RedirectToAction("Cities");
        }

        [HttpGet]
        public ActionResult InsertLocation(int CID)
        {
            return View(CID);
        }

        [HttpPost]
        public ActionResult InsertLocation(Location Location, int CID)
        {
            db.Locations_InsertLocation(CID, Location.LocationName);
            return RedirectToAction("Cities");
        }

        //Posts
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