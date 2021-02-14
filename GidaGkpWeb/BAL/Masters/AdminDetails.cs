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
        GidaGkpEntities _db = null;
        public List<ApplicationUserModel> GetApplicantUser()
        {
            try
            {
                _db = new GidaGkpEntities();
                return (from user in _db.ApplicantUsers
                        where user.UserType != "Test"
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
                            DOB = user.DOB,
                            UserName = user.UserName,
                            IsActive = user.IsActive
                        }).Distinct().ToList();
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
        public List<ApplicationUserModel> GetApplicantUserDetail()
        {
            try
            {
                _db = new GidaGkpEntities();
                return (from user in _db.ApplicantUsers
                        join application1 in _db.ApplicantApplicationDetails on user.Id equals application1.UserId into application2
                        from application in application2.DefaultIfEmpty()
                        join doc1 in _db.ApplicantUploadDocs on user.Id equals doc1.UserId into doc2
                        from doc in doc2.DefaultIfEmpty()
                        join transaction1 in _db.ApplicantTransactionDetails on application.ApplicationId equals transaction1.ApplicationId into transaction2
                        from transaction in transaction2.DefaultIfEmpty()
                        where user.UserType != "Test"
                        select new ApplicationUserModel
                        {
                            ApplicationNumber = doc != null ? application.ApplicationNumber : "",
                            PaidAmount = transaction != null ? transaction.amount : "",
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
                            DOB = user.DOB,
                            UserName = user.UserName,
                            IsActive = user.IsActive
                        }).Distinct().ToList();
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
                _db = new GidaGkpEntities();
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

        public Enums.CrudStatus SaveNotice(AdminNotice notice)
        {
            try
            {
                _db = new GidaGkpEntities();
                int _effectRow = 0;
                if (notice.Id > 0)
                {
                    var adminNotice = _db.AdminNotices.Where(x => x.Id == notice.Id).FirstOrDefault();
                    if (adminNotice != null)
                    {
                        adminNotice.IsActive = notice.IsActive;
                        adminNotice.Department = notice.Department;
                        if (notice.NoticeDocumentFile != null)
                        {
                            adminNotice.NoticeDocumentFile = notice.NoticeDocumentFile;
                            adminNotice.NoticeDocumentFileType = notice.NoticeDocumentFileType;
                            adminNotice.NoticeDocumentName = notice.NoticeDocumentName;
                        }
                        adminNotice.NoticeNewTag = notice.NoticeNewTag;
                        adminNotice.Notice_Date = notice.Notice_Date;
                        adminNotice.Notice_title = notice.Notice_title;
                        adminNotice.NoticeTypeId = notice.NoticeTypeId;
                        _db.Entry(adminNotice).State = EntityState.Modified;
                        _effectRow = _db.SaveChanges();
                    }
                }
                else
                {
                    _db.Entry(notice).State = EntityState.Added;
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

        public List<AdminNoticeModel> GetNoticeList()
        {
            try
            {
                _db = new GidaGkpEntities();
                return (from notice in _db.AdminNotices
                        join departmentLookup1 in _db.Lookups on notice.Department equals departmentLookup1.LookupId into departmentLookup2
                        from departmentLookup in departmentLookup2.DefaultIfEmpty()
                        join noticeTypeLookup1 in _db.Lookups on notice.NoticeTypeId equals noticeTypeLookup1.LookupId into noticeTypeLookup2
                        from noticeTypeLookup in noticeTypeLookup2.DefaultIfEmpty()
                        select new AdminNoticeModel
                        {
                            NoticeTypeId = notice.NoticeTypeId,
                            DepartmentName = departmentLookup.LookupName,
                            Id = notice.Id,
                            IsActive = notice.IsActive,
                            NoticeDocumentFile = notice.NoticeDocumentFile,
                            NoticeDocumentFileType = notice.NoticeDocumentFileType,
                            NoticeDocumentName = notice.NoticeDocumentName,
                            NoticeNewTag = notice.NoticeNewTag,
                            Notice_Date = notice.Notice_Date,
                            Notice_title = notice.Notice_title,
                            Notice_Type = noticeTypeLookup.LookupName,
                            CreationDate = notice.CreationDate
                        }).Distinct().ToList();
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
                return new List<AdminNoticeModel>();
            }
        }

        public AdminNoticeModel GetNoticeById(int noticeId)
        {
            try
            {
                _db = new GidaGkpEntities();
                return (from notice in _db.AdminNotices
                        join departmentLookup1 in _db.Lookups on notice.Department equals departmentLookup1.LookupId into departmentLookup2
                        from departmentLookup in departmentLookup2.DefaultIfEmpty()
                        join noticeTypeLookup1 in _db.Lookups on notice.NoticeTypeId equals noticeTypeLookup1.LookupId into noticeTypeLookup2
                        from noticeTypeLookup in noticeTypeLookup2.DefaultIfEmpty()
                        where notice.Id == noticeId
                        select new AdminNoticeModel
                        {
                            NoticeTypeId = notice.NoticeTypeId,
                            DepartmentName = departmentLookup.LookupName,
                            Department = notice.Department,
                            Id = notice.Id,
                            IsActive = notice.IsActive,
                            NoticeDocumentFileType = notice.NoticeDocumentFileType,
                            NoticeDocumentName = notice.NoticeDocumentName,
                            NoticeNewTag = notice.NoticeNewTag,
                            Notice_Date = notice.Notice_Date,
                            Notice_title = notice.Notice_title,
                            Notice_Type = noticeTypeLookup.LookupName
                        }).FirstOrDefault();
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
                return new AdminNoticeModel();
            }
        }
    }
}