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
    }
}