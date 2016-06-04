using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using BloodBankITI.Models;
using Login = BloodBankITI.Models.Login;

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
            db.Admins_update(id, fname, lname,username,pw);
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


        //Needers

        [HttpGet]
        public ActionResult Needers()
        {
            return View(db.selectneederallinfo().ToList());
        }

        [HttpGet]
        public ActionResult NeederEdit(int id)
        {
            select_NeederID_Result needer=db.select_NeederID(id).FirstOrDefault();
            List <Cities_SelectAll_Result> cities=db.Cities_SelectAll().ToList();
            List<Select_BloodTypes_Result> bloodTypes = db.Select_BloodTypes().ToList();
            NeederCityBlood neederCityBlood = new NeederCityBlood()
            {
                Needer = needer,
                CitiesSelectAllResults = cities,
                BloodTypesResults = bloodTypes

            };
            return View(neederCityBlood);
        }
        [HttpPost]
        public ActionResult NeederEdit(Needer needer)
        {
            db.Update_Needer(needer.Email, needer.Fname, needer.Lname, needer.BID, needer.CID, needer.NID);
            return RedirectToAction("Needers");
        }

        public ActionResult NeederDelete(int id)
        {
            db.delete_Needer(id);
            return RedirectToAction("Needers");
        }


        //ViewRequests 

        [HttpGet]
        public ActionResult NeederDonor()
        {
            
            return View(db.neederdonorall().ToList());

        }
        //NGO
        [HttpGet]
        public ActionResult Ngo()
        {
            return View(db.NGO_selectt().ToList());
        }

        [HttpGet]
        public ActionResult NGOEdit(int id)
        {
            NGO_selectByIDAll_Result ngo = db.NGO_selectByIDAll(id).FirstOrDefault();

            List<Cities_SelectAll_Result> cities = db.Cities_SelectAll().ToList();

            NgoUpdate update = new NgoUpdate()
            {
                ngo =
                    new NGO()
                    {
                        CID = ngo.CID,
                        NID = ngo.NID,
                        Name = ngo.Name,
                        Phone = ngo.Phone,
                        Address = ngo.Address,
                        Status = ngo.Status,
                        Approved = ngo.Approved
                    },
                CitiesSelectAllResults = cities
            };

            return View(update);
        }

        [HttpPost]
        public ActionResult NGOEdit(NGO ngo)

        {
            db.NGOUPDATEADMIN(ngo.NID, ngo.Name, ngo.CID, ngo.Phone, ngo.Address,ngo.Status,ngo.Approved);
            db.SaveChanges();

           return RedirectToAction("Ngo");
        }
        

        public ActionResult NGODelete(int id)
        {
            db.NGO_delete(id);
            return RedirectToAction("Ngo");
        }




        //Partners
        [HttpGet]
        public ActionResult Partners()
        {
            return View(db.Partners_selectt().ToList());
        }
        
        [HttpGet]
        public ActionResult PartnersEdit(int id)
        {

            return View(db.Partners_select(id).FirstOrDefault());
        }

        [HttpPost]

        public ActionResult PartnersEdit(Partner Partner)
        {
            db.Partners_update(Partner.PAID,Partner.Name, Partner.Address,Partner.Status);

            return RedirectToAction("Partners");
        }

        [HttpGet]
        public ActionResult PartnersInsert()
        {

            return View();
        }

        [HttpPost]

        public ActionResult PartnersInsert(Partner partner)
        {
            db.Partners_insert( partner.Name, partner.Address);

            return RedirectToAction("Partners");
        }


        public ActionResult PartnersStatestics()
        {
            return View();
        }
        //users 
        //insert  New Users
        [HttpPost]
        public ActionResult UserTypesInsert(UserType userType)
        {
            db.UserType_insert(userType.Type);
            return RedirectToAction("UsersTypesView");
        }


        [HttpGet]
        public ActionResult UserTypesView()
        {

            return View(db.UserType_selectt().ToList());
        }

        [HttpGet]
        public ActionResult UserTypesEdit(int id)
        {

            return View(db.UserType_select(id).FirstOrDefault());
        }

        [HttpPost]

        public ActionResult UserTypesEdit(UserType userType)
        {
            db.UserType_update(userType.Type, userType.UTID);

            return RedirectToAction("UserTypesView");
        }



     
    }
}