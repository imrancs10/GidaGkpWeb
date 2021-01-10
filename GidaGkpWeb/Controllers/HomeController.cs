using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GidaGkpWeb.Controllers
{
    public class HomeController : CommonController
    {
        // GET: Home
        public ActionResult Dashboard()
        {
            return View();
        }

        public ActionResult AboutGida()
        {
            return View();
        }

        public ActionResult CitizenCharter()
        {
            return View();
        }

        public ActionResult EmployeeList()
        {
            return View();
        }

        public ActionResult GidaMember()
        {
            return View();
        }
        public ActionResult OrganizationChart()
        {
            return View();
        }
        public ActionResult PDirectory()
        {
            return View();
        }

        
    }
}