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
using GidaGkpWeb.BAL.Login;
using GidaGkpWeb.BAL;
using CCA.Util;
using System.Collections.Specialized;
using GidaGkpWeb.Infrastructure.Utility;

namespace GidaGkpWeb.Controllers
{
    public class MastersController : CommonController
    {
        CCACrypto ccaCrypto = new CCACrypto();
        string workingKey = "AB986C5175D944E600B2567AEDC18A04";//put in the 32bit alpha numeric key in the quotes provided here 	
        string ccaRequest = "";
        public string strMerchantId = "310096";
        public string strEncRequest = "";
        public string strAccessCode = "AVHE00IA10BD01EHDB";// put the access key in the quotes provided here.
        public ActionResult Dashboard()
        {
            return View();
        }

        public ActionResult ApplicantDashboard()
        {
            return View();
        }

        public ActionResult PaymentRequest()
        {
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

            if (Params["status_message"] == "Completed Successfully")
            {
                ApplicantDetails _details = new ApplicantDetails();
                ApplicantTransactionDetail detail = new ApplicantTransactionDetail()
                {
                    UserId = UserData.UserId,
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
                    trans_date = DateTime.Now,
                };
                _details.SaveApplicantTransactionDeatil(detail);
                SetAlertMessage("Payment done successfully", "Payment Status");
                return RedirectToAction("ApplicantDashboard");
            }
            else
            {
                SetAlertMessage("Payment Failed", "Error");
                return View();
            }

            
        }

        [HttpPost]
        public JsonResult SavePlotDetail(string AppliedFor, string SchemeType, string PlotRange, string SchemeName, string plotArea, string SectorName, string EstimatedRate, string PaymemtSchedule, string TotalInvestment, string ApplicationFee, string EarnestMoneyDeposite, string GST, string NetAmount, string TotalAmount, string IndustryOwnershipType, string UnitName, string Name, string dob, string PresentAddress, string PermanentAddress, string RelationshipStatus)
        {
            ApplicantDetails _details = new ApplicantDetails();
            if (string.IsNullOrEmpty(AppliedFor) || string.IsNullOrEmpty(SchemeType))
            {
                //SetAlertMessage("Incomplete Detail", "Error");
                return null;
            }
            string baseURL = string.Format("{0}://{1}{2}", Request.Url.Scheme, Request.Url.Authority, Url.Content("~"));
            string amountToBePaid = "1";  //NetAmount
            string ccaRequest = "tid=" + VerificationCodeGeneration.GetSerialNumber() + "&merchant_id=" + strMerchantId + "&order_id=" + VerificationCodeGeneration.GetSerialNumber() + "&amount=" + amountToBePaid + "&currency=INR&redirect_url=" + baseURL + "Masters/PaymentResponse&cancel_url=" + baseURL + "Masters/PaymentResponse&language=EN&billing_name=" + Name + "&billing_address=" + PresentAddress + "&billing_city=&billing_state=&billing_zip=&billing_country=&billing_tel=&billing_email=&delivery_name=" + Name + "&delivery_address=" + PermanentAddress + "&delivery_city=&delivery_state=&delivery_zip=&delivery_country=&delivery_tel=&merchant_param1=additional+Info.&merchant_param2=additional+Info.&merchant_param3=additional+Info.&merchant_param4=additional+Info.&merchant_param5=additional+Info.&payment_option=OPTNBK&emi_plan_id=&emi_tenure_id=&card_type=&card_name=&data_accept=&card_number=&expiry_month=&expiry_year=&cvv_number=&issuing_bank=&mobile_number=&mm_id=&otp=&promo_code=&";
            Session["strEncRequest"] = ccaCrypto.Encrypt(ccaRequest, workingKey);
            Session["strAccessCode"] = strAccessCode;
            Session["AmountToBePaid"] = NetAmount;
            return Json(_details.SavePlotDetail(UserData.UserId, AppliedFor, SchemeType, PlotRange, SchemeName, plotArea, SectorName, EstimatedRate, PaymemtSchedule, TotalInvestment, ApplicationFee, EarnestMoneyDeposite, GST, NetAmount, TotalAmount, IndustryOwnershipType, UnitName, Name, dob, PresentAddress, PermanentAddress, RelationshipStatus), JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public JsonResult SaveApplicantDetails(string FullName, string FName, string MName, string SName, string DOB, string Gender, string Reservation, string Nationality, string AdhaarNo, string PAN, string MobileNo, string Phone, string Email, string Religion, string SubCategory, string CAddress, string PAddress, string IdentityProof, string ResidentialProof)
        {
            ApplicantDetails _details = new ApplicantDetails();
            if (string.IsNullOrEmpty(FullName) || string.IsNullOrEmpty(FName))
            {
                //SetAlertMessage("Incomplete Detail", "Error");
                return null;
            }
            return Json(CrudResponse(_details.SaveApplicantDetail(UserData.UserId, FullName, FName, MName, SName, DOB, Gender, Reservation, Nationality, AdhaarNo, PAN, MobileNo, Phone, Email, Religion, SubCategory, CAddress, PAddress, IdentityProof, ResidentialProof)), JsonRequestBehavior.AllowGet);
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
            return Json(CrudResponse(_details.SaveProjectDetail(UserData.UserId, ProposedIndustryType, ProjectEstimatedCost, ProposedCoveredArea, ProposedOpenArea, PurpuseOpenArea, ProposedInvestmentLand, ProposedInvestmentBuilding, ProposedInvestmentPlant, FumesNatureQuantity, LiquidQuantity, LiquidChemicalComposition, SolidQuantity, SolidChemicalComposition, GasQuantity, GasChemicalComposition, PowerRequirement, FirstYearNoOfTelephone, FirstYearNoOfFax, UltimateNoOfTelephone, UltimateNoOfFax, Skilled, UnSkilled)), JsonRequestBehavior.AllowGet);
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
            return Json(CrudResponse(_details.SaveBankDetail(UserData.UserId, BankAccountName, BankAccountNo, BankName, BranchName, BranchAddress, IFSCCode)), JsonRequestBehavior.AllowGet);
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
            if (ProjectReports != null && ProjectReports.ContentLength > 0 &&
                Proposedplan != null && Proposedplan.ContentLength > 0 && PartnershipDeed != null && PartnershipDeed.ContentLength > 0 &&
                PanCard != null && PanCard.ContentLength > 0 && AddressProof != null && AddressProof.ContentLength > 0 &&
                BalanceSheet != null && BalanceSheet.ContentLength > 0 && IncomeTaxreturn != null && IncomeTaxreturn.ContentLength > 0 &&
                Experienceproof != null && Experienceproof.ContentLength > 0 && educationalqualification != null && educationalqualification.ContentLength > 0 &&
                electricitybill != null && electricitybill.ContentLength > 0 && financialdetails != null && financialdetails.ContentLength > 0 &&
                Otherproposedindustry != null && Otherproposedindustry.ContentLength > 0 && CasteCertificate != null && CasteCertificate.ContentLength > 0 &&
                IdentityProof != null && IdentityProof.ContentLength > 0 && AllotmentLetter != null && AllotmentLetter.ContentLength > 0 &&
                LandAcquition != null && LandAcquition.ContentLength > 0 && ApplicantPhoto != null && ApplicantPhoto.ContentLength > 0 &&
                ApplicantSignature != null && ApplicantSignature.ContentLength > 0)
            {
                documentDetail.ProjectReport = new byte[ProjectReports.ContentLength];
                ProjectReports.InputStream.Read(documentDetail.ProjectReport, 0, ProjectReports.ContentLength);

                documentDetail.ProposedPlanLandUses = new byte[Proposedplan.ContentLength];
                Proposedplan.InputStream.Read(documentDetail.ProposedPlanLandUses, 0, Proposedplan.ContentLength);

                documentDetail.Memorendum = new byte[PartnershipDeed.ContentLength];
                PartnershipDeed.InputStream.Read(documentDetail.Memorendum, 0, PartnershipDeed.ContentLength);

                documentDetail.ScanPAN = new byte[PanCard.ContentLength];
                PanCard.InputStream.Read(documentDetail.ScanPAN, 0, PanCard.ContentLength);

                documentDetail.ScanAddressProof = new byte[AddressProof.ContentLength];
                AddressProof.InputStream.Read(documentDetail.ScanAddressProof, 0, AddressProof.ContentLength);

                documentDetail.BalanceSheet = new byte[BalanceSheet.ContentLength];
                BalanceSheet.InputStream.Read(documentDetail.BalanceSheet, 0, BalanceSheet.ContentLength);

                documentDetail.ITReturn = new byte[IncomeTaxreturn.ContentLength];
                IncomeTaxreturn.InputStream.Read(documentDetail.ITReturn, 0, IncomeTaxreturn.ContentLength);

                documentDetail.ExperienceProof = new byte[Experienceproof.ContentLength];
                Experienceproof.InputStream.Read(documentDetail.ExperienceProof, 0, Experienceproof.ContentLength);

                documentDetail.ApplicantEduTechQualification = new byte[educationalqualification.ContentLength];
                educationalqualification.InputStream.Read(documentDetail.ApplicantEduTechQualification, 0, educationalqualification.ContentLength);

                documentDetail.PreEstablishedIndustriesDoc = new byte[electricitybill.ContentLength];
                electricitybill.InputStream.Read(documentDetail.PreEstablishedIndustriesDoc, 0, electricitybill.ContentLength);

                documentDetail.FinDetailsEstablishedIndustries = new byte[financialdetails.ContentLength];
                financialdetails.InputStream.Read(documentDetail.FinDetailsEstablishedIndustries, 0, financialdetails.ContentLength);

                documentDetail.OtherDocForProposedIndustry = new byte[Otherproposedindustry.ContentLength];
                Otherproposedindustry.InputStream.Read(documentDetail.OtherDocForProposedIndustry, 0, Otherproposedindustry.ContentLength);

                documentDetail.ScanCastCert = new byte[CasteCertificate.ContentLength];
                CasteCertificate.InputStream.Read(documentDetail.ScanCastCert, 0, CasteCertificate.ContentLength);

                documentDetail.ScanID = new byte[IdentityProof.ContentLength];
                IdentityProof.InputStream.Read(documentDetail.ScanID, 0, IdentityProof.ContentLength);

                documentDetail.AllotmentLetter = new byte[AllotmentLetter.ContentLength];
                AllotmentLetter.InputStream.Read(documentDetail.AllotmentLetter, 0, AllotmentLetter.ContentLength);

                documentDetail.LandEquitionDocProof = new byte[LandAcquition.ContentLength];
                LandAcquition.InputStream.Read(documentDetail.LandEquitionDocProof, 0, LandAcquition.ContentLength);

                //documentDetail.LandEquitionDocProof = new byte[outsideGIDAElectricitybill.ContentLength];
                //outsideGIDAElectricitybill.InputStream.Read(documentDetail.LandEquitionDocProof, 0, outsideGIDAElectricitybill.ContentLength);

                documentDetail.ApplicantPhoto = new byte[ApplicantPhoto.ContentLength];
                ApplicantPhoto.InputStream.Read(documentDetail.ApplicantPhoto, 0, ApplicantPhoto.ContentLength);

                documentDetail.ApplicantSignature = new byte[ApplicantSignature.ContentLength];
                ApplicantSignature.InputStream.Read(documentDetail.ApplicantSignature, 0, ApplicantSignature.ContentLength);

                ApplicantDetails _details = new ApplicantDetails();
                _details.SaveApplicantDocument(UserData.UserId, documentDetail);
                SetAlertMessage("document detail saved", "Document Entry");
                return RedirectToAction("PaymentRequest");
            }
            else
            {
                SetAlertMessage("Document detail not saved, Please Attach all reqired document", "Document Entry");
                return RedirectToAction("ApplicantDashboard");
            }
        }


        [HttpPost]
        public JsonResult GetLookupDetail(int? lookupTypeId, string lookupType)
        {
            MasterDetails _details = new MasterDetails();
            if (lookupTypeId == 0)
            {
                lookupTypeId = null;
            }
            return Json(_details.GetLookupDetail(lookupTypeId, lookupType), JsonRequestBehavior.AllowGet);
        }


        public ActionResult Logout()
        {
            Session.Abandon();
            Session.Clear();
            return RedirectToAction("ApplicantLogin", "Login");
        }
    }
}