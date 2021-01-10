using DataLayer;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using GidaGkpWeb.Global;
using GidaGkpWeb.Models.Masters;

namespace GidaGkpWeb.Controllers
{
    public class MastersController : CommonController
    {
        public ActionResult Dashboard()
        {
            return View();
        }

        public ActionResult ApplicantDashboard()
        {
            return View();
        }

        public ActionResult Logout()
        {
            Session.Abandon();
            Session.Clear();
            return RedirectToAction("ApplicantLogin", "Login");
        }
    }
}