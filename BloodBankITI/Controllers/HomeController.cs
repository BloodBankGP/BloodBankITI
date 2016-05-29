using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using BloodBankITI.Models;

namespace BloodBankITI.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

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

            return RedirectToAction("WallPosts");
        }
    }
}