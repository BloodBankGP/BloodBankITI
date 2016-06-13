using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using BloodBankITI.Models;
using System.Net.Mail;
using System.Text;
using System.IO;

namespace BloodBankITI.Controllers
{
    public class HomeController : Controller
    {

        BloodBankDBITIEntities db = new BloodBankDBITIEntities();

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
        public ActionResult Logout()
        {
            Session.Abandon();
           return RedirectToAction("Index");
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
            HttpResponseMessage response = client.PostAsJsonAsync("insertNeeder/n", n).Result;
            
            if (response.IsSuccessStatusCode)
            {  
                response = client.PostAsJsonAsync("AskForBlood/"+n.CID+"/"+n.BID+"/"+n.NID,"").Result;
            
                if (response.IsSuccessStatusCode)
                {
                    int count = response.Content.ReadAsAsync<int>().Result;
                    return RedirectToAction("FollowRequest",
                        new
                        {
                            msg =
                                "Your request was sent to " + count +
                                " Donors, Follow this link to know if someone accepted your request and get their data to contact them: " +
                                "http://localhost:7508/Home/RequestsResults/" + n.NID +"/"+ n.Fname + n.Lname
                        });
                }
                else
                    return RedirectToAction("FollowRequest",
                        new
                        {
                            msg = "An error happened and no requests were sent, please try again!"
                        });
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult FollowRequest(string msg)
        {
            return View(msg);
        }

        [HttpGet]
        public ActionResult RequestsResults(int nid, string name)
        {
            return View(nid);
        }

        [HttpGet]
        public ActionResult WallPosts()
        {
            List<PostsComments> PostsComments = new List<Models.PostsComments>();

            List<posts_SelectAll_Result> posts = new List<posts_SelectAll_Result>();
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://www.bloodservice.somee.com/Home/");
            HttpResponseMessage response = client.GetAsync("AllPosts").Result;
            if (response.IsSuccessStatusCode)
            {
                posts = response.Content.ReadAsAsync<List<posts_SelectAll_Result>>().Result;
            }

            foreach (posts_SelectAll_Result item in posts)
            {

                List<Comments_SelectAllByPostID_Result> comments = new List<Comments_SelectAllByPostID_Result>();
                response = client.GetAsync("ALLCommentPerPost/"+item.PID).Result;
                if (response.IsSuccessStatusCode)
                {
                    comments = response.Content.ReadAsAsync<List<Comments_SelectAllByPostID_Result>>().Result;

                }

                PostsComments.Add(new PostsComments() { post = item, comments = comments});
            }
            return View(PostsComments);
        }

        [HttpPost]
        public ActionResult InsertComment (Comments comment)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://www.bloodservice.somee.com/Home/");
            HttpResponseMessage response = client.PostAsJsonAsync("ALLCommentPerPost/comment", comment).Result;
            string result;

            if (response.IsSuccessStatusCode)
            {
                result = "Done";
            }
            else
                result = "Failed to insert comment";

            return RedirectToAction("GetPostByID", new { id = comment.Post_ID });
        }

        [HttpGet]
        public ActionResult GetPostByID (int id)
        {
            PostComments PostComments = new PostComments();

            Posts_GetPostByID_Result post = new Posts_GetPostByID_Result();
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://www.bloodservice.somee.com/Home/");
            HttpResponseMessage response = client.GetAsync("GetPost/"+id).Result;
            if (response.IsSuccessStatusCode)
            {
                post = response.Content.ReadAsAsync<Posts_GetPostByID_Result>().Result;

            }

                List<Comments_SelectAllByPostID_Result> comments = new List<Comments_SelectAllByPostID_Result>();
                response = client.GetAsync("ALLCommentPerPost/" + post.PID).Result;
                if (response.IsSuccessStatusCode)
                {
                    comments = response.Content.ReadAsAsync<List<Comments_SelectAllByPostID_Result>>().Result;

                }

                PostComments = new PostComments() { post = post, comments = comments };
              

            return View(PostComments);
        }

        [HttpGet]
        public ActionResult EmergencyToDay()
        {
            List<Cities_SelectAll_Result> Cities = new List<Cities_SelectAll_Result>();
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://www.bloodservice.somee.com/Home/");
            HttpResponseMessage response = client.GetAsync("ALLCities").Result;
            if (response.IsSuccessStatusCode)
            {
                Cities = response.Content.ReadAsAsync<List<Cities_SelectAll_Result>>().Result;

            }
            return View(Cities);
        }

        [HttpGet]
        public ActionResult NgoRequest ()
        {
            List<Cities_SelectAll_Result> Cities = new List<Cities_SelectAll_Result>();
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://www.bloodservice.somee.com/Home/");
            HttpResponseMessage response = client.GetAsync("ALLCities").Result;
            if (response.IsSuccessStatusCode)
            {
                Cities = response.Content.ReadAsAsync<List<Cities_SelectAll_Result>>().Result;

            }
            return View(Cities);
        }

        [HttpPost]
        public ActionResult NgoRequest (NGO_selectByID_Result ngo)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://www.bloodservice.somee.com/Home/");
            HttpResponseMessage response = client.PostAsJsonAsync("NgoRequest/ngo", ngo).Result;
            string result;

            if (response.IsSuccessStatusCode)
            {
                result = "Done";
            }
            else
                result = "Failed to insert NGO";

            return RedirectToAction("Index");
        }

            [HttpGet]
        public ActionResult Donate()
        {

            List<Cities_SelectAll_Result> cities = new List<Cities_SelectAll_Result>();
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://www.bloodservice.somee.com/Home/");
            HttpResponseMessage response = client.GetAsync("ALLCities").Result;
            if (response.IsSuccessStatusCode)
            {
                cities = response.Content.ReadAsAsync<List<Cities_SelectAll_Result>>().Result;

            }


            List<Select_BloodTypes_Result> bloodTypes = new List<Select_BloodTypes_Result>();

            response = client.GetAsync("AllBloodTypes").Result;
            if (response.IsSuccessStatusCode)
            {
                bloodTypes = response.Content.ReadAsAsync<List<Select_BloodTypes_Result>>().Result;

            }

            donorinsertform donor = new donorinsertform()
            {
                BloodTypesResults = bloodTypes,
                Donor = null,
                CitiesSelectAllResults = cities,
                //LocationsSelectAllByCity = null
            };
            return View(donor);
        }


        [HttpPost]
        public ActionResult Donate(donor_SelectByDID_Result donor)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://www.bloodservice.somee.com/Home/");
            HttpResponseMessage response = client.PostAsJsonAsync("donor_insert/donor", donor).Result;
            string result;

            if (response.IsSuccessStatusCode)
            {
                result = "Done";  
                string id = response.Content.ReadAsStringAsync().Result;
                if (donor.BID == null)
                {
                    return RedirectToAction("selectPartner", new { id = Int32.Parse(id) });
                }
            }
            else
                result = "Failed to insert Donor";
            

            return RedirectToAction("Index");
        }

       [HttpGet] 
        public ActionResult selectPartner(int id) //donor id
        {
            
            donor_SelectByDID_Result donor = new donor_SelectByDID_Result();
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://www.bloodservice.somee.com/Home/");
            HttpResponseMessage response = client.GetAsync("ViewProfile/" + id).Result;
            if (response.IsSuccessStatusCode)
            {
                donor = response.Content.ReadAsAsync<donor_SelectByDID_Result>().Result;
            }


            List<Partner_SelectByCity_Result> partner = new List<Partner_SelectByCity_Result>();
            response = client.GetAsync("getPartnar_City/" + donor.CID).Result;
            if (response.IsSuccessStatusCode)
            {
                partner = response.Content.ReadAsAsync<List<Partner_SelectByCity_Result>>().Result;
            }


            donorpartner donorpartner = new donorpartner()
            {
                donor =donor,
                partnerSelectByCity = partner
            }
                ;

            return View(donorpartner);

            
        }

        [HttpPost]
        public ActionResult selectPartner(Donor  donor) 
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://www.bloodservice.somee.com/Home/");
            HttpResponseMessage response = client.PostAsJsonAsync("donorupdate/donor", donor).Result;
            if (response.IsSuccessStatusCode)
            {
                PartnersStatestic partnersStatestic = new PartnersStatestic()
                {
                    PID = Int32.Parse(donor.PAID.ToString()),
                    DID = donor.DID,
                    BID = null,
                    Insert_Date = null

                };

                response = client.PostAsJsonAsync("PartnersStatesticInsert/PartnersStatestic", partnersStatestic).Result;

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    return RedirectToAction("selectPartner", new { id = donor.DID });
                }
            }
            else
                return RedirectToAction("selectPartner", new { id = donor.DID });    
        }

        //Contact Us
        [HttpPost]
        public ActionResult Contact(Contact contact)
        {
            db.ContactInsert(contact.FName, contact.LName, contact.Age, contact.City, contact.Email, contact.Msg);
            return RedirectToAction("Thanks");
        }

        [HttpGet]
        public ActionResult Thanks()
        {
            return View();
        }
    }
}