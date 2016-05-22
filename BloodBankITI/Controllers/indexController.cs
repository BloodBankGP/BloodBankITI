using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BloodBankITI.Controllers
{
    public class indexController : Controller
    {
        [HttpGet]
        // GET: Index
        public ActionResult Index()
        {
            return View();
        }
    }
}