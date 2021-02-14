using GidaGkpWeb.BAL;
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

        [HttpGet]
        public FileResult GetNoticeFile(int noticeId)
        {
            AdminDetails detail = new AdminDetails();
            var noticeData = detail.GetNoticeById(noticeId);
            byte[] bytes = noticeData.NoticeDocumentFile;
            //var response = new FileContentResult(bytes, "text/csv");
            //response.FileDownloadName = noticeData.DepartmentName;
            //Response.Clear();
            //Response.AddHeader("content-disposition", "inline; filename=" + noticeData.DepartmentName);
            //Response.ContentType = noticeData.NoticeDocumentFileType;
            //Response.OutputStream.Write(bytes, 0, bytes.Length);
            //Response.End();

            return File(bytes, noticeData.NoticeDocumentFileType, noticeData.DepartmentName);
        }

    }
}