using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.Xml;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;
using BloodBankService.Models;
using System.Web.Http.Cors;

namespace BloodBankService.Controllers
{
    [RoutePrefix("Home")]
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class HomeController : ApiController
    {
        Models.BloodBankDBITIEntities1 db = new Models.BloodBankDBITIEntities1();
    

        /// <summary>
        ///Anonymous
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("AllPosts")]
        public List<Models.posts_SelectAll_Result> AllPosts ()
        {
            return  db.posts_SelectAll().ToList();   
        }

        [HttpPost]
        [Route("CreatePost/{post}")]
        public void CreatePost(Models.Post post)
        {
            db.posts_InsertPost(post.Post1 ,post.Phone,post.BID,post.CID,post.Name,post.Periodic);
        }

        [HttpGet]
        [Route("AllPosts/{bid:int}/{cid:int}")]
        public List<Models.posts_SelectByBID_CID_Result> AllPostsBySearch(int bid , int cid)
        {
            return db.posts_SelectByBID_CID(bid , cid).ToList();
        }
        [HttpGet]
        [Route("DonorByID/{cid:int}/{bid:int}/{lid:int}")]
        public List<Models.Donors_SelectID_Result> DonorsSelectByID(int cid, int bid, int lid)
        {
            return db.Donors_SelectID(cid, bid, lid).ToList();
        }

        [HttpGet]
        [Route("NgoSelect/{cid:int}")]
        public List<Models.NGO_select_Result> NgoSelectAllByCity(int cid)
        {
            return db.NGO_select(cid).ToList();
        }

        [HttpGet]
        [Route("NgoByID/{nid:int}")]
        public Models.NGO_selectByID_Result NgoSelectById(int nid)
        {
            return db.NGO_selectByID(nid).FirstOrDefault();
        }

        [HttpPost]
        [Route("NgoRequest/{ngo}")]
        public string NgoRequest(NGO ngo)
        {
            if (db.NGO_insert(ngo.Name, ngo.CID, ngo.Phone, ngo.Address) == 1)
            {
                return "Ngo Inserted";
            }
            else
            {
                return "Operation Falied";
            }


        }

        [HttpGet]
        [Route("CheckLogin/{username}/{password}")]
        public Models.CheckLogin_Result CheckLogin(string username, string password)
        {
            return db.CheckLogin(username, password).FirstOrDefault();
        }

        [HttpPost]
        [Route("Signup/{donor}/ {login}")]
        public string Signup(Donor donor, Login login)
        {
            if (db.CheckName(login.UserName) == null)
            {
                var id = db.Donors_Insert(donor.Fname, donor.Lname,donor.DonorGender, donor.Phone, donor.BID, donor.CID,
                    donor.LID, true, donor.Pending, donor.DonationDate, donor.PAID,donor.PhoneStatus);

                db.Login_insert(login.UserName, login.Password, 2, id);

                return "Inserted";
            }
            else
            {
                return "Invalid Name";
            }
            
        }

        [HttpGet]
        [Route("EmergencyToday/{cityid:int}/{dayid:int}")]
        public EmergencyToday_Result EmergencyToday(int cityid, int dayid)
        {
            return db.EmergencyToday(dayid, cityid).FirstOrDefault();
        }


        ////////////////////////////Donor
        [HttpPut]
        [Route("updatePending/{donorid}")]
        public void UpdatePending(int donorid)
        {
            db.donor_updatepending(donorid);
        }

        [HttpGet]
        [Route("SelectDonorByDID/{donorid}")]
        public donor_SelectByDID_Result SelectDonorByID(int donorid)
        {
           return db.donor_SelectByDID(donorid).FirstOrDefault();
        }

        [HttpPut]
        [Route ("donorupdate/{donor}")]
        public void donor_update(Donor donor)
        {
            db.Donors_UpdateID(donor.Fname, donor.Lname, donor.Phone,donor.BID, donor.CID,
                donor.LID, donor.Status, donor.Pending, donor.DonationDate, donor.PAID,donor.DID, donor.PhoneStatus);
        }

        [HttpPost]
        [Route("donor_insert/{donor}")]
        public void donor_insert(Models.Donor donor)
        {
            if (donor.DonorGender == "Male")
            {
                TimeSpan s = DateTime.Now.Date - donor.DonationDate.Value;

                if (s.Days >= 90)
                {
                    donor.Pending = true;
                }
                else
                {
                    donor.Pending = false;
                }
            }

            if (donor.DonorGender == "Female")
            {
                TimeSpan s = DateTime.Now.Date - donor.DonationDate.Value;

                if (s.Days >= 120)
                {
                    donor.Pending = true;
                }
                else
                {
                    donor.Pending = false;
                }
            }

            db.Donors_Insert(donor.Fname, donor.Lname, donor.DonorGender, donor.Phone, donor.BID, donor.CID,
                donor.LID, true, donor.Pending, donor.DonationDate, donor.PAID, donor.PhoneStatus);

        }

        /////////////////////////////Partnar
        [HttpGet]
        [Route("getPartnar_Donors/{paid:int}")]
        public List<GetPartnersDonor_Result> getPartnar_Donors(int paid)
        {
            return db.GetPartnersDonor(paid).ToList();
        }

        [HttpPut]
        [Route("insertBloodType/{bid:int}/{did:int}")]
        public void insertBloodType(int bid, int did)
        {
            db.DonorBloodType(bid, did);
        }
       
        ///////////////Needer

        [HttpPost]
        [Route("insertNeeder/{n}")]
        public void insertNeeder(Needer n)
        {
            db.insert_needer(n.Email, n.Fname, n.Lname, n.BID, n.CID, n.Phone);
        }

        [HttpGet]
        [Route("selectneeders")]
        public List<Select_Needer_Result> selectNeeders()
        {
            return db.Select_Needer().ToList();
        }

        [HttpGet]
        [Route("NeederByBlood/{bid:int}")]

        public List<selectNeederByBlood_Result> NeederByBlood(int bid)
        {
            return db.selectNeederByBlood(bid).ToList();
        }

        [HttpGet]
        [Route("NeederByCity/{cid:int}")]

        public List<selectNeederByCity_Result> NeederByCity(int cid)
        {
            return db.selectNeederByCity(cid).ToList();
        }

        [HttpGet]
        [Route("NeederByCityBlood/{cid:int}/{bid:int}")]

        public List<selectNeederByCityBlood_Result> NeederByCityBlood(int cid, int bid)
        {
            return db.selectNeederByCityBlood(cid, bid).ToList();
        }

        //Blood Types 
        [HttpGet]
        [Route("AllBloodTypes")]
        public List<Models.Select_BloodTypes_Result> AllBloodTypes()
        {
            return db.Select_BloodTypes().ToList();
        }
     
        //Cities 
        [HttpGet]
        [Route("ALLCities")]
        public List<Models.Cities_SelectAll_Result>  AllCitiesSelectAllResults()
        {
            return db.Cities_SelectAll().ToList();

        }

        [HttpGet]
        [Route("LocationByCID/{Cid:int}")]
        public List<Models.Locations_SelectAllByCityID_Result>LocationsSelectAllByCity(int Cid)
        {
            return db.Locations_SelectAllByCityID(Cid).ToList();
        }

        [HttpGet]
        [Route("AllDays")]
        public List<Models.Select_DaysALL_Result> DaysAllResults()
        {
            return db.Select_DaysALL().ToList();

        }

        [HttpGet]
        [Route("ALLHospitals")]
        public List<Models.Hospitals_SelectAll_Result> HospitalsSelectAllResults()
        {
            return db.Hospitals_SelectAll().ToList();
        }

        [HttpGet]
        [Route("ALLCommentPerPost/{post_id:int}")]
        public List<Models.Comments_SelectAllByPostID_Result> CommentsSelectPerPostResults(int post_id)
        {
            return db.Comments_SelectAllByPostID(post_id).ToList();
        }

        [HttpPost]
        [Route("ALLCommentPerPost/{comment}")]
        public void Comments_Insert(Comments comment)
        {
            db.Comments_insert(comment.ID, comment.Post_ID, comment.Name, comment.Comment);
        }

    }
}
