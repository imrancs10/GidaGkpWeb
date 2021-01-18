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

        [HttpPost]
        public JsonResult SavePlotDetail(string AppliedFor, string SchemeType, string PlotRange, string SchemeName, string plotArea, string SectorName, string EstimatedRate, string PaymemtSchedule, string TotalInvestment, string ApplicationFee, string EarnestMoneyDeposite, string GST, string NetAmount, string TotalAmount, string IndustryOwnershipType, string UnitName, string Name, string dob, string PresentAddress, string PermanentAddress, string RelationshipStatus)
        {
            ApplicantDetails _details = new ApplicantDetails();
            if (string.IsNullOrEmpty(AppliedFor) || string.IsNullOrEmpty(SchemeType))
            {
                //SetAlertMessage("Incomplete Detail", "Error");
                return null;
            }
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
                return RedirectToAction("ApplicantDashboard");
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