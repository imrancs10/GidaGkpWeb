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
            ViewData["ApplicantData"] = _details.GetApplicantUser();
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

        public ActionResult PropertyDashboard()
        {
            return View();
        }
        public ActionResult GrievancesDashboard()
        {
            return View();
        }
        [HttpPost]
        [ValidateInput(false)]
        public FileResult ExportApplicantUser(string GridHtml)
        {
            return File(Encoding.ASCII.GetBytes(GridHtml), "application/vnd.ms-excel", "Total Active Applicant User.xls");
        }

        [HttpPost]
        [ValidateInput(false)]
        public FileResult ExportFormSubmitted(string GridHtml)
        {
            return File(Encoding.ASCII.GetBytes(GridHtml), "application/vnd.ms-excel", "Plot Registered Only.xls");
        }

        [HttpPost]
        [ValidateInput(false)]
        public FileResult ExportFormCompleted(string GridHtml)
        {
            return File(Encoding.ASCII.GetBytes(GridHtml), "application/vnd.ms-excel", "Payment Completed.xls");
        }
        public ActionResult Notice(int? NoticeId = null)
        {
            if (NoticeId != null)
            {
                AdminDetails _details = new AdminDetails();
                ViewData["ApplicantData"] = _details.GetNoticeById(NoticeId.Value);
            }
            return View();
        }
        public ActionResult NoticeList()
        {
            AdminDetails _details = new AdminDetails();
            ViewData["ApplicantData"] = _details.GetNoticeList();
            return View();
        }
        [HttpPost]
        public JsonResult GetNoticeDetail(int NoticeId)
        {
            AdminDetails _details = new AdminDetails();
            if (NoticeId > 0)
            {
                var data = _details.GetNoticeById(NoticeId);
                return Json(data, JsonRequestBehavior.AllowGet);
            }
            return null;
        }
        [HttpPost]
        public ActionResult SaveNotice(HttpPostedFileBase Document, string NoticeType, string Title, string DepartmentNotice, string NoticeDate, string NewTag, string Publish, int? NoticeId)
        {
            AdminNotice notice = new AdminNotice();
            if (Document != null && Document.ContentLength > 0)
            {
                notice.NoticeDocumentFile = new byte[Document.ContentLength];
                Document.InputStream.Read(notice.NoticeDocumentFile, 0, Document.ContentLength);
                notice.NoticeDocumentName = Document.FileName;
                notice.NoticeDocumentFileType = Document.ContentType;
            }
            notice.NoticeTypeId = Convert.ToInt32(NoticeType);
            notice.Notice_title = Title;
            notice.Department = !string.IsNullOrEmpty(DepartmentNotice) ? Convert.ToInt32(DepartmentNotice) : 0;
            if (NoticeDate != "")
                notice.Notice_Date = Convert.ToDateTime(NoticeDate);
            if (NewTag == "on")
                notice.NoticeNewTag = true;
            else
                notice.NoticeNewTag = false;
            if (Publish == "on")
                notice.IsActive = true;
            else
                notice.IsActive = false;

            if (NoticeId <= 0 || NoticeId == null)
            {
                notice.CreatedBy = UserData.UserId;
                notice.CreationDate = DateTime.Now;
            }
            else
            {
                notice.Id = NoticeId.Value;
            }

            AdminDetails _details = new AdminDetails();
            var result = _details.SaveNotice(notice);
            if (result == Enums.CrudStatus.Saved)
                SetAlertMessage("Notice has been Saved", "Notice Save");
            else
                SetAlertMessage("Notice Saving failed", "Notice Save");
            if (NoticeId != null && NoticeId > 0)
            {
                return RedirectToAction("NoticeList");
            }
            else
            {
                return RedirectToAction("Notice");
            }

        }
        public ActionResult Logout()
        {
            Session.Abandon();
            Session.Clear();
            return RedirectToAction("AdminLogin", "Login");
        }
    }
}