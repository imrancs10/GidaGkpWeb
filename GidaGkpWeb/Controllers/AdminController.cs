using DataLayer;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using GidaGkpWeb.Global;
using GidaGkpWeb.Models;
using GidaGkpWeb.BAL.Login;
using GidaGkpWeb.BAL;
using CCA.Util;
using System.Collections.Specialized;
using GidaGkpWeb.Infrastructure.Utility;
using GidaGkpWeb.Infrastructure.Authentication;
using System.Web.Script.Serialization;
using System.Web.Security;
using System.Net.Http;
using System.Net;
using System.Text;
using System.IO;

namespace GidaGkpWeb.Controllers
{
    [AdminSessionTimeout]
    public class AdminController : CommonController
    {
        public ActionResult Dashboard()
        {
            return View();
        }

        public ActionResult ApplicantUser()
        {
            AdminDetails _details = new AdminDetails();
            ViewData["ApplicantData"] = _details.GetApplicantUserDetail();
            return View();
        }
        public ActionResult ActivateDeActivateUser(int userId)
        {
            if (userId > 0)
            {
                AdminDetails _details = new AdminDetails();
                var result = _details.ActivateDeActivateUser(userId);
                if (result == Enums.CrudStatus.Saved)
                    SetAlertMessage("User has been Activated/DeActivated", "User Action");
                else
                    SetAlertMessage("User has not been Activated/DeActivated", "User Action");
                return RedirectToAction("ApplicantUser");
            }
            return RedirectToAction("ApplicantUser");
        }

        public ActionResult ApplicantFormSubmitted()
        {
            AdminDetails _details = new AdminDetails();
            ViewData["ApplicantData"] = _details.GetApplicantUserDetail().Where(x => x.ApplicationNumber != "" && x.PaidAmount == "").ToList();
            return View();
        }
        public ActionResult ApplicantTransactionCompleted()
        {
            AdminDetails _details = new AdminDetails();
            ViewData["ApplicantData"] = _details.GetApplicantUserDetail().Where(x => x.PaidAmount != "").ToList();
            return View();
        }

    }
}