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
            if (Session["UserId"] == null && Session["UserName"] == null)
            {
                return RedirectToAction("Index", "Login");
            }

            return View();
        }

        //View Profile
        [HttpGet]
        public ActionResult ViewProfile()
        {
            if (Session["UserId"] == null && Session["UserName"] == null)
            {
                return RedirectToAction("Index", "Login");
            }
            return View(db.Admins_select(Int32.Parse(Session["UserId"].ToString())).FirstOrDefault());
        }

        [HttpGet]
        public ActionResult UpdateProfile()
        {
            if (Session["UserId"] == null && Session["UserName"] == null)
            {
                return RedirectToAction("Index", "Login");
            }
            return View(db.Admins_select(Int32.Parse(Session["UserId"].ToString())).FirstOrDefault());
        }

        [HttpPost]
        public ActionResult UpdateProfile(Admin admin, Login login)
        {
            db.Admins_update(admin.AID, admin.Fname, admin.Lname, login.UserName, login.Password,admin.Picture);
            return View("Index");
        }


        //Admins
        [HttpGet]
        public ActionResult AdminsView()
        {
            if (Session["UserId"] == null && Session["UserName"] == null)
            {
                RedirectToAction("Index", "Login");
            }
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
            if (Session["UserId"] == null && Session["UserName"] == null)
            {
                return RedirectToAction("Index", "Login");
            }
            return View()
;
        }
        [HttpPost]
        public ActionResult AdminInsert(Admin admin, Login login)
        {
            db.Admins_insert(admin.Fname, admin.Lname, login.UserName, login.Password,admin.Picture);
            return RedirectToAction("AdminsView");
        }


        //Blood Types
        [HttpGet]
        public ActionResult BloodTypes()
        {
            if (Session["UserId"] == null && Session["UserName"] == null)
            {
                return RedirectToAction("Index", "Login");
            }
            return View(db.Select_BloodTypes().ToList());
        }


        //Cities
        [HttpGet]
        public ActionResult Cities()
        {
            if (Session["UserId"] == null && Session["UserName"] == null)
            {
                return RedirectToAction("Index", "Login");
            }
            List<CitiesLocations> citiesLocations = new List<CitiesLocations>();
            List<Cities_SelectAll_Result> c = db.Cities_SelectAll().ToList();
            List<SelectCityLocations_Result> l = new List<SelectCityLocations_Result>();

            foreach (Cities_SelectAll_Result item in c)
            {
                l = db.SelectCityLocations(item.CID).ToList();
                citiesLocations.Add(new CitiesLocations() { city = item, locations = l });
            }

            return View(citiesLocations);
        }

        [HttpGet]
        public ActionResult InsertCity()
        {
            if (Session["UserId"] == null && Session["UserName"] == null)
            {
                return RedirectToAction("Index", "Login");
            }
            return View();
        }

        [HttpPost]
        public ActionResult InsertCity(City City)
        {
            db.Cities_InsertCity(City.CityName, City.Logo);
            return RedirectToAction("Cities");
        }

        [HttpGet]
        public ActionResult InsertLocation(int CID)
        {
            if (Session["UserId"] == null && Session["UserName"] == null)
            {
                return RedirectToAction("Index", "Login");
            }
            return View(CID);
        }

        [HttpPost]
        public ActionResult InsertLocation(Location Location, int CID)
        {
            db.Locations_InsertLocation(CID, Location.LocationName);
            return RedirectToAction("Cities");
        }

        //Posts and Comments
        [HttpGet]
        public ActionResult PostsComments()
        {
            if (Session["UserId"] == null && Session["UserName"] == null)
            {
                return RedirectToAction("Index", "Login");
            }
            List<PostsComments> postsComments = new List<PostsComments>();
            List<posts_SelectAll_Result> posts = db.posts_SelectAll().ToList();
            List<Comments_SelectAllByPostID_Result> postComments = new List<Comments_SelectAllByPostID_Result>();

            foreach (posts_SelectAll_Result p in posts)
            {
                postComments = db.Comments_SelectAllByPostID(p.PID).ToList();
                postsComments.Add(new PostsComments() {post = p, comments = postComments});
            }

            return View(postsComments);
        }

        public ActionResult DeletePost(Post post)
        {
            db.posts_DeletePost(post.PID);
            return RedirectToAction("PostsComments");
        }

        public ActionResult DeleteComment(int CID)
        {
            db.CommentDelete(CID);
            return RedirectToAction("PostsComments");
        }

        //Donors
        [HttpGet]
        public ActionResult Donors()
        {
            if (Session["UserId"] == null && Session["UserName"] == null)
            {
                return RedirectToAction("Index", "Login");
            }
            return View(db.Donors_select().ToList());
        }

        [HttpGet]
        public ActionResult DonorsByBID(Donor donor)
        {
            if (Session["UserId"] == null && Session["UserName"] == null)
            {
                return RedirectToAction("Index", "Login");
            }
            List<DonorByBid_Result> donors = db.DonorByBid(donor.BID).ToList();
            return View(donors);
        }

        public ActionResult DeleteDonor(int DID)
        {
            db.Donors_Delete(DID);
            return RedirectToAction("Donors");
        }

        [HttpGet]
        public ActionResult Emergency()
        {
            if (Session["UserId"] == null && Session["UserName"] == null)
            {
                return RedirectToAction("Index", "Login");
            }
            return View(db.EmergencySelectAll().ToList());  
        }
        [HttpGet]
        public ActionResult EmergencyEdit(int did , int cid)
        {
            if (Session["UserId"] == null && Session["UserName"] == null)
            {
                return RedirectToAction("Index", "Login");
            }
            EmergencySelectCityDay_Result emergency = db.EmergencySelectCityDay(did, cid).FirstOrDefault();

            List<Hospitals_SelectByCity_Result> hospitals = db.Hospitals_SelectByCity(cid).ToList();

            EmergencyCityHospitals emergencyCityHospitals = new EmergencyCityHospitals()
            {
                emergency = emergency,
                Hospitals = hospitals
            };
            return View(emergencyCityHospitals);
        }

        [HttpPost]
        public ActionResult EmergencyEdit(Emergency emergency)
        {
            db.EmergencyUpdate(emergency.DayID, emergency.CID, emergency.HID);
            return RedirectToAction("Emergency");
        }

        [HttpGet]
        public ActionResult Hospitals()
        {
            if (Session["UserId"] == null && Session["UserName"] == null)
            {
                return RedirectToAction("Index", "Login");
            }
            return View(db.Hospitals_SelectAll().ToList());
        }

        [HttpGet]
        public ActionResult HospitalsEdit(int id)
        {
            if (Session["UserId"] == null && Session["UserName"] == null)
            {
                return RedirectToAction("Index", "Login");
            }
            Hospitals_SelectByID_Result hospitals = db.Hospitals_SelectByID(id).FirstOrDefault();

            List<Cities_SelectAll_Result> cities = db.Cities_SelectAll().ToList();

            HospitalsCity hospitalsCity = new HospitalsCity()
            {
                Hospital = hospitals,
                CitiesSelectAllResults = cities
            };
            return View(hospitalsCity);
        }

        [HttpPost]
        public ActionResult HospitalsEdit(Hospital hospital)
        {
            db.Hospitals_UpdateHospital(hospital.Name, hospital.CID, hospital.Phone, hospital.Address, hospital.HID);
            return RedirectToAction("Hospitals");
        }

        [HttpGet]
        public ActionResult HospitalsInsert()
        {
            if (Session["UserId"] == null && Session["UserName"] == null)
            {
                return RedirectToAction("Index", "Login");
            }

            List<Cities_SelectAll_Result> cities = db.Cities_SelectAll().ToList();

            HospitalsCity hospitalsCity = new HospitalsCity()
            {
                Hospital = null,
                CitiesSelectAllResults = cities
            };
            return View(hospitalsCity);
        }

        [HttpPost]
        public ActionResult HospitalsInsert(Hospital hospital)
        {
            db.Hospitals_InsertHospital(hospital.Name, hospital.CID, hospital.Phone, hospital.Address);
            return RedirectToAction("Hospitals");
        }

        [HttpGet]
        public ActionResult ShowMsgs()

        {
            if (Session["UserId"] == null && Session["UserName"] == null)
            {
                return RedirectToAction("Index", "Login");
            }
            return View(db.ContactSelect().ToList());
        }

        [HttpGet]
        public ActionResult ShowMsg(int id)
        {
            if (Session["UserId"] == null && Session["UserName"] == null)
            {
                return RedirectToAction("Index", "Login");
            }
            return View(db.GetContact(id).FirstOrDefault());
        }

        public ActionResult DeleteMsg(int id)
        {
            if (Session["UserId"] == null && Session["UserName"] == null)
            {
                return RedirectToAction("Index", "Login");
            }
            db.ContactDelete(id);
            return RedirectToAction("ShowMsgs");
        }


        public ActionResult HospitalsDelete(int id)
        {
            db.Hospitals_DeleteHospital(id);
            return RedirectToAction("Hospitals");
        }
        //Needers

        [HttpGet]
        public ActionResult Needers()
        {
            if (Session["UserId"] == null && Session["UserName"] == null)
            {
                return RedirectToAction("Index", "Login");
            }
            return View(db.selectneederallinfo().ToList());
        }

        [HttpGet]
        public ActionResult NeederEdit(int id)
        {
            if (Session["UserId"] == null && Session["UserName"] == null)
            {
                return RedirectToAction("Index", "Login");
            }
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
            if (Session["UserId"] == null && Session["UserName"] == null)
            {
                return RedirectToAction("Index", "Login");
            }
            return View(db.neederdonorall().ToList());

        }
        //NGO
        [HttpGet]
        public ActionResult Ngo()
        {
            if (Session["UserId"] == null && Session["UserName"] == null)
            {
                return RedirectToAction("Index", "Login");
            }
            return View(db.NGO_selectt().ToList());
        }

        [HttpGet]
        public ActionResult NGOEdit(int id)
        {
            if (Session["UserId"] == null && Session["UserName"] == null)
            {
                return RedirectToAction("Index", "Login");
            }
            NGO_selectByIDAll_Result ngo = db.NGO_selectByIDAll(id).FirstOrDefault();

            List<Cities_SelectAll_Result> cities = db.Cities_SelectAll().ToList();

            NgoUpdate update = new NgoUpdate()
            {
                ngo =
                    new NGO_selectByID_Result()
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
            db.NGOUPDATEADMIN(ngo.NID, ngo.Name, ngo.CID, ngo.Phone, ngo.Address,ngo.Status,ngo.Approved,ngo.Fb,ngo.Website);
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
            if (Session["UserId"] == null && Session["UserName"] == null)
            {
                return RedirectToAction("Index", "Login");
            }
            return View(db.Partners_selectt().ToList());
        }

       

        [HttpGet]
        public ActionResult PartnersEdit(int id)
        {
            if (Session["UserId"] == null && Session["UserName"] == null)
            {
                return RedirectToAction("Index", "Login");
            }
            Partners_select_Result partner = db.Partners_select(id).FirstOrDefault();

            List<Cities_SelectAll_Result> cities = db.Cities_SelectAll().ToList();

            PartnerCities partnerCities = new PartnerCities()
            {
                cities = cities,
                partnersSelectResult = partner
            };
            return View(partnerCities);
        }


        [HttpPost]

        public ActionResult PartnersEdit(Partner Partner)
        {
            db.Partners_update(Partner.PAID,Partner.Name, Partner.Address,Partner.Status, Partner.CID);

            return RedirectToAction("Partners");
        }

        [HttpGet]
        public ActionResult PartnersInsert()
        {
            if (Session["UserId"] == null && Session["UserName"] == null)
            {
                return RedirectToAction("Index", "Login");
            }
            List<Cities_SelectAll_Result> cities = db.Cities_SelectAll().ToList();

            return View(cities);
        }

        [HttpPost]

        public ActionResult PartnersInsert(Partner partner)
        {
            db.Partners_insert( partner.Name, partner.Address, partner.CID);

            return RedirectToAction("Partners");
        }


        //Statistics
        [HttpGet]
        public ActionResult PartnersStatistics()
        {
            if (Session["UserId"] == null && Session["UserName"] == null)
            {
                return RedirectToAction("Index", "Login");
            }
            return View();
        }

        [HttpGet]
        public ActionResult TodayStatistics()
        {
            if (Session["UserId"] == null && Session["UserName"] == null)
            {
                return RedirectToAction("Index", "Login");
            }
            return View(db.GetAllTodayStatestics().ToList());
        }

        [HttpGet]
        public ActionResult AllStatistics()
        {
            if (Session["UserId"] == null && Session["UserName"] == null)
            {
                return RedirectToAction("Index", "Login");
            }
            return View(db.GetStatestics().ToList());
        }


        //users 
        //insert  New Users
        [HttpGet]
        public ActionResult UserTypesInsert()
        {
            
            return View();
        }
        [HttpPost]
        public ActionResult UserTypesInsert(UserType userType)
        {
            db.UserType_insert(userType.Type);
            return RedirectToAction("UsersTypesView");
        }


        [HttpGet]
        public ActionResult UserTypesView()
        {
            if (Session["UserId"] == null && Session["UserName"] == null)
            {
                return RedirectToAction("Index", "Login");
            }
            return View(db.UserType_selectt().ToList());
        }

        [HttpGet]
        public ActionResult UserTypesEdit(int id)
        {
            if (Session["UserId"] == null && Session["UserName"] == null)
            {
                return RedirectToAction("Index", "Login");
            }
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