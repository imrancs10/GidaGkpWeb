using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DataLayer;
using GidaGkpWeb.Global;
using System.Data.Entity;
using GidaGkpWeb.Models;
using System.Data.Entity.Validation;

namespace GidaGkpWeb.BAL
{
    public class AdminDetails
    {
        GidaGKPEntities _db = null;

        public List<ApplicationUserModel> GetApplicantUserDetail()
        {
            try
            {
                _db = new GidaGKPEntities();
                return (from user in _db.ApplicantUsers
                        select new ApplicationUserModel
                        {
                            AadharNumber = user.AadharNumber,
                            ContactNo = user.ContactNo,
                            CreationDate = user.CreationDate,
                            Email = user.Email,
                            FatherName = user.FatherName,
                            FullName = user.FullName,
                            Id = user.Id,
                            SchemeName = user.SchemeName,
                            SchemeType = user.SchemeType,
                            SectorName = user.SectorName,
                            UserType = user.UserType,
                            UserName = user.UserName,
                            IsActive = user.IsActive
                        }).ToList();
            }
            catch (DbEntityValidationException e)
            {
                foreach (var eve in e.EntityValidationErrors)
                {
                    foreach (var ve in eve.ValidationErrors)
                    {
                        Elmah.ErrorLog.GetDefault(HttpContext.Current).Log(new Elmah.Error(e));
                    }
                }
                return new List<ApplicationUserModel>();
            }
        }

        public Enums.CrudStatus ActivateDeActivateUser(int userId)
        {
            try
            {
                _db = new GidaGKPEntities();
                int _effectRow = 0;
                var applicantUser = _db.ApplicantUsers.Where(x => x.Id == userId).FirstOrDefault();
                if (applicantUser != null)
                {
                    applicantUser.IsActive = !applicantUser.IsActive;
                    _db.Entry(applicantUser).State = EntityState.Modified;
                    _effectRow = _db.SaveChanges();
                }
                return _effectRow > 0 ? Enums.CrudStatus.Saved : Enums.CrudStatus.NotSaved;
            }
            catch (DbEntityValidationException e)
            {
                foreach (var eve in e.EntityValidationErrors)
                {
                    foreach (var ve in eve.ValidationErrors)
                    {
                        Elmah.ErrorLog.GetDefault(HttpContext.Current).Log(new Elmah.Error(e));
                    }
                }
                return Enums.CrudStatus.InternalError;
            }


        }
    }
}