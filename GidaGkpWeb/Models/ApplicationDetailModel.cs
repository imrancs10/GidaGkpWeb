﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GidaGkpWeb.Models
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

    public class ApplicantPlotDetailModel
    {
        public int Id { get; set; }
        public Nullable<int> UserId { get; set; }
        public Nullable<int> ApplicationId { get; set; }
        public string ApplicationNumber { get; set; }
        public Nullable<int> AppliedFor { get; set; }
        public Nullable<int> SchemeType { get; set; }
        public Nullable<int> SchemeName { get; set; }
        public Nullable<int> PlotRange { get; set; }
        public string PlotArea { get; set; }
        public Nullable<int> SectorName { get; set; }
        public string EstimatedRate { get; set; }
        public Nullable<int> PaymentSchedule { get; set; }
        public Nullable<decimal> TotalInvestment { get; set; }
        public Nullable<decimal> GST { get; set; }
        public Nullable<decimal> ApplicationFee { get; set; }
        public Nullable<decimal> TotalAmount { get; set; }
        public Nullable<decimal> EarnestMoney { get; set; }
        public Nullable<decimal> NetAmount { get; set; }
        public Nullable<int> IndustryOwnership { get; set; }
        public string UnitName { get; set; }
        public string SignatryName { get; set; }
        public Nullable<System.DateTime> SignatryDateOfBirth { get; set; }
        public string SignatryPresentAddress { get; set; }
        public string SignatryPresentPhoneNumber { get; set; }
        public string SignatryPermanentAddress { get; set; }
        public string SignatryPermanentPhoneNumber { get; set; }
        public Nullable<int> RelationshipStatus { get; set; }
        public Nullable<System.DateTime> CreationDate { get; set; }
    }
    public  class ApplicantDetailModel
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public Nullable<int> ApplicationId { get; set; }
        public string FullApplicantName { get; set; }
        public string FName { get; set; }
        public string MName { get; set; }
        public string SName { get; set; }
        public Nullable<System.DateTime> ApplicantDOB { get; set; }
        public string Gender { get; set; }
        public string Category { get; set; }
        public string Nationality { get; set; }
        public string AdhaarNumber { get; set; }
        public string PAN { get; set; }
        public string Mobile { get; set; }
        public string Phone { get; set; }
        public string EmailId { get; set; }
        public string Religion { get; set; }
        public string SubCategory { get; set; }
        public string CAddress { get; set; }
        public string PAddress { get; set; }
        public string IdentiyProof { get; set; }
        public string ResidentialProof { get; set; }
        public Nullable<System.DateTime> CreationDate { get; set; }
    }

    public  class ApplicantProjectDetailModel
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public Nullable<int> ApplicationId { get; set; }
        public string FullApplicantName { get; set; }
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
        public string Skilled { get; set; }
        public string UnSkilled { get; set; }
        public Nullable<System.DateTime> CreationDate { get; set; }
    }

    public class ApplicantBankDetailModel
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public Nullable<int> ApplicationId { get; set; }
        public string BankName { get; set; }
        public string BBName { get; set; }
        public string BBAddress { get; set; }
        public string IFSC_Code { get; set; }
        public string AccountHolderName { get; set; }
        public string BankAccountNo { get; set; }
        public Nullable<System.DateTime> CreationDate { get; set; }
    }
}