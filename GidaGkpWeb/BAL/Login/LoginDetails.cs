﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DataLayer;
using GidaGkpWeb.Global;
using System.Data.Entity;

namespace GidaGkpWeb.BAL.Login
{
    public class LoginDetails
    {
        GidaGkpEntities _db = null;

        /// <summary>
        /// Get Authenticate User credentials
        /// </summary>
        /// <param name="UserName">Username</param>
        /// <param name="Password">Password</param>
        /// <returns>Enums</returns>
        public Enums.LoginMessage GetLogin(string UserName, string Password)
        {
            //string _passwordHash = Utility.GetHashString(Password);
            _db = new GidaGkpEntities();

            var _userRow = _db.ApplicantUsers.Where(x => x.UserName.Equals(UserName) && x.Password.Equals(Password) && x.IsActive == true).FirstOrDefault();

            if (_userRow != null)
            {
                UserData.UserId = _userRow.Id;
                UserData.Username = _userRow.UserName;
                UserData.FullName = _userRow.FullName;
                UserData.Email = _userRow.Email;
                UserData.UserType = _userRow.UserType;
                return Enums.LoginMessage.Authenticated;
            }
            else
                return Enums.LoginMessage.InvalidCreadential;
        }

        public Enums.LoginMessage GetAdminLogin(string UserName, string Password)
        {
            //string _passwordHash = Utility.GetHashString(Password);
            _db = new GidaGkpEntities();

            var _userRow = _db.AdminUsers.Where(x => x.UserName.Equals(UserName) && x.Password.Equals(Password) && x.IsActive == true).FirstOrDefault();

            if (_userRow != null)
            {
                UserData.UserId = _userRow.Id;
                UserData.Username = _userRow.UserName;
                UserData.UserType = _userRow.UserType;
                return Enums.LoginMessage.Authenticated;
            }
            else
                return Enums.LoginMessage.InvalidCreadential;
        }

        public Enums.LoginMessage GetLoginByUsrrname(string UserName)
        {
            //string _passwordHash = Utility.GetHashString(Password);
            _db = new GidaGkpEntities();

            var _userRow = _db.ApplicantUsers.Where(x => x.UserName.Equals(UserName)).FirstOrDefault();

            if (_userRow != null)
            {
                UserData.UserId = _userRow.Id;
                UserData.Username = _userRow.UserName;
                UserData.FullName = _userRow.FullName;
                UserData.Email = _userRow.Email;
                UserData.UserType = _userRow.UserType;
                return Enums.LoginMessage.Authenticated;
            }
            else
                return Enums.LoginMessage.InvalidCreadential;
        }

        public Enums.CrudStatus RegisterApplicant(string fullName, string email, string contactno, string FName, string Adhaar, DateTime dob, string usrName, string password, string SchemeType, string SchemeName, string SectorName, string AllotmentNumber)
        {
            _db = new GidaGkpEntities();
            var _userRow = _db.ApplicantUsers.Where(x => x.UserName.Equals(usrName)).FirstOrDefault();

            int _effectRow = 0;
            if (_userRow == null)
            {
                ApplicantUser _newRecord = new ApplicantUser()
                {
                    FullName = fullName,
                    Email = email,
                    ContactNo = contactno,
                    FatherName = FName,
                    AadharNumber = Adhaar,
                    DOB = dob,
                    UserName = usrName,
                    Password = password,
                    AllotmentNumber = AllotmentNumber,
                    SchemeName = SchemeName,
                    SchemeType = SchemeType,
                    SectorName = SectorName,
                    CreationDate = DateTime.Now,
                    UserType = "GIDA",
                    IsActive = true
                };
                _db.Entry(_newRecord).State = EntityState.Added;
                _effectRow = _db.SaveChanges();
                return _effectRow > 0 ? Enums.CrudStatus.Saved : Enums.CrudStatus.NotSaved;
            }
            else
                return Enums.CrudStatus.DataAlreadyExist;
        }

        public ApplicantUser GetApplicantDetailByEmailAndMobileNumber(string emailId, string mobilenumber)
        {
            _db = new GidaGkpEntities();
            return _db.ApplicantUsers.Where(x => x.Email == emailId.Trim() && x.ContactNo == mobilenumber.Trim()).FirstOrDefault();
        }

        public ApplicantUser UpdateApplicantDetail(ApplicantUser info)
        {
            _db = new GidaGkpEntities();
            var _applicantRow = _db.ApplicantUsers.Where(x => x.Id.Equals(info.Id)).FirstOrDefault();
            if (_applicantRow != null)
            {
                _applicantRow.ResetCode = info.ResetCode;
                _applicantRow.Password = !string.IsNullOrEmpty(info.Password) ? info.Password : _applicantRow.Password;
                _db.Entry(_applicantRow).State = EntityState.Modified;
                _db.SaveChanges();
            }
            return _applicantRow;
        }

        public ApplicantUser GetApplicantDetailByresetCode(string resetCode)
        {
            _db = new GidaGkpEntities();
            return _db.ApplicantUsers.Where(x => x.ResetCode == resetCode).FirstOrDefault();
        }

        public ApplicantUser GetApplicantDetailByMobileNumberOrEmail(string UserId)
        {
            _db = new GidaGkpEntities();
            return _db.ApplicantUsers.Where(x => x.ContactNo == UserId.Trim() || x.Email == UserId.Trim()).FirstOrDefault();
        }
    }
}