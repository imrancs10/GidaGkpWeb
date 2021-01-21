using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DataLayer;
using GidaGkpWeb.Global;
using System.Data.Entity;
using GidaGkpWeb.Models.Masters;

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

            ApplicantFormStep step = new ApplicantFormStep()
            {
                UserId = userId,
                ApplicantStepCompleted = 1
            };
            _db.Entry(step).State = EntityState.Added;

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
            ApplicantApplicationDetail app = new ApplicantApplicationDetail()
            {
                UserId = userId,
                ApplicationNumber = DateTime.Now.Month.ToString() + DateTime.Now.Year.ToString() + SectorDescription.Replace("Sector ", "") + maxId.ToString().PadLeft(4, '0')
            };
            _db.Entry(app).State = EntityState.Added;

            int _effectRow = 0;
            ApplicantPlotDetail _newRecord = new ApplicantPlotDetail()
            {
                ApplicationFee = Convert.ToDecimal(ApplicationFee),
                UserId = userId,
                AppliedFor = AppliedFor,
                CreationDate = DateTime.Now,
                EarnestMoney = Convert.ToDecimal(EarnestMoneyDeposite),
                EstimatedRate = EstimatedRate,
                GST = Convert.ToDecimal(GST),
                IndustryOwnership = IndustryOwnershipType,
                NetAmount = Convert.ToDecimal(NetAmount),
                PaymentSchedule = PaymemtSchedule,
                PlotArea = plotArea,
                PlotRange = PlotRange,
                RelationshipStatus = RelationshipStatus,
                SchemeName = SchemeName,
                SchemeType = SchemeType,
                SectorName = SectorName,
                SignatryDateOfBirth = Convert.ToDateTime(dob),
                SignatryName = Name,
                SignatryPermanentAddress = PermanentAddress,
                SignatryPresentAddress = PresentAddress,
                TotalAmount = Convert.ToDecimal(TotalAmount),
                TotalInvestment = Convert.ToDecimal(TotalInvestment),
                UnitName = UnitName
            };
            _db.Entry(_newRecord).State = EntityState.Added;
            _effectRow = _db.SaveChanges();
            return _effectRow > 0 ? app.ApplicationNumber : "Error";
        }

        public Enums.CrudStatus SaveApplicantDetail(int userId, string FullName, string FName, string MName, string SName, string DOB, string Gender, string Reservation, string Nationality, string AdhaarNo, string PAN, string MobileNo, string Phone, string Email, string Religion, string SubCategory, string CAddress, string PAddress, string IdentityProof, string ResidentialProof)
        {
            _db = new GidaGKPEntities();

            ApplicantFormStep step = new ApplicantFormStep()
            {
                UserId = userId,
                ApplicantStepCompleted = 2
            };
            _db.Entry(step).State = EntityState.Added;

            int _effectRow = 0;
            ApplicantDetail _newRecord = new ApplicantDetail()
            {
                AdhaarNumber = AdhaarNo,
                UserId = userId,
                SubCategory = SubCategory,
                ApplicantDOB = Convert.ToDateTime(DOB),
                CAddress = CAddress,
                Category = SubCategory,
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
                SName = SName
            };
            _db.Entry(_newRecord).State = EntityState.Added;
            _effectRow = _db.SaveChanges();
            return _effectRow > 0 ? Enums.CrudStatus.Saved : Enums.CrudStatus.NotSaved;
        }

        public Enums.CrudStatus SaveProjectDetail(int userId, string ProposedIndustryType, string ProjectEstimatedCost, string ProposedCoveredArea, string ProposedOpenArea, string PurpuseOpenArea, string ProposedInvestmentLand, string ProposedInvestmentBuilding, string ProposedInvestmentPlant, string FumesNatureQuantity, string LiquidQuantity, string LiquidChemicalComposition, string SolidQuantity, string SolidChemicalComposition, string GasQuantity, string GasChemicalComposition, string PowerRequirement, string FirstYearNoOfTelephone, string FirstYearNoOfFax, string UltimateNoOfTelephone, string UltimateNoOfFax, string Skilled, string UnSkilled)
        {
            _db = new GidaGKPEntities();

            ApplicantFormStep step = new ApplicantFormStep()
            {
                UserId = userId,
                ApplicantStepCompleted = 3
            };
            _db.Entry(step).State = EntityState.Added;

            int _effectRow = 0;
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
                UnSkilled = UnSkilled
            };
            _db.Entry(_newRecord).State = EntityState.Added;
            _effectRow = _db.SaveChanges();
            return _effectRow > 0 ? Enums.CrudStatus.Saved : Enums.CrudStatus.NotSaved;
        }

        public Enums.CrudStatus SaveBankDetail(int userId, string BankAccountName, string BankAccountNo, string BankName, string BranchName, string BranchAddress, string IFSCCode)
        {
            _db = new GidaGKPEntities();

            ApplicantFormStep step = new ApplicantFormStep()
            {
                UserId = userId,
                ApplicantStepCompleted = 4
            };
            _db.Entry(step).State = EntityState.Added;

            int _effectRow = 0;
            ApplicantBankDetail _newRecord = new ApplicantBankDetail()
            {
                UserId = userId,
                CreationDate = DateTime.Now,
                AccountHolderName = BankAccountName,
                BankAccountNo = BankAccountNo,
                BankName = BankName,
                BBAddress = BranchAddress,
                BBName = BranchName,
                IFSC_Code = IFSCCode
            };
            _db.Entry(_newRecord).State = EntityState.Added;
            _effectRow = _db.SaveChanges();
            return _effectRow > 0 ? Enums.CrudStatus.Saved : Enums.CrudStatus.NotSaved;
        }

        public Enums.CrudStatus SaveApplicantDocument(int userId, ApplicantUploadDoc docDetail)
        {
            _db = new GidaGKPEntities();

            ApplicantFormStep step = new ApplicantFormStep()
            {
                UserId = userId,
                ApplicantStepCompleted = 5
            };
            _db.Entry(step).State = EntityState.Added;

            int _effectRow = 0;

            _db.Entry(docDetail).State = EntityState.Added;
            _effectRow = _db.SaveChanges();
            return _effectRow > 0 ? Enums.CrudStatus.Saved : Enums.CrudStatus.NotSaved;
        }

        public ApplicationDetailModel GetUserPlotDetail(int userId)
        {
            _db = new GidaGKPEntities();
            return (from application in _db.ApplicantApplicationDetails
                    join applicantDetail in _db.ApplicantDetails on application.UserId equals applicantDetail.UserId
                    join plotDetail in _db.ApplicantPlotDetails on application.UserId equals plotDetail.UserId
                    join transaction in _db.ApplicantTransactionDetails on applicantDetail.UserId equals transaction.UserId
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
                        PaymentDate = transaction.trans_date
                    }).FirstOrDefault();
        }

        public AcknowledgementDetailModel GetAcknowledgementDetail(int userId)
        {
            _db = new GidaGKPEntities();
            return (from applicationDetail in _db.ApplicantApplicationDetails
                    join applicantDetail in _db.ApplicantDetails on applicationDetail.UserId equals applicantDetail.UserId
                    join plotDetail in _db.ApplicantPlotDetails on applicationDetail.UserId equals plotDetail.UserId
                    join transactionDetail in _db.ApplicantTransactionDetails on applicantDetail.UserId equals transactionDetail.UserId
                    join projectDetail in _db.ApplicantProjectDetails on applicantDetail.UserId equals projectDetail.UserId
                    join documentDetail in _db.ApplicantUploadDocs on applicantDetail.UserId equals documentDetail.UserId
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
                        IndustryOwnership = plotDetail.IndustryOwnership,
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
                        RelationshipStatus = plotDetail.RelationshipStatus,
                        SectorName = plotDetail.SectorName,
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
    }


}