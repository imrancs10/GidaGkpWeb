using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DataLayer;
using GidaGkpWeb.Global;
using System.Data.Entity;

namespace GidaGkpWeb.BAL
{
    public class ApplicantDetails
    {
        GidaGKPEntities _db = null;

        public Enums.CrudStatus SavePlotDetail(int userId, string AppliedFor, string SchemeType, string PlotRange, string SchemeName, string plotArea, string SectorName, string EstimatedRate, string PaymemtSchedule, string TotalInvestment, string ApplicationFee, string EarnestMoneyDeposite, string GST, string NetAmount, string TotalAmount, string IndustryOwnershipType, string UnitName, string Name, string dob, string PresentAddress, string PermanentAddress, string RelationshipStatus)
        {
            _db = new GidaGKPEntities();

            ApplicantFormStep step = new ApplicantFormStep()
            {
                UserId = userId,
                ApplicantStepCompleted = 1
            };
            _db.Entry(step).State = EntityState.Added;

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
            return _effectRow > 0 ? Enums.CrudStatus.Saved : Enums.CrudStatus.NotSaved;
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
    }
}