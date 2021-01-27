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
    public class ApplicantDetailModel
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

    public class ApplicantProjectDetailModel
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

    public class ApplicantUploadDocumentModel
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public Nullable<int> ApplicationId { get; set; }
        public byte[] ProjectReport { get; set; }
        public string ProjectReportURL { get; set; }
        public byte[] ProposedPlanLandUses { get; set; }
        public string ProposedPlanLandUsesURL { get; set; }
        public byte[] Memorendum { get; set; }
        public string MemorendumURL { get; set; }
        public byte[] ScanPAN { get; set; }
        public string ScanPANURL { get; set; }
        public byte[] ScanAddressProof { get; set; }
        public string ScanAddressProofURL { get; set; }
        public byte[] BalanceSheet { get; set; }
        public string BalanceSheetURL { get; set; }
        public byte[] ITReturn { get; set; }
        public string ITReturnURL { get; set; }
        public byte[] ExperienceProof { get; set; }
        public string ExperienceProofURL { get; set; }
        public byte[] ApplicantEduTechQualification { get; set; }
        public string ApplicantEduTechQualificationURL { get; set; }
        public byte[] PreEstablishedIndustriesDoc { get; set; }
        public string PreEstablishedIndustriesDocURL { get; set; }
        public byte[] FinDetailsEstablishedIndustries { get; set; }
        public string FinDetailsEstablishedIndustriesURL { get; set; }
        public byte[] OtherDocForProposedIndustry { get; set; }
        public string OtherDocForProposedIndustryURL { get; set; }
        public byte[] ScanCastCert { get; set; }
        public string ScanCastCertURL { get; set; }
        public byte[] ScanID { get; set; }
        public string ScanIDURL { get; set; }
        public byte[] BankVerifiedSignature { get; set; }
        public string BankVerifiedSignatureURL { get; set; }
        public byte[] DocProofForIndustrialEstablishedOutsideGida { get; set; }
        public string DocProofForIndustrialEstablishedOutsideGidaURL { get; set; }
        public byte[] ApplicantPhoto { get; set; }
        public string ApplicantPhotoURL { get; set; }
        public byte[] ApplicantSignature { get; set; }
        public string ApplicantSignatureURL { get; set; }
        public Nullable<System.DateTime> CreationDate { get; set; }
        public string ProjectReportFileName { get; set; }
        public string ProjectReportFileType { get; set; }
        public string ProposedPlanLandUsesFileName { get; set; }
        public string ProposedPlanLandUsesFileType { get; set; }
        public string MemorendumFileName { get; set; }
        public string MemorendumFileType { get; set; }
        public string ScanPANFileName { get; set; }
        public string ScanPANFileType { get; set; }
        public string ScanAddressProofFileName { get; set; }
        public string ScanAddressProofFileType { get; set; }
        public string BalanceSheetFileName { get; set; }
        public string BalanceSheetFileType { get; set; }
        public string ITReturnFileName { get; set; }
        public string ITReturnFileType { get; set; }
        public string ExperienceProofFileName { get; set; }
        public string ExperienceProofFileType { get; set; }
        public string ApplicantEduTechQualificationFileName { get; set; }
        public string ApplicantEduTechQualificationFileType { get; set; }
        public string PreEstablishedIndustriesDocFileName { get; set; }
        public string PreEstablishedIndustriesDocFileType { get; set; }
        public string FinDetailsEstablishedIndustriesFileName { get; set; }
        public string FinDetailsEstablishedIndustriesFileType { get; set; }
        public string OtherDocForProposedIndustryFileName { get; set; }
        public string OtherDocForProposedIndustryFileType { get; set; }
        public string ScanCastCertFileName { get; set; }
        public string ScanCastCertFileType { get; set; }
        public string ScanIDFileName { get; set; }
        public string ScanIDFileType { get; set; }
        public string BankVerifiedSignatureFileName { get; set; }
        public string BankVerifiedSignatureFileType { get; set; }
        public string DocProofForIndustrialEstablishedOutsideGidaFileName { get; set; }
        public string DocProofForIndustrialEstablishedOutsideGidaFileType { get; set; }
        public string ApplicantPhotoFileName { get; set; }
        public string ApplicantPhotoFileType { get; set; }
        public string ApplicantSignatureFileName { get; set; }
        public string ApplicantSignatureFileType { get; set; }
    }
}