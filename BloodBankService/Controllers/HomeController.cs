using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.Xml;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;
using BloodBankService.Models;
using System.Web.Http.Cors;
using System.Net.Http;
using System.Text;
using System.Net;
using System.Net.Http.Headers;
using System.Data.Entity.Core.Objects;

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
        public string NgoRequest(NGO_selectByID_Result ngo)
        {
            List<string> usernames = db.CheckUsernames().ToList();
            int result = 1;
            foreach (var u in usernames)
            {
                if (ngo.Username == u)
                    result = 0;
            }

            if (result == 1)
            {
                if (db.NGO_insert(ngo.Name, ngo.CID, ngo.Phone, ngo.Address, ngo.Username, ngo.Password, ngo.Fb, ngo.Website) == 1)
                {
                    return "Ngo Inserted";
                }
                else
                {
                    return "Something went wrong";
                }
            }
            else
            {
                return "This username is already taken";
            }
        }
        [HttpPut]
        [Route("NGoUpdate/{Ngo}")]
        public void NGO_update(NGO_selectByID_Result Ngo)
        {
            db.NGO_update(Ngo.NID, Ngo.Name,Ngo.CID,Ngo.Phone, Ngo.Address,Ngo.Username,Ngo.Password,Ngo.Fb,Ngo.Website);

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
                    donor.LID, true, donor.Pending, donor.DonationDate, donor.PAID, login.UserName, login.Password);

                db.Login_insert(login.UserName, login.Password, 2, Int32.Parse(id.ToString()));

                return "Inserted";
            }
            else
            {
                return "Invalid Name";
            }            
        }


        ////////////////////////////Donor
        [HttpGet]
        [Route("ViewProfile/{did:int}")]
        public donor_SelectByDID_Result selectDonorByID(int did)
        {
            return db.donor_SelectByDID(did).FirstOrDefault();
        }
        
        [HttpPost]
        [Route("updatePending/{donor}")]
        public void UpdatePending(Donor donor)
        {
            db.donor_updatepending(donor.DID);
        }

        //[HttpGet]
        //[Route("SelectDonorByDID/{donorid}")]
        //public donor_SelectByDID_Result SelectDonorByID(int donorid)
        //{
        //   return db.donor_SelectByDID(donorid).FirstOrDefault();
        //}

        [HttpPost]
        [Route ("donorupdate/{donor}")]
        public void donor_update(donor_SelectByDID_Result donor)
        {
            db.Donors_UpdateID(donor.Fname, donor.Lname, donor.Phone, donor.BID, donor.CID, donor.LID, donor.DID, donor.DonorGender,donor.Username,donor.Password);
        }

        [HttpPost]
        [Route("donor_insert/{donor}")]
        public HttpResponseMessage donor_insert(donor_SelectByDID_Result donor)
        {
            if (donor.DonorGender == "Male")
            {
                TimeSpan s = DateTime.Now.Date - donor.DonationDate.Value;

                if (s.Days >= 90)
                {
                    donor.Pending = false;
                }
                else
                {
                    donor.Pending = true;
                }
            }

            if (donor.DonorGender == "Female")
            {
                TimeSpan s = DateTime.Now.Date - donor.DonationDate.Value;

                if (s.Days >= 120)
                {
                    donor.Pending = false;
                }
                else
                {
                    donor.Pending = true;
                }
            }

            var id = db.Donors_Insert(donor.Fname, donor.Lname, donor.DonorGender, donor.Phone, donor.BID, donor.CID,
                  donor.LID, true, donor.Pending, donor.DonationDate, donor.PAID, donor.Username, donor.Password);

            Donors_Insert_Result don = new Donors_Insert_Result(){ id = id.FirstOrDefault().id.Value};

            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK,"value");
            response.Content = new StringContent(don.id.Value.ToString(), Encoding.Unicode);

         return response;
        }

        /////////////////////////////Partnar
        [HttpGet]
        [Route("getPartnar_Donors/{paid:int}")]
        public List<GetPartnersDonor_Result> getPartnar_Donors(int paid)
        {
            return db.GetPartnersDonor(paid).ToList();
        }
        [HttpGet]
        [Route("PartnerByID/{Pid:int}")]
        public Partners_select_Result partnersbyId(int Pid)
        {
            return db.Partners_select(Pid).FirstOrDefault();
           
        }

        [HttpPost]
        [Route("donorupdatepartner/{donor}")]
        public void donor_updatePartner(donor_SelectByDID_Result donor)
        {
            db.Donor_insertPartner(donor.PAID, donor.DID);
        }

        [HttpPost]
        [Route("insertBloodType/{bid:int}/{did:int}")]
        public void insertBloodType(int bid, int did)
        {
            db.DonorBloodType(bid, did);
        }
       
        ///////////////Needer

        [HttpPost]
        [Route("insertNeeder/{n}")]
        public HttpResponseMessage insertNeeder(Needer n)
        {
            var id = db.insert_needer(n.Email, n.Fname, n.Lname, n.BID, n.CID, n.Phone);

            insert_needer_Result needer = new insert_needer_Result() { needer_id = id.FirstOrDefault().needer_id.Value };

            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK, "value");
            response.Content = new StringContent(needer.needer_id.Value.ToString(), Encoding.Unicode);
            return response;
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
            db.Comments_insert(comment.Post_ID, comment.Name, comment.Comment);
        }

        [HttpGet]
        [Route("GetPost/{id:int}")]
        public Posts_GetPostByID_Result Comments_Insert(int id)
        {
            return db.Posts_GetPostByID(id).FirstOrDefault();
        }

        [HttpGet]
        [Route("EmergencyToday/{id:int}")]
        public EmergencySelectCityDay_Result EmergencyToday (int id)
        {
            return db.EmergencySelectCityDay ((int)DateTime.Now.DayOfWeek,id).FirstOrDefault();
        }

        [HttpPost]
        //////Ask For Blood
        [Route("AskForBlood/{cid:int}/{bid:int}/{nid:int}")]
        public HttpResponseMessage AskForBlood(int cid, int bid, int nid)
        {
            List<AskForBlood_Result> donors = db.AskForBlood(bid, cid).ToList();

            var count = (ObjectResult<Needer_DonorInsert_Result>)null;
            foreach (var d in donors)
            {
                count = db.Needer_DonorInsert(nid, bid, cid, d.DID);
            }
            Needer_DonorInsert_Result needonor = new Needer_DonorInsert_Result() { DonorsNo = count.FirstOrDefault().DonorsNo.Value };

            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK, "value");

            response.Content = new StringContent(needonor.DonorsNo.Value.ToString(), Encoding.Unicode);

            return response;
        }

        [HttpGet]
        [Route("NeederRequests/{nid:int}/{name}")]
        public List<Needer_DonorAccepted_Result> NeederRequests(int nid, string name)
        {
            return db.Needer_DonorAccepted(nid,name).ToList();
        }

        [HttpGet]
        /////Donor Requests
        [Route("DonorRequests/{did:int}")]
        public List<DonorRequests_Result> DonorRequests(int did)
        {
            return db.DonorRequests(did).ToList();
        }

        [HttpPost]
        /////Donor Requests
        [Route("DonorAcceptRequest/{did:int}/{nid:int}")]
        public void DonorAcceptRequest(int did,int nid)
        {
            db.AcceptRequest(nid, did);
        }

        [HttpPost]
        /////Donor Requests
        [Route("DonorCancelRequest/{did:int}/{nid:int}")]
        public void DonorCancelRequest(int did, int nid)
        {
            db.CancelRequest(nid, did);
        }

        [HttpGet]
        [Route("getPartnar_City/{cid:int}")]
        public List<Partner_SelectByCity_Result> getPartnar_City(int cid)
        {
            return db.Partner_SelectByCity(cid).ToList();
        }

        [HttpPost]
        [Route("PartnersStatesticInsert/{PartnersStatestic}")]
        public void PartnersStatesticInsert(PartnersStatestic PartnersStatestic)
        {
            db.PartnerStatestics_insert(PartnersStatestic.PID, PartnersStatestic.DID);
        }

        [HttpGet]
        [Route("CheckUsername/{username}")]
        public int getPartnar_City(string username)
        {
            CheckName_Result check = db.CheckName(username).FirstOrDefault();

            if (check != null)
                return 1;
            else
                return 0;
        }
    }
}
