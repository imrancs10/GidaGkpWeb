﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DataLayer;
using GidaGkpWeb.Global;
using System.Data.Entity;
using GidaGkpWeb.Models;

namespace GidaGkpWeb.BAL
{
    public class ApplicantDetails
    {
        GidaGKPEntities _db = null;

        public int SaveApplicantTransactionDeatil(ApplicantTransactionDetail detail)
        {
            _db = new GidaGKPEntities();
            _db.Entry(detail).State = EntityState.Added;
            return _db.SaveChanges();

        }
        public string SavePlotDetail(int userId, string AppliedFor, string SchemeType, string PlotRange, string SchemeName, string plotArea, string SectorName, string SectorDescription, string EstimatedRate, string PaymemtSchedule, string TotalInvestment, string ApplicationFee, string EarnestMoneyDeposite, string GST, string NetAmount, string TotalAmount, string IndustryOwnershipType, string UnitName, string Name, string dob, string PresentAddress, string PermanentAddress, string RelationshipStatus)
        {
            _db = new GidaGKPEntities();
            int _effectRow = 0;
            ApplicantApplicationDetail app = new ApplicantApplicationDetail();

            var existingPlotDetail = _db.ApplicantPlotDetails.Where(x => x.ApplicationId == UserData.ApplicationId).FirstOrDefault();
            if (existingPlotDetail != null)  // update plot detail
            {
                app.ApplicationNumber = _db.ApplicantApplicationDetails.Where(x => x.ApplicationId == UserData.ApplicationId).FirstOrDefault().ApplicationNumber;
                existingPlotDetail.ApplicationFee = Convert.ToDecimal(ApplicationFee);
                existingPlotDetail.UserId = userId;
                existingPlotDetail.AppliedFor = Convert.ToInt32(AppliedFor);
                existingPlotDetail.EarnestMoney = Convert.ToDecimal(EarnestMoneyDeposite);
                existingPlotDetail.EstimatedRate = EstimatedRate;
                existingPlotDetail.GST = Convert.ToDecimal(GST);
                existingPlotDetail.IndustryOwnership = Convert.ToInt32(IndustryOwnershipType);
                existingPlotDetail.NetAmount = Convert.ToDecimal(NetAmount);
                existingPlotDetail.PaymentSchedule = Convert.ToInt32(PaymemtSchedule);
                existingPlotDetail.PlotArea = plotArea;
                existingPlotDetail.PlotRange = Convert.ToInt32(PlotRange);
                existingPlotDetail.RelationshipStatus = Convert.ToInt32(RelationshipStatus);
                existingPlotDetail.SchemeName = Convert.ToInt32(SchemeName);
                existingPlotDetail.SchemeType = Convert.ToInt32(SchemeType);
                existingPlotDetail.SectorName = Convert.ToInt32(SectorName);
                existingPlotDetail.SignatryDateOfBirth = Convert.ToDateTime(dob);
                existingPlotDetail.SignatryName = Name;
                existingPlotDetail.SignatryPermanentAddress = PermanentAddress;
                existingPlotDetail.SignatryPresentAddress = PresentAddress;
                existingPlotDetail.TotalAmount = Convert.ToDecimal(TotalAmount);
                existingPlotDetail.TotalInvestment = Convert.ToDecimal(TotalInvestment);
                existingPlotDetail.UnitName = UnitName;
                existingPlotDetail.ApplicationId = UserData.ApplicationId;
                _db.Entry(existingPlotDetail).State = EntityState.Modified;
                _effectRow = _db.SaveChanges();
            }
            else //save plot detail
            {
                var applciationDeatil = _db.ApplicantApplicationDetails.OrderByDescending(x => x.ApplicationId).Take(1).FirstOrDefault();
                int maxId = 0;
                if (applciationDeatil != null)
                {
                    maxId = applciationDeatil.ApplicationId + 1;
                }
                else
                {
                    maxId = 1;
                }
                app.UserId = userId;
                app.ApplicationNumber = DateTime.Now.Month.ToString() + DateTime.Now.Year.ToString() + SectorDescription.Replace("Sector ", "") + maxId.ToString().PadLeft(4, '0');
                _db.Entry(app).State = EntityState.Added;
                _db.SaveChanges();
                UserData.ApplicationId = app.ApplicationId;

                ApplicantFormStep step = new ApplicantFormStep()
                {
                    UserId = userId,
                    ApplicantStepCompleted = 1,
                    ApplicationId = UserData.ApplicationId
                };
                _db.Entry(step).State = EntityState.Added;

                ApplicantPlotDetail _newRecord = new ApplicantPlotDetail()
                {
                    ApplicationFee = Convert.ToDecimal(ApplicationFee),
                    UserId = userId,
                    AppliedFor = Convert.ToInt32(AppliedFor),
                    CreationDate = DateTime.Now,
                    EarnestMoney = Convert.ToDecimal(EarnestMoneyDeposite),
                    EstimatedRate = EstimatedRate,
                    GST = Convert.ToDecimal(GST),
                    IndustryOwnership = Convert.ToInt32(IndustryOwnershipType),
                    NetAmount = Convert.ToDecimal(NetAmount),
                    PaymentSchedule = Convert.ToInt32(PaymemtSchedule),
                    PlotArea = plotArea,
                    PlotRange = Convert.ToInt32(PlotRange),
                    RelationshipStatus = Convert.ToInt32(RelationshipStatus),
                    SchemeName = Convert.ToInt32(SchemeName),
                    SchemeType = Convert.ToInt32(SchemeType),
                    SectorName = Convert.ToInt32(SectorName),
                    SignatryDateOfBirth = Convert.ToDateTime(dob),
                    SignatryName = Name,
                    SignatryPermanentAddress = PermanentAddress,
                    SignatryPresentAddress = PresentAddress,
                    TotalAmount = Convert.ToDecimal(TotalAmount),
                    TotalInvestment = Convert.ToDecimal(TotalInvestment),
                    UnitName = UnitName,
                    ApplicationId = UserData.ApplicationId
                };

                _db.Entry(_newRecord).State = EntityState.Added;
                _effectRow = _db.SaveChanges();
            }


            return _effectRow > 0 ? app.ApplicationNumber : "Error";
        }

        public Enums.CrudStatus SaveApplicantDetail(int userId, string FullName, string FName, string MName, string SName, string DOB, string Gender, string Category, string Nationality, string AdhaarNo, string PAN, string MobileNo, string Phone, string Email, string Religion, string SubCategory, string CAddress, string PAddress, string IdentityProof, string ResidentialProof)
        {
            _db = new GidaGKPEntities();

            ApplicantFormStep step = new ApplicantFormStep()
            {
                UserId = userId,
                ApplicantStepCompleted = 2,
                ApplicationId = UserData.ApplicationId
            };
            _db.Entry(step).State = EntityState.Added;

            int _effectRow = 0;

            var extingApplicantDetail = _db.ApplicantDetails.Where(x => x.ApplicationId == UserData.ApplicationId).FirstOrDefault();
            if (extingApplicantDetail != null)  // update Applicant detail
            {
                extingApplicantDetail.AdhaarNumber = AdhaarNo;
                extingApplicantDetail.UserId = userId;
                extingApplicantDetail.SubCategory = SubCategory;
                extingApplicantDetail.ApplicantDOB = Convert.ToDateTime(DOB);
                extingApplicantDetail.CAddress = CAddress;
                extingApplicantDetail.Category = Category;
                extingApplicantDetail.CreationDate = DateTime.Now;
                extingApplicantDetail.EmailId = Email;
                extingApplicantDetail.FName = FName;
                extingApplicantDetail.FullApplicantName = FullName;
                extingApplicantDetail.Gender = Gender;
                extingApplicantDetail.IdentiyProof = IdentityProof;
                extingApplicantDetail.MName = MName;
                extingApplicantDetail.Mobile = MobileNo;
                extingApplicantDetail.Nationality = Nationality;
                extingApplicantDetail.PAddress = PAddress;
                extingApplicantDetail.PAN = PAN;
                extingApplicantDetail.Phone = Phone;
                extingApplicantDetail.Religion = Religion;
                extingApplicantDetail.ResidentialProof = ResidentialProof;
                extingApplicantDetail.SName = SName;
                extingApplicantDetail.ApplicationId = UserData.ApplicationId;
                _db.Entry(extingApplicantDetail).State = EntityState.Modified;
                _effectRow = _db.SaveChanges();
            }
            else
            {
                ApplicantDetail _newRecord = new ApplicantDetail()
                {
                    AdhaarNumber = AdhaarNo,
                    UserId = userId,
                    SubCategory = SubCategory,
                    ApplicantDOB = Convert.ToDateTime(DOB),
                    CAddress = CAddress,
                    Category = Category,
                    CreationDate = DateTime.Now,
                    EmailId = Email,
                    FName = FName,
                    FullApplicantName = FullName,
                    Gender = Gender,
                    IdentiyProof = IdentityProof,
                    MName = MName,
                    Mobile = MobileNo,
                    Nationality = Nationality,
                    PAddress = PAddress,
                    PAN = PAN,
                    Phone = Phone,
                    Religion = Religion,
                    ResidentialProof = ResidentialProof,
                    SName = SName,
                    ApplicationId = UserData.ApplicationId
                };
                _db.Entry(_newRecord).State = EntityState.Added;
                _effectRow = _db.SaveChanges();
            }

            return _effectRow > 0 ? Enums.CrudStatus.Saved : Enums.CrudStatus.NotSaved;
        }

        public Enums.CrudStatus SaveProjectDetail(int userId, string ProposedIndustryType, string ProjectEstimatedCost, string ProposedCoveredArea, string ProposedOpenArea, string PurpuseOpenArea, string ProposedInvestmentLand, string ProposedInvestmentBuilding, string ProposedInvestmentPlant, string FumesNatureQuantity, string LiquidQuantity, string LiquidChemicalComposition, string SolidQuantity, string SolidChemicalComposition, string GasQuantity, string GasChemicalComposition, string PowerRequirement, string FirstYearNoOfTelephone, string FirstYearNoOfFax, string UltimateNoOfTelephone, string UltimateNoOfFax, string Skilled, string UnSkilled)
        {
            _db = new GidaGKPEntities();

            ApplicantFormStep step = new ApplicantFormStep()
            {
                UserId = userId,
                ApplicantStepCompleted = 3,
                ApplicationId = UserData.ApplicationId
            };
            _db.Entry(step).State = EntityState.Added;

            int _effectRow = 0;

            var extingProjectDetail = _db.ApplicantProjectDetails.Where(x => x.ApplicationId == UserData.ApplicationId).FirstOrDefault();
            if (extingProjectDetail != null)  // update project detail
            {
                extingProjectDetail.UserId = userId;
                extingProjectDetail.FirstYearNoOfFax = FirstYearNoOfFax;
                extingProjectDetail.FirstYearNoOfTelephone = FirstYearNoOfTelephone;
                extingProjectDetail.FumesNatureQuantity = FumesNatureQuantity;
                extingProjectDetail.GasChemicalComposition = GasChemicalComposition;
                extingProjectDetail.GasQuantity = GasQuantity;
                extingProjectDetail.EffluentTreatmentMeasures = "";
                extingProjectDetail.LiquidChemicalComposition = LiquidChemicalComposition;
                extingProjectDetail.LiquidQuantity = LiquidQuantity;
                extingProjectDetail.PowerRequirement = PowerRequirement;
                extingProjectDetail.ProjectEstimatedCost = ProjectEstimatedCost;
                extingProjectDetail.ProposedCoveredArea = ProposedCoveredArea;
                extingProjectDetail.ProposedIndustryType = ProposedIndustryType;
                extingProjectDetail.ProposedInvestmentBuilding = ProposedInvestmentBuilding;
                extingProjectDetail.ProposedInvestmentLand = ProposedInvestmentLand;
                extingProjectDetail.ProposedInvestmentPlant = ProposedInvestmentPlant;
                extingProjectDetail.ProposedOpenArea = ProposedOpenArea;
                extingProjectDetail.PurpuseOpenArea = PurpuseOpenArea;
                extingProjectDetail.Skilled = Skilled;
                extingProjectDetail.SolidChemicalComposition = SolidChemicalComposition;
                extingProjectDetail.SolidQuantity = SolidQuantity;
                extingProjectDetail.UltimateNoOfFax = UltimateNoOfFax;
                extingProjectDetail.UltimateNoOfTelephone = UltimateNoOfTelephone;
                extingProjectDetail.UnSkilled = UnSkilled;
                extingProjectDetail.ApplicationId = UserData.ApplicationId;
                _db.Entry(extingProjectDetail).State = EntityState.Modified;
                _effectRow = _db.SaveChanges();
            }
            else
            {
                ApplicantProjectDetail _newRecord = new ApplicantProjectDetail()
                {
                    UserId = userId,
                    CreationDate = DateTime.Now,
                    FirstYearNoOfFax = FirstYearNoOfFax,
                    FirstYearNoOfTelephone = FirstYearNoOfTelephone,
                    FumesNatureQuantity = FumesNatureQuantity,
                    GasChemicalComposition = GasChemicalComposition,
                    GasQuantity = GasQuantity,
                    EffluentTreatmentMeasures = "",
                    LiquidChemicalComposition = LiquidChemicalComposition,
                    LiquidQuantity = LiquidQuantity,
                    PowerRequirement = PowerRequirement,
                    ProjectEstimatedCost = ProjectEstimatedCost,
                    ProposedCoveredArea = ProposedCoveredArea,
                    ProposedIndustryType = ProposedIndustryType,
                    ProposedInvestmentBuilding = ProposedInvestmentBuilding,
                    ProposedInvestmentLand = ProposedInvestmentLand,
                    ProposedInvestmentPlant = ProposedInvestmentPlant,
                    ProposedOpenArea = ProposedOpenArea,
                    PurpuseOpenArea = PurpuseOpenArea,
                    Skilled = Skilled,
                    SolidChemicalComposition = SolidChemicalComposition,
                    SolidQuantity = SolidQuantity,
                    UltimateNoOfFax = UltimateNoOfFax,
                    UltimateNoOfTelephone = UltimateNoOfTelephone,
                    UnSkilled = UnSkilled,
                    ApplicationId = UserData.ApplicationId
                };
                _db.Entry(_newRecord).State = EntityState.Added;
                _effectRow = _db.SaveChanges();
            }

            return _effectRow > 0 ? Enums.CrudStatus.Saved : Enums.CrudStatus.NotSaved;
        }

        public Enums.CrudStatus SaveBankDetail(int userId, string BankAccountName, string BankAccountNo, string BankName, string BranchName, string BranchAddress, string IFSCCode)
        {
            _db = new GidaGKPEntities();

            ApplicantFormStep step = new ApplicantFormStep()
            {
                UserId = userId,
                ApplicantStepCompleted = 4,
                ApplicationId = UserData.ApplicationId
            };
            _db.Entry(step).State = EntityState.Added;

            int _effectRow = 0;

            var extingBankDetail = _db.ApplicantBankDetails.Where(x => x.ApplicationId == UserData.ApplicationId).FirstOrDefault();
            if (extingBankDetail != null)  // update bank detail
            {
                extingBankDetail.AccountHolderName = BankAccountName;
                extingBankDetail.BankAccountNo = BankAccountNo;
                extingBankDetail.BankName = BankName;
                extingBankDetail.BBAddress = BranchAddress;
                extingBankDetail.BBName = BranchName;
                extingBankDetail.IFSC_Code = IFSCCode;
                _db.Entry(extingBankDetail).State = EntityState.Modified;
                _effectRow = _db.SaveChanges();
            }

            else
            {
                ApplicantBankDetail _newRecord = new ApplicantBankDetail()
                {
                    UserId = userId,
                    CreationDate = DateTime.Now,
                    AccountHolderName = BankAccountName,
                    BankAccountNo = BankAccountNo,
                    BankName = BankName,
                    BBAddress = BranchAddress,
                    BBName = BranchName,
                    IFSC_Code = IFSCCode,
                    ApplicationId = UserData.ApplicationId
                };
                _db.Entry(_newRecord).State = EntityState.Added;
                _effectRow = _db.SaveChanges();
            }

            return _effectRow > 0 ? Enums.CrudStatus.Saved : Enums.CrudStatus.NotSaved;
        }

        public Enums.CrudStatus SaveApplicantDocument(int userId, ApplicantUploadDoc docDetail)
        {
            _db = new GidaGKPEntities();

            ApplicantFormStep step = new ApplicantFormStep()
            {
                UserId = userId,
                ApplicantStepCompleted = 5,
                ApplicationId = UserData.ApplicationId
            };
            _db.Entry(step).State = EntityState.Added;

            int _effectRow = 0;

            var extingDocumentDetail = _db.ApplicantUploadDocs.Where(x => x.ApplicationId == UserData.ApplicationId).FirstOrDefault();
            if (extingDocumentDetail != null)  // update doc detail
            {
                extingDocumentDetail.AllotmentLetter = docDetail.AllotmentLetter;
                extingDocumentDetail.ApplicantEduTechQualification = docDetail.ApplicantEduTechQualification;
                extingDocumentDetail.ApplicantPhoto = docDetail.ApplicantPhoto; 
                extingDocumentDetail.ApplicantSignature = docDetail.ApplicantSignature; 
                extingDocumentDetail.BalanceSheet = docDetail.BalanceSheet;
                extingDocumentDetail.DocProofForIndustrialEstablishedOutsideGida = docDetail.DocProofForIndustrialEstablishedOutsideGida; 
                extingDocumentDetail.ExperienceProof = docDetail.ExperienceProof; 
                extingDocumentDetail.FinDetailsEstablishedIndustries = docDetail.FinDetailsEstablishedIndustries; 
                extingDocumentDetail.ITReturn = docDetail.ITReturn; 
                extingDocumentDetail.LandEquitionDocProof = docDetail.LandEquitionDocProof; 
                extingDocumentDetail.Memorendum= docDetail.Memorendum; 
                extingDocumentDetail.OtherDocForProposedIndustry = docDetail.OtherDocForProposedIndustry; 
                extingDocumentDetail.PreEstablishedIndustriesDoc = docDetail.PreEstablishedIndustriesDoc; 
                extingDocumentDetail.ProjectReport = docDetail.ProjectReport; 
                extingDocumentDetail.ProposedPlanLandUses = docDetail.ProposedPlanLandUses; 
                extingDocumentDetail.ScanAddressProof = docDetail.ScanAddressProof; 
                extingDocumentDetail.ScanCastCert = docDetail.ScanCastCert; 
                extingDocumentDetail.ScanID = docDetail.ScanID;
                extingDocumentDetail.ScanPAN = docDetail.ScanPAN;
                _db.Entry(extingDocumentDetail).State = EntityState.Modified;
                _effectRow = _db.SaveChanges();
            }
            else
            {
                docDetail.ApplicationId = UserData.ApplicationId;
                _db.Entry(docDetail).State = EntityState.Added;
                _effectRow = _db.SaveChanges();
            }

            return _effectRow > 0 ? Enums.CrudStatus.Saved : Enums.CrudStatus.NotSaved;
        }

        public ApplicationDetailModel GetUserPlotDetail(int userId)
        {
            _db = new GidaGKPEntities();
            return (from application in _db.ApplicantApplicationDetails
                    join applicantDetail in _db.ApplicantDetails on application.ApplicationId equals applicantDetail.ApplicationId
                    join plotDetail in _db.ApplicantPlotDetails on application.ApplicationId equals plotDetail.ApplicationId
                    join transaction1 in _db.ApplicantTransactionDetails on applicantDetail.ApplicationId equals transaction1.ApplicationId into transaction2
                    from transaction in transaction2.DefaultIfEmpty()
                    where application.UserId == userId
                    select new ApplicationDetailModel
                    {
                        ApplicationNumber = application.ApplicationNumber,
                        FullApplicantName = applicantDetail.FullApplicantName,
                        CAddress = applicantDetail.CAddress,
                        Mobile = applicantDetail.Mobile,
                        TotalAmount = plotDetail.TotalAmount,
                        NetAmount = plotDetail.NetAmount,
                        ApplicationFee = plotDetail.ApplicationFee,
                        EarnestMoneyDeposit = plotDetail.EarnestMoney,
                        GST = plotDetail.GST,
                        PaymentDate = transaction != null ? transaction.trans_date : DateTime.Now
                    }).FirstOrDefault();
        }

        public AcknowledgementDetailModel GetAcknowledgementDetail(int userId)
        {
            _db = new GidaGKPEntities();
            return (from applicationDetail in _db.ApplicantApplicationDetails
                    join applicantDetail in _db.ApplicantDetails on applicationDetail.ApplicationId equals applicantDetail.ApplicationId
                    join plotDetail in _db.ApplicantPlotDetails on applicationDetail.ApplicationId equals plotDetail.ApplicationId
                    join transactionDetail in _db.ApplicantTransactionDetails on applicantDetail.ApplicationId equals transactionDetail.ApplicationId
                    join projectDetail in _db.ApplicantProjectDetails on applicantDetail.ApplicationId equals projectDetail.ApplicationId
                    join documentDetail in _db.ApplicantUploadDocs on applicantDetail.ApplicationId equals documentDetail.ApplicationId
                    join IndustryOwnership in _db.Lookups on plotDetail.IndustryOwnership equals IndustryOwnership.LookupId
                    join RelationshipStatus in _db.Lookups on plotDetail.RelationshipStatus equals RelationshipStatus.LookupId
                    join lookupSectorName in _db.Lookups on plotDetail.SectorName equals lookupSectorName.LookupId

                    where applicationDetail.UserId == userId
                    select new AcknowledgementDetailModel
                    {
                        ApplicationNumber = applicationDetail.ApplicationNumber,
                        FullApplicantName = applicantDetail.FullApplicantName,
                        CAddress = applicantDetail.CAddress,
                        Mobile = applicantDetail.Mobile,
                        TotalAmount = plotDetail.TotalAmount,
                        NetAmount = plotDetail.NetAmount,
                        ApplicationFee = plotDetail.ApplicationFee,
                        EarnestMoneyDeposit = plotDetail.EarnestMoney,
                        GST = plotDetail.GST,
                        PaymentDate = transactionDetail.trans_date,
                        BankName = transactionDetail.payment_mode,
                        ApplicantPhoto = documentDetail.ApplicantPhoto,
                        ApplicantSignature = documentDetail.ApplicantSignature,
                        EffluentTreatmentMeasures = projectDetail.EffluentTreatmentMeasures,
                        FirstYearNoOfFax = projectDetail.FirstYearNoOfFax,
                        FirstYearNoOfTelephone = projectDetail.FirstYearNoOfTelephone,
                        FumesNatureQuantity = projectDetail.FumesNatureQuantity,
                        GasChemicalComposition = projectDetail.GasChemicalComposition,
                        GasQuantity = projectDetail.GasQuantity,
                        IndustryOwnership = IndustryOwnership.LookupName,
                        LiquidChemicalComposition = projectDetail.LiquidChemicalComposition,
                        LiquidQuantity = projectDetail.LiquidQuantity,
                        PlotArea = plotDetail.PlotArea,
                        PowerRequirement = projectDetail.PowerRequirement,
                        ProjectEstimatedCost = projectDetail.ProjectEstimatedCost,
                        ProposedCoveredArea = projectDetail.ProposedCoveredArea,
                        ProposedIndustryType = projectDetail.ProposedIndustryType,
                        ProposedInvestmentBuilding = projectDetail.ProposedInvestmentBuilding,
                        ProposedInvestmentLand = projectDetail.ProposedInvestmentLand,
                        ProposedInvestmentPlant = projectDetail.ProposedInvestmentPlant,
                        ProposedOpenArea = projectDetail.ProposedOpenArea,
                        PurpuseOpenArea = projectDetail.PurpuseOpenArea,
                        RelationshipStatus = RelationshipStatus.LookupName,
                        SectorName = lookupSectorName.LookupName,
                        SignatryDateOfBirth = plotDetail.SignatryDateOfBirth,
                        SignatryName = plotDetail.SignatryName,
                        SignatryPermanentAddress = plotDetail.SignatryPermanentAddress,
                        SignatryPermanentPhoneNumber = plotDetail.SignatryPermanentPhoneNumber,
                        SignatryPresentAddress = plotDetail.SignatryPresentAddress,
                        SignatryPresentPhoneNumber = plotDetail.SignatryPresentPhoneNumber,
                        SolidChemicalComposition = projectDetail.SolidChemicalComposition,
                        SolidQuantity = projectDetail.SolidQuantity,
                        UltimateNoOfFax = projectDetail.UltimateNoOfFax,
                        UltimateNoOfTelephone = projectDetail.UltimateNoOfTelephone,
                        UnitName = plotDetail.UnitName
                    }).FirstOrDefault();
        }

        public List<ApplicationDetailModel> GetUserApplicationDetail(int userId)
        {
            _db = new GidaGKPEntities();
            return (from application in _db.ApplicantApplicationDetails
                    join applicantDetail1 in _db.ApplicantDetails on application.ApplicationId equals applicantDetail1.ApplicationId into applicantDetail2
                    from applicantDetail in applicantDetail2.DefaultIfEmpty()
                    join plotDetail1 in _db.ApplicantPlotDetails on application.ApplicationId equals plotDetail1.ApplicationId into plotDetail2
                    from plotDetail in plotDetail2.DefaultIfEmpty()
                    join transaction1 in _db.ApplicantTransactionDetails on applicantDetail.ApplicationId equals transaction1.ApplicationId into transaction2
                    from transaction in transaction2.DefaultIfEmpty()
                    join lookupSchemeType1 in _db.Lookups on plotDetail.SchemeType equals lookupSchemeType1.LookupId into lookupSchemeType2
                    from lookupSchemeType in lookupSchemeType2.DefaultIfEmpty()

                    join lookupSchemeName1 in _db.Lookups on plotDetail.SchemeName equals lookupSchemeName1.LookupId into lookupSchemeName2
                    from lookupSchemeName in lookupSchemeName2.DefaultIfEmpty()

                    join lookupSectorName1 in _db.Lookups on plotDetail.SchemeName equals lookupSectorName1.LookupId into lookupSectorName2
                    from lookupSectorName in lookupSectorName2.DefaultIfEmpty()

                    where application.UserId == userId
                    select new ApplicationDetailModel
                    {
                        ApplicationId = application.ApplicationId,
                        ApplicationNumber = application.ApplicationNumber,
                        FullApplicantName = applicantDetail.FullApplicantName,
                        CAddress = applicantDetail.CAddress,
                        Mobile = applicantDetail.Mobile,
                        TotalAmount = plotDetail.TotalAmount,
                        NetAmount = plotDetail.NetAmount,
                        ApplicationFee = plotDetail.ApplicationFee,
                        EarnestMoneyDeposit = plotDetail.EarnestMoney,
                        GST = plotDetail.GST,
                        PaymentDate = transaction != null ? transaction.trans_date : DateTime.Now,
                        PlotArea = plotDetail.PlotArea,
                        SchemeType = lookupSchemeType != null ? lookupSchemeType.LookupName : "",
                        SchemeName = lookupSchemeName != null ? lookupSchemeName.LookupName : "",
                        SectorName = lookupSectorName != null ? lookupSectorName.LookupName : ""
                    }).ToList();
        }

        public int GetUserApplicationCount(int userId)
        {
            _db = new GidaGKPEntities();
            return (from application in _db.ApplicantApplicationDetails
                    where application.UserId == userId
                    select new ApplicationDetailModel
                    {
                        ApplicationId = application.ApplicationId,
                        ApplicationNumber = application.ApplicationNumber,
                    }).ToList().Count();
        }

        public ApplicantPlotDetailModel GetApplciantPlotDetailByApplicationId(int applicationId)
        {
            _db = new GidaGKPEntities();
            return (from plotDetail in _db.ApplicantPlotDetails
                    join app in _db.ApplicantApplicationDetails on plotDetail.ApplicationId equals app.ApplicationId
                    where app.ApplicationId == applicationId
                    select new ApplicantPlotDetailModel
                    {
                        ApplicationFee = plotDetail.ApplicationFee,
                        ApplicationId = plotDetail.ApplicationId,
                        ApplicationNumber = app.ApplicationNumber,
                        AppliedFor = plotDetail.AppliedFor,
                        CreationDate = plotDetail.CreationDate,
                        EarnestMoney = plotDetail.EarnestMoney,
                        EstimatedRate = plotDetail.EstimatedRate,
                        GST = plotDetail.GST,
                        Id = plotDetail.Id,
                        IndustryOwnership = plotDetail.IndustryOwnership,
                        NetAmount = plotDetail.NetAmount,
                        PaymentSchedule = plotDetail.PaymentSchedule,
                        PlotArea = plotDetail.PlotArea,
                        PlotRange = plotDetail.PlotRange,
                        RelationshipStatus = plotDetail.RelationshipStatus,
                        SchemeName = plotDetail.SchemeName,
                        SchemeType = plotDetail.SchemeType,
                        SectorName = plotDetail.SectorName,
                        SignatryDateOfBirth = plotDetail.SignatryDateOfBirth,
                        SignatryName = plotDetail.SignatryName,
                        SignatryPermanentAddress = plotDetail.SignatryPermanentAddress,
                        SignatryPermanentPhoneNumber = plotDetail.SignatryPermanentPhoneNumber,
                        SignatryPresentAddress = plotDetail.SignatryPresentAddress,
                        SignatryPresentPhoneNumber = plotDetail.SignatryPresentPhoneNumber,
                        TotalAmount = plotDetail.TotalAmount,
                        TotalInvestment = plotDetail.TotalInvestment,
                        UnitName = plotDetail.UnitName,
                        UserId = plotDetail.UserId
                    }).FirstOrDefault();
        }

        public ApplicantDetailModel GetApplicantPersonalDetail(int applicationId)
        {
            _db = new GidaGKPEntities();
            return (from applicantDetail in _db.ApplicantDetails
                    join app in _db.ApplicantApplicationDetails on applicantDetail.ApplicationId equals app.ApplicationId
                    where app.ApplicationId == applicationId
                    select new ApplicantDetailModel
                    {
                        AdhaarNumber = applicantDetail.AdhaarNumber,
                        UserId = applicantDetail.UserId,
                        SubCategory = applicantDetail.SubCategory,
                        ApplicantDOB = applicantDetail.ApplicantDOB,
                        CAddress = applicantDetail.CAddress,
                        Category = applicantDetail.Category,
                        CreationDate = DateTime.Now,
                        EmailId = applicantDetail.EmailId,
                        FName = applicantDetail.FName,
                        FullApplicantName = applicantDetail.FullApplicantName,
                        Gender = applicantDetail.Gender,
                        IdentiyProof = applicantDetail.IdentiyProof,
                        MName = applicantDetail.MName,
                        Mobile = applicantDetail.Mobile,
                        Nationality = applicantDetail.Nationality,
                        PAddress = applicantDetail.PAddress,
                        PAN = applicantDetail.PAN,
                        Phone = applicantDetail.Phone,
                        Religion = applicantDetail.Religion,
                        ResidentialProof = applicantDetail.ResidentialProof,
                        SName = applicantDetail.SName,
                        ApplicationId = UserData.ApplicationId
                    }).FirstOrDefault();
        }

        public ApplicantProjectDetailModel GetApplicantProjectDetail(int applicationId)
        {
            _db = new GidaGKPEntities();
            return (from applicantDetail in _db.ApplicantProjectDetails
                    join app in _db.ApplicantApplicationDetails on applicantDetail.ApplicationId equals app.ApplicationId
                    where app.ApplicationId == applicationId
                    select new ApplicantProjectDetailModel
                    {
                        CreationDate = applicantDetail.CreationDate,
                        EffluentTreatmentMeasures = applicantDetail.EffluentTreatmentMeasures,
                        FirstYearNoOfFax = applicantDetail.FirstYearNoOfFax,
                        FirstYearNoOfTelephone = applicantDetail.FirstYearNoOfTelephone,
                        FullApplicantName = applicantDetail.FullApplicantName,
                        FumesNatureQuantity = applicantDetail.FumesNatureQuantity,
                        GasChemicalComposition = applicantDetail.GasChemicalComposition,
                        GasQuantity = applicantDetail.GasQuantity,
                        Id = applicantDetail.Id,
                        LiquidChemicalComposition = applicantDetail.LiquidChemicalComposition,
                        LiquidQuantity = applicantDetail.LiquidQuantity,
                        PowerRequirement = applicantDetail.PowerRequirement,
                        ProjectEstimatedCost = applicantDetail.ProjectEstimatedCost,
                        ProposedCoveredArea = applicantDetail.ProposedCoveredArea,
                        ProposedIndustryType = applicantDetail.ProposedIndustryType,
                        ProposedInvestmentBuilding = applicantDetail.ProposedInvestmentBuilding,
                        ProposedInvestmentPlant = applicantDetail.ProposedInvestmentPlant,
                        ProposedOpenArea = applicantDetail.ProposedOpenArea,
                        PurpuseOpenArea = applicantDetail.PurpuseOpenArea,
                        Skilled = applicantDetail.Skilled,
                        SolidChemicalComposition = applicantDetail.SolidChemicalComposition,
                        SolidQuantity = applicantDetail.SolidQuantity,
                        UltimateNoOfFax = applicantDetail.UltimateNoOfFax,
                        UltimateNoOfTelephone = applicantDetail.UltimateNoOfTelephone,
                        UnSkilled = applicantDetail.UnSkilled,
                        UserId = applicantDetail.UserId,
                        ProposedInvestmentLand = applicantDetail.ProposedInvestmentLand,
                        ApplicationId = UserData.ApplicationId
                    }).FirstOrDefault();
        }

        public ApplicantBankDetailModel GetApplicantBankDetail(int applicationId)
        {
            _db = new GidaGKPEntities();
            return (from bankDetail in _db.ApplicantBankDetails
                    join app in _db.ApplicantApplicationDetails on bankDetail.ApplicationId equals app.ApplicationId
                    where app.ApplicationId == applicationId
                    select new ApplicantBankDetailModel
                    {
                        UserId = bankDetail.UserId,
                        AccountHolderName = bankDetail.AccountHolderName,
                        ApplicationId = bankDetail.ApplicationId,
                        BankAccountNo = bankDetail.BankAccountNo,
                        BankName = bankDetail.BankName,
                        BBAddress = bankDetail.BBAddress,
                        BBName = bankDetail.BBName,
                        CreationDate = bankDetail.CreationDate,
                        Id = bankDetail.Id,
                        IFSC_Code = bankDetail.IFSC_Code
                    }).FirstOrDefault();
        }

    }
}