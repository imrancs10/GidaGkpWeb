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
            return View();
        }
        public ActionResult ApplicantFormSubmitted()
        {
            return View();
        }
        public ActionResult ApplicantTransactionCompleted()
        {
            return View();
        }
        
    }
}