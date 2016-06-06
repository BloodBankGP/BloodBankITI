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
            HttpResponseMessage response = client.PostAsJsonAsync("insertNeeder/n", n).Result;
            string result;
            
            if (response.IsSuccessStatusCode)
            {
                result = "Done";
            }
            else
                result = "Failed to insert Needer";
            return RedirectToAction("Index");
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
        public ActionResult NgoRequest (NGO ngo)
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
        public ActionResult Donate(Donor donor)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://www.bloodservice.somee.com/Home/");
            HttpResponseMessage response = client.PostAsJsonAsync("donor_insert/donor", donor).Result;
            string result;

            if (response.IsSuccessStatusCode)
            {
                result = "Done";
            }
            else
                result = "Failed to insert Donor";
            if (donor.BID == 0)
            {
                return RedirectToAction("selectPartner",new {id=donor.DID});
            }
    
            return RedirectToAction("Index");
        }

        [HttpGet] 
        public ActionResult selectPartner(int id)












        [HttpPost]
    }
}