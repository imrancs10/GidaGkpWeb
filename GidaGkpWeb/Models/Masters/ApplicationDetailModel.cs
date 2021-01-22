using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GidaGkpWeb.Models.Masters
{
    public class ApplicationDetailModel
    {
        public int ApplicationId { get; set; }
        public string ApplicationNumber { get; set; }
        public string FullApplicantName { get; set; }
        public string CAddress { get; set; }
        public string Mobile { get; set; }
        public decimal? TotalAmount { get; set; }
        public decimal? NetAmount { get; set; }
        public decimal? ApplicationFee { get; set; }
        public decimal? GST { get; set; }
        public decimal? EarnestMoneyDeposit { get; set; }
        public DateTime? PaymentDate { get; set; }
        public string SchemeType { get; set; }
        public string SchemeName { get; set; }
        public string SectorName { get; set; }
        public string PlotArea { get; set; }
    }
    public class AcknowledgementDetailModel : ApplicationDetailModel
    {
        public string SectorName { get; set; }
        public string PlotArea { get; set; }
        public string IndustryOwnership { get; set; }
        public string UnitName { get; set; }
        public string SignatryName { get; set; }
        public DateTime? SignatryDateOfBirth { get; set; }
        public string SignatryPermanentAddress { get; set; }
        public string SignatryPermanentPhoneNumber { get; set; }
        public string SignatryPresentAddress { get; set; }
        public string SignatryPresentPhoneNumber { get; set; }
        public string RelationshipStatus { get; set; }
        public string ProposedIndustryType { get; set; }
        public string ProjectEstimatedCost { get; set; }
        public string ProposedCoveredArea { get; set; }
        public string ProposedOpenArea { get; set; }
        public string PurpuseOpenArea { get; set; }
        public string ProposedInvestmentLand { get; set; }
        public string ProposedInvestmentBuilding { get; set; }
        public string ProposedInvestmentPlant { get; set; }
        public string FumesNatureQuantity { get; set; }
        public string LiquidQuantity { get; set; }
        public string LiquidChemicalComposition { get; set; }
        public string SolidQuantity { get; set; }
        public string SolidChemicalComposition { get; set; }
        public string GasQuantity { get; set; }
        public string GasChemicalComposition { get; set; }
        public string EffluentTreatmentMeasures { get; set; }
        public string PowerRequirement { get; set; }
        public string FirstYearNoOfTelephone { get; set; }
        public string FirstYearNoOfFax { get; set; }
        public string UltimateNoOfTelephone { get; set; }
        public string UltimateNoOfFax { get; set; }
        public string BankName { get; set; }
        public byte[] ApplicantPhoto { get; set; }
        public byte[] ApplicantSignature { get; set; }
    }
}