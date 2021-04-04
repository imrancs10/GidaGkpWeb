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
using ICSharpCode.SharpZipLib.Zip;

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
                var result = _details.GetNoticeById(NoticeId.Value);
                result.NoticeDocumentFile = null;
                ViewData["ApplicantData"] = result;
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
                data.NoticeDocumentFile = null;
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

        [HttpGet]
        public FileResult DownloadAttachment(string applicationId)
        {
            ApplicantDetails detail = new ApplicantDetails();
            var documentData = detail.GetApplicantUploadDocDetail(Convert.ToInt32(applicationId));
            if (documentData == null)
            {
                SetAlertMessage("Something went wrong in downloading the attchment, try again later", "Downlaod Attachment");
                return null;
            }
            //byte[] bytes = noticeData.NoticeDocumentFile;
            //return File(bytes, System.Net.Mime.MediaTypeNames.Application.Octet, noticeData.NoticeDocumentName);
            var fileName = string.Format("{0}_files.zip", DateTime.Today.Date.ToString("dd-MM-yyyy") + "_UserId_" + documentData.UserId);
            var temppath = Server.MapPath("~/TempFiles/");
            if (!Directory.Exists(temppath))
            {
                Directory.CreateDirectory(temppath);
            }
            var tempOutPutPath = Path.Combine(temppath, fileName);
            using (ZipOutputStream s = new ZipOutputStream(System.IO.File.Create(tempOutPutPath)))
            {
                s.SetLevel(9); // 0-9, 9 being the highest compression  

                if (documentData.ProjectReport != null)
                {
                    ZipEntry entry = new ZipEntry(documentData.ProjectReportFileName);
                    entry.DateTime = DateTime.Now;
                    entry.IsUnicodeText = true;
                    s.PutNextEntry(entry);
                    s.Write(documentData.ProjectReport, 0, documentData.ProjectReport.Length);
                }

                if (documentData.ProposedPlanLandUses != null)
                {
                    ZipEntry entry = new ZipEntry(documentData.ProposedPlanLandUsesFileName);
                    entry.DateTime = DateTime.Now;
                    entry.IsUnicodeText = true;
                    s.PutNextEntry(entry);
                    s.Write(documentData.ProposedPlanLandUses, 0, documentData.ProposedPlanLandUses.Length);
                }

                if (documentData.Memorendum != null)
                {
                    ZipEntry entry = new ZipEntry(documentData.MemorendumFileName);
                    entry.DateTime = DateTime.Now;
                    entry.IsUnicodeText = true;
                    s.PutNextEntry(entry);
                    s.Write(documentData.Memorendum, 0, documentData.Memorendum.Length);
                }

                if (documentData.ScanPAN != null)
                {
                    ZipEntry entry = new ZipEntry(documentData.ScanPANFileName);
                    entry.DateTime = DateTime.Now;
                    entry.IsUnicodeText = true;
                    s.PutNextEntry(entry);
                    s.Write(documentData.ScanPAN, 0, documentData.ScanPAN.Length);
                }

                if (documentData.ScanAddressProof != null)
                {
                    ZipEntry entry = new ZipEntry(documentData.ScanAddressProofFileName);
                    entry.DateTime = DateTime.Now;
                    entry.IsUnicodeText = true;
                    s.PutNextEntry(entry);
                    s.Write(documentData.ScanAddressProof, 0, documentData.ScanAddressProof.Length);
                }

                if (documentData.BalanceSheet != null)
                {
                    ZipEntry entry = new ZipEntry(documentData.BalanceSheetFileName);
                    entry.DateTime = DateTime.Now;
                    entry.IsUnicodeText = true;
                    s.PutNextEntry(entry);
                    s.Write(documentData.BalanceSheet, 0, documentData.BalanceSheet.Length);
                }

                if (documentData.ITReturn != null)
                {
                    ZipEntry entry = new ZipEntry(documentData.ITReturnFileName);
                    entry.DateTime = DateTime.Now;
                    entry.IsUnicodeText = true;
                    s.PutNextEntry(entry);
                    s.Write(documentData.ITReturn, 0, documentData.ITReturn.Length);
                }

                if (documentData.ExperienceProof != null)
                {
                    ZipEntry entry = new ZipEntry(documentData.ExperienceProofFileName);
                    entry.DateTime = DateTime.Now;
                    entry.IsUnicodeText = true;
                    s.PutNextEntry(entry);
                    s.Write(documentData.ExperienceProof, 0, documentData.ExperienceProof.Length);
                }

                if (documentData.ApplicantEduTechQualification != null)
                {
                    ZipEntry entry = new ZipEntry(documentData.ApplicantEduTechQualificationFileName);
                    entry.DateTime = DateTime.Now;
                    entry.IsUnicodeText = true;
                    s.PutNextEntry(entry);
                    s.Write(documentData.ApplicantEduTechQualification, 0, documentData.ApplicantEduTechQualification.Length);
                }

                if (documentData.PreEstablishedIndustriesDoc != null)
                {
                    ZipEntry entry = new ZipEntry(documentData.PreEstablishedIndustriesDocFileName);
                    entry.DateTime = DateTime.Now;
                    entry.IsUnicodeText = true;
                    s.PutNextEntry(entry);
                    s.Write(documentData.PreEstablishedIndustriesDoc, 0, documentData.PreEstablishedIndustriesDoc.Length);
                }

                if (documentData.FinDetailsEstablishedIndustries != null)
                {
                    ZipEntry entry = new ZipEntry(documentData.FinDetailsEstablishedIndustriesFileName);
                    entry.DateTime = DateTime.Now;
                    entry.IsUnicodeText = true;
                    s.PutNextEntry(entry);
                    s.Write(documentData.FinDetailsEstablishedIndustries, 0, documentData.FinDetailsEstablishedIndustries.Length);
                }

                if (documentData.OtherDocForProposedIndustry != null)
                {
                    ZipEntry entry = new ZipEntry(documentData.OtherDocForProposedIndustryFileName);
                    entry.DateTime = DateTime.Now;
                    entry.IsUnicodeText = true;
                    s.PutNextEntry(entry);
                    s.Write(documentData.OtherDocForProposedIndustry, 0, documentData.OtherDocForProposedIndustry.Length);
                }

                if (documentData.ScanCastCert != null)
                {
                    ZipEntry entry = new ZipEntry(documentData.ScanCastCertFileName);
                    entry.DateTime = DateTime.Now;
                    entry.IsUnicodeText = true;
                    s.PutNextEntry(entry);
                    s.Write(documentData.ScanCastCert, 0, documentData.ScanCastCert.Length);
                }

                if (documentData.ScanID != null)
                {
                    ZipEntry entry = new ZipEntry(documentData.ScanIDFileName);
                    entry.DateTime = DateTime.Now;
                    entry.IsUnicodeText = true;
                    s.PutNextEntry(entry);
                    s.Write(documentData.ScanID, 0, documentData.ScanID.Length);
                }

                if (documentData.BankVerifiedSignature != null)
                {
                    ZipEntry entry = new ZipEntry(documentData.BankVerifiedSignatureFileName);
                    entry.DateTime = DateTime.Now;
                    entry.IsUnicodeText = true;
                    s.PutNextEntry(entry);
                    s.Write(documentData.BankVerifiedSignature, 0, documentData.BankVerifiedSignature.Length);
                }

                if (documentData.DocProofForIndustrialEstablishedOutsideGida != null)
                {
                    ZipEntry entry = new ZipEntry(documentData.DocProofForIndustrialEstablishedOutsideGidaFileName);
                    entry.DateTime = DateTime.Now;
                    entry.IsUnicodeText = true;
                    s.PutNextEntry(entry);
                    s.Write(documentData.DocProofForIndustrialEstablishedOutsideGida, 0, documentData.DocProofForIndustrialEstablishedOutsideGida.Length);
                }

                if (documentData.ApplicantPhoto != null)
                {
                    ZipEntry entry = new ZipEntry(documentData.ApplicantPhotoFileName);
                    entry.DateTime = DateTime.Now;
                    entry.IsUnicodeText = true;
                    s.PutNextEntry(entry);
                    s.Write(documentData.ApplicantPhoto, 0, documentData.ApplicantPhoto.Length);
                }

                if (documentData.ApplicantSignature != null)
                {
                    ZipEntry entry = new ZipEntry(documentData.ApplicantSignatureFileName);
                    entry.DateTime = DateTime.Now;
                    entry.IsUnicodeText = true;
                    s.PutNextEntry(entry);
                    s.Write(documentData.ApplicantSignature, 0, documentData.ApplicantSignature.Length);
                }

                s.Finish();
                s.Flush();
                s.Close();
            }
            byte[] finalResult = System.IO.File.ReadAllBytes(tempOutPutPath);
            if (System.IO.File.Exists(tempOutPutPath))
                System.IO.File.Delete(tempOutPutPath);

            if (finalResult == null || !finalResult.Any())
                throw new Exception(String.Format("No Files found with Image"));
            return File(finalResult, "application/zip", fileName);
        }
        public ActionResult Logout()
        {
            Session.Abandon();
            Session.Clear();
            return RedirectToAction("AdminLogin", "Login");
        }
    }
}