using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

namespace BloodBankService.Controllers
{
    public class HomeController : ApiController
    {
        Models.BloodBankDBITIEntities db = new Models.BloodBankDBITIEntities();

        [HttpGet]
        public List<Models.posts_SelectAll_Result> AllPosts ()
        {
            var result = db.posts_SelectAll();
        }
    }
}
