using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using BloodBankService.Models;

namespace BloodBankService.Controllers
{
    public class HomeController : ApiController
    {
        Models.BloodBankDBITIEntities db = new Models.BloodBankDBITIEntities();


        /// <summary>
        ///Anonymous
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public List<Models.posts_SelectAll_Result> AllPosts ()
        {
            return  db.posts_SelectAll().ToList();   
        }

        [HttpPost]
        public void CreatePost(Models.Post post)
        {
            db.posts_InsertPost(post.Post1 ,post.Phone,post.BID,post.CID,post.Name,post.Periodic);
        }

        [HttpGet]
        public List<Models.posts_SelectByBID_CID_Result> AllPostsBySearch(int bid , int cid)
        {
            return db.posts_SelectByBID_CID(bid , cid).ToList();
        }
        [HttpGet]
        public List<Models.Donors_SelectID_Result> DonorsSelectByID(int cid, int bid, int lid)
        {
            return db.Donors_SelectID(cid, bid, lid).ToList();
        }

        [HttpGet]
        public List<Models.NGO_select_Result> NgoSelectAllByCity(int cid)
        {
            return db.NGO_select(cid).ToList();
        }

        [HttpGet]
        public Models.NGO_selectByID_Result NgoSelectById(int nid)
        {
            return db.NGO_selectByID(nid).FirstOrDefault();
        }

        [HttpPost]
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
        public Models.CheckLogin_Result CheckLogin(string username, string password)
        {
            return db.CheckLogin(username, password).FirstOrDefault();
        }

        [HttpPost]
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
        public EmergencyToday_Result EmergencyToday(int cityid, int dayid)
        {
            return db.EmergencyToday(dayid, cityid).FirstOrDefault();
        }


        ////////////////////////////Donor
        [HttpPut]
        public void UpdatePending(int donorid)
        {
            db.donor_updatepending(donorid);
        }

        [HttpPut]
        public void donor_update(Donor donor)
        {
            db.Donors_UpdateID(donor.Fname, donor.Lname, donor.Phone,donor.BID, donor.CID,
                donor.LID, donor.Status, donor.Pending, donor.DonationDate, donor.PAID,donor.DID, donor.PhoneStatus);
        }

        /////////////////////////////Partnar
        [HttpGet]
        public List<GetPartnersDonor_Result> getPartnar_Donors(int paid)
        {
            return db.GetPartnersDonor(paid).ToList();
        }

        [HttpPut]
        public void insertBloodType(int bid, int did)
        {
            db.DonorBloodType(bid, did);
        }
        
    }
}
