﻿using DataLayer;
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

namespace GidaGkpWeb.Controllers
{
    public class ApplicantController : CommonController
    {
        CCACrypto ccaCrypto = new CCACrypto();
        string workingKey = "AB986C5175D944E600B2567AEDC18A04";//put in the 32bit alpha numeric key in the quotes provided here 	
        string ccaRequest = "";
        public string strMerchantId = "310096";
        public string strEncRequest = "";
        public string strAccessCode = "AVHE00IA10BD01EHDB";// put the access key in the quotes provided here.
        public ActionResult Dashboard()
        {
            //show gida logo and info
            return View();
        }

        public ActionResult ApplyForPlot()
        {
            //Squire box colofull
            return View();
        }

        public ActionResult ViewAdvertisement()
        {
            //Table with scheme Advertisement
            return View();
        }

        public ActionResult ApplicantDashboard(int? applicationId = null)
        {
            if (applicationId != null && applicationId > 0)
            {
                UserData.ApplicationId = applicationId.Value;
                return View();
            }
            else
            {
                ApplicantDetails _details = new ApplicantDetails();
                var dataCount = _details.GetUserApplicationCount(((CustomPrincipal)User).Id);
                if (dataCount > 0)
                {
                    return RedirectToAction("ApplicantViewApplication");
                }
                else
                {
                    return View();
                }
            }

        }

        public ActionResult ApplicantViewApplication()
        {
            ApplicantDetails _details = new ApplicantDetails();
            ViewData["ApplicationData"] = _details.GetUserApplicationDetail(((CustomPrincipal)User).Id);
            return View();
        }

        [HttpPost]
        public JsonResult SavePlotDetail(string AppliedFor, string SchemeType, string PlotRange, string SchemeName, string plotArea, string SectorName, string SectorDescription, string EstimatedRate, string PaymemtSchedule, string TotalInvestment, string ApplicationFee, string EarnestMoneyDeposite, string GST, string NetAmount, string TotalAmount, string IndustryOwnershipType, string UnitName, string Name, string dob, string PresentAddress, string PermanentAddress, string RelationshipStatus)
        {
            ApplicantDetails _details = new ApplicantDetails();
            if (string.IsNullOrEmpty(AppliedFor) || string.IsNullOrEmpty(SchemeType))
            {
                //SetAlertMessage("Incomplete Detail", "Error");
                return null;
            }
            var AppNumber = _details.SavePlotDetail(((CustomPrincipal)User).Id, AppliedFor, SchemeType, PlotRange, SchemeName, plotArea, SectorName, SectorDescription, EstimatedRate, PaymemtSchedule, TotalInvestment, ApplicationFee, EarnestMoneyDeposite, GST, NetAmount, TotalAmount, IndustryOwnershipType, UnitName, Name, dob, PresentAddress, PermanentAddress, RelationshipStatus);
            if (AppNumber != "Error")
                Session["ApplicationNumber"] = AppNumber;
            return Json(AppNumber, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public JsonResult SaveApplicantDetails(string FullName, string FName, string MName, string SName, string DOB, string Gender, string Category, string Nationality, string AdhaarNo, string PAN, string MobileNo, string Phone, string Email, string Religion, string SubCategory, string CAddress, string PAddress, string IdentityProof, string ResidentialProof)
        {
            ApplicantDetails _details = new ApplicantDetails();
            if (string.IsNullOrEmpty(FullName) || string.IsNullOrEmpty(FName))
            {
                //SetAlertMessage("Incomplete Detail", "Error");
                return null;
            }
            return Json(CrudResponse(_details.SaveApplicantDetail(((CustomPrincipal)User).Id, FullName, FName, MName, SName, DOB, Gender, Category, Nationality, AdhaarNo, PAN, MobileNo, Phone, Email, Religion, SubCategory, CAddress, PAddress, IdentityProof, ResidentialProof)), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult SaveProjectDetails(string ProposedIndustryType, string ProjectEstimatedCost, string ProposedCoveredArea, string ProposedOpenArea, string PurpuseOpenArea, string ProposedInvestmentLand, string ProposedInvestmentBuilding, string ProposedInvestmentPlant, string FumesNatureQuantity, string LiquidQuantity, string LiquidChemicalComposition, string SolidQuantity, string SolidChemicalComposition, string GasQuantity, string GasChemicalComposition, string PowerRequirement, string FirstYearNoOfTelephone, string FirstYearNoOfFax, string UltimateNoOfTelephone, string UltimateNoOfFax, string Skilled, string UnSkilled)
        {
            ApplicantDetails _details = new ApplicantDetails();
            if (string.IsNullOrEmpty(ProposedIndustryType) || string.IsNullOrEmpty(ProjectEstimatedCost))
            {
                //SetAlertMessage("Incomplete Detail", "Error");
                return null;
            }
            return Json(CrudResponse(_details.SaveProjectDetail(((CustomPrincipal)User).Id, ProposedIndustryType, ProjectEstimatedCost, ProposedCoveredArea, ProposedOpenArea, PurpuseOpenArea, ProposedInvestmentLand, ProposedInvestmentBuilding, ProposedInvestmentPlant, FumesNatureQuantity, LiquidQuantity, LiquidChemicalComposition, SolidQuantity, SolidChemicalComposition, GasQuantity, GasChemicalComposition, PowerRequirement, FirstYearNoOfTelephone, FirstYearNoOfFax, UltimateNoOfTelephone, UltimateNoOfFax, Skilled, UnSkilled)), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult SaveBankDetail(string BankAccountName, string BankAccountNo, string BankName, string BranchName, string BranchAddress, string IFSCCode)
        {
            ApplicantDetails _details = new ApplicantDetails();
            if (string.IsNullOrEmpty(BankAccountName) || string.IsNullOrEmpty(BankAccountNo))
            {
                //SetAlertMessage("Incomplete Detail", "Error");
                return null;
            }
            return Json(CrudResponse(_details.SaveBankDetail(((CustomPrincipal)User).Id, BankAccountName, BankAccountNo, BankName, BranchName, BranchAddress, IFSCCode)), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult SaveApplicantDocument(HttpPostedFileBase ProjectReports, HttpPostedFileBase Proposedplan, HttpPostedFileBase PartnershipDeed,
            HttpPostedFileBase PanCard, HttpPostedFileBase AddressProof, HttpPostedFileBase BalanceSheet, HttpPostedFileBase IncomeTaxreturn,
            HttpPostedFileBase Experienceproof, HttpPostedFileBase educationalqualification, HttpPostedFileBase electricitybill,
            HttpPostedFileBase financialdetails, HttpPostedFileBase Otherproposedindustry, HttpPostedFileBase CasteCertificate,
            HttpPostedFileBase IdentityProof, HttpPostedFileBase AllotmentLetter, HttpPostedFileBase LandAcquition,
            HttpPostedFileBase outsideGIDAElectricitybill, HttpPostedFileBase ApplicantPhoto, HttpPostedFileBase ApplicantSignature)
        {
            ApplicantUploadDoc documentDetail = new ApplicantUploadDoc();
            if (ProjectReports != null && ProjectReports.ContentLength > 0)
            {
                documentDetail.ProjectReport = new byte[ProjectReports.ContentLength];
                ProjectReports.InputStream.Read(documentDetail.ProjectReport, 0, ProjectReports.ContentLength);
            }
            if (Proposedplan != null && Proposedplan.ContentLength > 0)
            {
                documentDetail.ProposedPlanLandUses = new byte[Proposedplan.ContentLength];
                Proposedplan.InputStream.Read(documentDetail.ProposedPlanLandUses, 0, Proposedplan.ContentLength);
            }
            if (PartnershipDeed != null && PartnershipDeed.ContentLength > 0)
            {
                documentDetail.Memorendum = new byte[PartnershipDeed.ContentLength];
                PartnershipDeed.InputStream.Read(documentDetail.Memorendum, 0, PartnershipDeed.ContentLength);
            }

            if (PanCard != null && PanCard.ContentLength > 0)
            {
                documentDetail.ScanPAN = new byte[PanCard.ContentLength];
                PanCard.InputStream.Read(documentDetail.ScanPAN, 0, PanCard.ContentLength);
            }
            if (AddressProof != null && AddressProof.ContentLength > 0)
            {
                documentDetail.ScanAddressProof = new byte[AddressProof.ContentLength];
                AddressProof.InputStream.Read(documentDetail.ScanAddressProof, 0, AddressProof.ContentLength);
            }

            if (BalanceSheet != null && BalanceSheet.ContentLength > 0)
            {
                documentDetail.BalanceSheet = new byte[BalanceSheet.ContentLength];
                BalanceSheet.InputStream.Read(documentDetail.BalanceSheet, 0, BalanceSheet.ContentLength);
            }
            if (IncomeTaxreturn != null && IncomeTaxreturn.ContentLength > 0)
            {
                documentDetail.ITReturn = new byte[IncomeTaxreturn.ContentLength];
                IncomeTaxreturn.InputStream.Read(documentDetail.ITReturn, 0, IncomeTaxreturn.ContentLength);
            }
            if (Experienceproof != null && Experienceproof.ContentLength > 0)
            {
                documentDetail.ExperienceProof = new byte[Experienceproof.ContentLength];
                Experienceproof.InputStream.Read(documentDetail.ExperienceProof, 0, Experienceproof.ContentLength);
            }

            if (educationalqualification != null && educationalqualification.ContentLength > 0)
            {
                documentDetail.ApplicantEduTechQualification = new byte[educationalqualification.ContentLength];
                educationalqualification.InputStream.Read(documentDetail.ApplicantEduTechQualification, 0, educationalqualification.ContentLength);
            }

            if (electricitybill != null && electricitybill.ContentLength > 0)
            {
                documentDetail.PreEstablishedIndustriesDoc = new byte[electricitybill.ContentLength];
                electricitybill.InputStream.Read(documentDetail.PreEstablishedIndustriesDoc, 0, electricitybill.ContentLength);
            }

            if (financialdetails != null && financialdetails.ContentLength > 0)
            {
                documentDetail.FinDetailsEstablishedIndustries = new byte[financialdetails.ContentLength];
                financialdetails.InputStream.Read(documentDetail.FinDetailsEstablishedIndustries, 0, financialdetails.ContentLength);
            }

            if (Otherproposedindustry != null && Otherproposedindustry.ContentLength > 0)
            {
                documentDetail.OtherDocForProposedIndustry = new byte[Otherproposedindustry.ContentLength];
                Otherproposedindustry.InputStream.Read(documentDetail.OtherDocForProposedIndustry, 0, Otherproposedindustry.ContentLength);
            }

            if (CasteCertificate != null && CasteCertificate.ContentLength > 0)
            {
                documentDetail.ScanCastCert = new byte[CasteCertificate.ContentLength];
                CasteCertificate.InputStream.Read(documentDetail.ScanCastCert, 0, CasteCertificate.ContentLength);
            }

            if (IdentityProof != null && IdentityProof.ContentLength > 0)
            {
                documentDetail.ScanID = new byte[IdentityProof.ContentLength];
                IdentityProof.InputStream.Read(documentDetail.ScanID, 0, IdentityProof.ContentLength);
            }

            if (AllotmentLetter != null && AllotmentLetter.ContentLength > 0)
            {
                documentDetail.AllotmentLetter = new byte[AllotmentLetter.ContentLength];
                AllotmentLetter.InputStream.Read(documentDetail.AllotmentLetter, 0, AllotmentLetter.ContentLength);
            }
            if (LandAcquition != null && LandAcquition.ContentLength > 0)
            {
                documentDetail.LandEquitionDocProof = new byte[LandAcquition.ContentLength];
                LandAcquition.InputStream.Read(documentDetail.LandEquitionDocProof, 0, LandAcquition.ContentLength);
            }

            //documentDetail.LandEquitionDocProof = new byte[outsideGIDAElectricitybill.ContentLength];
            //outsideGIDAElectricitybill.InputStream.Read(documentDetail.LandEquitionDocProof, 0, outsideGIDAElectricitybill.ContentLength);
            if (ApplicantPhoto != null && ApplicantPhoto.ContentLength > 0)
            {
                documentDetail.ApplicantPhoto = new byte[ApplicantPhoto.ContentLength];
                ApplicantPhoto.InputStream.Read(documentDetail.ApplicantPhoto, 0, ApplicantPhoto.ContentLength);
            }

            if (ApplicantSignature != null && ApplicantSignature.ContentLength > 0)
            {
                documentDetail.ApplicantSignature = new byte[ApplicantSignature.ContentLength];
                ApplicantSignature.InputStream.Read(documentDetail.ApplicantSignature, 0, ApplicantSignature.ContentLength);
            }
            documentDetail.UserId = ((CustomPrincipal)User).Id;
            ApplicantDetails _details = new ApplicantDetails();
            var result = _details.SaveApplicantDocument(((CustomPrincipal)User).Id, documentDetail);
            if (result == Enums.CrudStatus.Saved)
            {
                SetAlertMessage("document detail saved", "Document Entry");
                return RedirectToAction("PaymentRequest");
            }
            else
            {
                SetAlertMessage("Document detail not saved, Please Attach all reqired document", "Document Entry");
                return RedirectToAction("ApplicantDashboard");
            }
        }

        public ActionResult PaymentRequest()
        {
            ApplicantDetails _details = new ApplicantDetails();
            var data = _details.GetUserPlotDetail(((CustomPrincipal)User).Id);

            string baseURL = string.Format("{0}://{1}{2}", Request.Url.Scheme, Request.Url.Authority, Url.Content("~"));
            string amountToBePaid = "1";  //data.NetAmount
            string ccaRequest = "tid=" + VerificationCodeGeneration.GetSerialNumber() + "&merchant_id=" + strMerchantId + "&order_id=" + VerificationCodeGeneration.GetSerialNumber() + "&amount=" + amountToBePaid + "&currency=INR&redirect_url=" + baseURL + "Applicant/PaymentResponse&cancel_url=" + baseURL + "Applicant/PaymentResponse&language=EN&billing_name=" + data.FullApplicantName + "&billing_address=" + data.CAddress + "&billing_city=&billing_state=&billing_zip=&billing_country=&billing_tel=&billing_email=&delivery_name=" + data.FullApplicantName + "&delivery_address=" + data.CAddress + "&delivery_city=&delivery_state=&delivery_zip=&delivery_country=&delivery_tel=&merchant_param1=additional+Info.&merchant_param2=additional+Info.&merchant_param3=additional+Info.&merchant_param4=additional+Info.&merchant_param5=additional+Info.&payment_option=OPTNBK&emi_plan_id=&emi_tenure_id=&card_type=&card_name=&data_accept=&card_number=&expiry_month=&expiry_year=&cvv_number=&issuing_bank=&mobile_number=&mm_id=&otp=&promo_code=&";
            Session["strEncRequest"] = ccaCrypto.Encrypt(ccaRequest, workingKey);
            Session["strAccessCode"] = strAccessCode;
            ViewData["UserData"] = data;
            return View();
        }
        public ActionResult PaymentResponse()
        {
            CCACrypto ccaCrypto = new CCACrypto();
            string encResponse = ccaCrypto.Decrypt(Request.Form["encResp"], workingKey);
            NameValueCollection Params = new NameValueCollection();
            string[] segments = encResponse.Split('&');
            foreach (string seg in segments)
            {
                string[] parts = seg.Split('=');
                if (parts.Length > 0)
                {
                    string Key = parts[0].Trim();
                    string Value = parts[1].Trim();
                    Params.Add(Key, Value);
                }
            }

            //for (int i = 0; i < Params.Count; i++)
            //{
            //    Response.Write(Params.Keys[i] + " = " + Params[i] + "<br>");
            //}

            if (Params["order_status"] == "Success")
            {
                ApplicantDetails _details = new ApplicantDetails();
                ApplicantTransactionDetail detail = new ApplicantTransactionDetail()
                {
                    UserId = ((CustomPrincipal)User).Id,
                    amount = Params["amount"],
                    bank_ref_no = Params["bank_ref_no"],
                    billing_address = Params["billing_address"],
                    billing_name = Params["billing_name"],
                    card_name = Params["card_name"],
                    created_date = DateTime.Now,
                    order_id = Convert.ToInt64(Params["order_id"]),
                    order_status = Params["order_status"],
                    payment_mode = Params["payment_mode"],
                    status_message = Params["status_message"],
                    tracking_id = Convert.ToInt64(Params["tracking_id"]),
                    ApplicationId = UserData.ApplicationId,
                    trans_date = DateTime.Now,
                };
                _details.SaveApplicantTransactionDeatil(detail);
                SetAlertMessage("Payment done successfully", "Payment Status");
                return RedirectToAction("PaymentResponseSuccess");
            }
            else if (Params["order_status"] == "Aborted")
            {
                SetAlertMessage("Payment Aborted", "Error");
                return RedirectToAction("PaymentRequest");
            }
            else
            {
                SetAlertMessage("Payment Failed", "Error");
                return RedirectToAction("PaymentRequest");
            }
        }

        public ActionResult PaymentResponseSuccess()
        {
            return View();
        }

        public ActionResult PaymentReciept()
        {
            ApplicantDetails _details = new ApplicantDetails();
            var data = _details.GetUserPlotDetail(((CustomPrincipal)User).Id);
            ViewData["UserData"] = data;
            return View();
        }

        public ActionResult PaymentAcknowledgement()
        {
            ApplicantDetails _details = new ApplicantDetails();
            var data = _details.GetAcknowledgementDetail(((CustomPrincipal)User).Id);
            ViewData["UserData"] = data;
            return View();
        }

        [HttpPost]
        public JsonResult GetPlotRegistrationDetail(int applicationId)
        {
            ApplicantDetails _details = new ApplicantDetails();
            if (applicationId > 0)
            {
                var data = _details.GetApplciantPlotDetailByApplicationId(applicationId);
                return Json(data, JsonRequestBehavior.AllowGet);
            }
            return null;
        }

        [HttpPost]
        public JsonResult GetApplicantPersonalDetail()
        {
            ApplicantDetails _details = new ApplicantDetails();
            if (UserData.ApplicationId > 0)
            {
                var data = _details.GetApplicantPersonalDetail(UserData.ApplicationId);
                return Json(data, JsonRequestBehavior.AllowGet);
            }
            return null;
        }

        [HttpPost]
        public JsonResult GetApplicantProjectDetail()
        {
            ApplicantDetails _details = new ApplicantDetails();
            if (UserData.ApplicationId > 0)
            {
                var data = _details.GetApplicantProjectDetail(UserData.ApplicationId);
                return Json(data, JsonRequestBehavior.AllowGet);
            }
            return null;
        }

        [HttpPost]
        public JsonResult GetApplicantBankDetail()
        {
            ApplicantDetails _details = new ApplicantDetails();
            if (UserData.ApplicationId > 0)
            {
                var data = _details.GetApplicantBankDetail(UserData.ApplicationId);
                return Json(data, JsonRequestBehavior.AllowGet);
            }
            return null;
        }

        //public FileContentResult GetFile(int id)
        //{
        //    byte[] fileContent = null;
        //    string mimeType = ""; string fileName = "";
        //    ApplicantDetails _details = new ApplicantDetails();
        //    var data = _details.GetApplicantPersonalDetail(UserData.ApplicationId);

        //    fileContent = (byte[])rdr["FileContent"];
        //    mimeType = rdr["MimeType"].ToString();
        //    fileName = rdr["FileName"].ToString();

        //    return File(fileContent, mimeType, fileName);
        //}

        public ActionResult Logout()
        {
            Session.Abandon();
            Session.Clear();
            return RedirectToAction("ApplicantLogin", "Login");
        }
    }
}