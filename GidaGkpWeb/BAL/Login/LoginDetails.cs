using System;
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
        GidaGKPEntities _db = null;

        /// <summary>
        /// Get Authenticate User credentials
        /// </summary>
        /// <param name="UserName">Username</param>
        /// <param name="Password">Password</param>
        /// <returns>Enums</returns>
        public Enums.LoginMessage GetLogin(string UserName, string Password)
        {
            //string _passwordHash = Utility.GetHashString(Password);
            _db = new GidaGKPEntities();

            var _userRow = _db.ApplicantUsers.Where(x => x.UserName.Equals(UserName) && x.Password.Equals(Password)).FirstOrDefault();

            if (_userRow != null)
            {
                UserData.UserId = _userRow.Id;
                UserData.Username = _userRow.UserName;
                UserData.FirstName = _userRow.FullName;
                UserData.MiddleName = _userRow.Email;
                return Enums.LoginMessage.Authenticated;
            }
            else
                return Enums.LoginMessage.InvalidCreadential;
        }

        public Enums.CrudStatus RegisterApplicant(string fullName, string email, string contactno, string FName, string Adhaar, DateTime dob, string usrName, string password, string SchemeType, string SchemeName, string SectorName, string AllotmentNumber)
        {
            _db = new GidaGKPEntities();
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
                    SectorName = SectorName
                };
                _db.Entry(_newRecord).State = EntityState.Added;
                _effectRow = _db.SaveChanges();
                return _effectRow > 0 ? Enums.CrudStatus.Saved : Enums.CrudStatus.NotSaved;
            }
            else
                return Enums.CrudStatus.DataAlreadyExist;
        }
    }
}