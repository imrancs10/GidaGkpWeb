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
            return Json(CrudResponse(_details.SavePlotDetail(UserData.UserId, AppliedFor, SchemeType, PlotRange, SchemeName, plotArea, SectorName, EstimatedRate, PaymemtSchedule, TotalInvestment, ApplicationFee, EarnestMoneyDeposite, GST, NetAmount, TotalAmount, IndustryOwnershipType, UnitName, Name, dob, PresentAddress, PermanentAddress, RelationshipStatus)), JsonRequestBehavior.AllowGet);
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

        public ActionResult Logout()
        {
            Session.Abandon();
            Session.Clear();
            return RedirectToAction("ApplicantLogin", "Login");
        }
    }
}