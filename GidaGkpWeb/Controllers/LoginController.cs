using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using System.Web.Security;
using DataLayer;
using GidaGkpWeb.BAL.Login;
using GidaGkpWeb.Global;
using GidaGkpWeb.Infrastructure;
using GidaGkpWeb.Infrastructure.Utility;

namespace GidaGkpWeb.Controllers
{
    public class LoginController : CommonController
    {
        // GET: Login
        public ActionResult ApplicantLogin()
        {
            ViewData["LoginPage"] = true;
            return View();
        }

        public ActionResult GetLogin(string username, string password)
        {
            LoginDetails _details = new LoginDetails();
            string _response = string.Empty;
            Enums.LoginMessage message = _details.GetLogin(username, password);
            _response = LoginResponse(message);
            if (message == Enums.LoginMessage.Authenticated)
            {
                setUserClaim();
                return RedirectToAction("ApplicantDashboard", "Masters");
            }
            else
            {
                SetAlertMessage(_response, "Login Response");
                return View("ApplicantLogin");
            }
        }

        public ActionResult ApplicantRegistration(string actionName = "")
        {
            if (actionName == "getotpscreen")
            {
                ViewData["registerAction"] = "getotpscreen";
            }
            return View();
        }

        public async Task<ActionResult> ApplicantLoginOTP(string fullName, string email, string contactno, string FName, string Adhaar, string dob, string usrName, string password, string cpassword, string SchemeType, string SchemeName, string SectorName, string AllotmentNumber)
        {
            string emailRegEx = @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z";
            if (contactno.Trim().Length != 10)
            {
                SetAlertMessage("Please Enter correct Mobile Number", "Register");
                return RedirectToAction("ApplicantRegistration");
            }
            else if (!Regex.IsMatch(email, emailRegEx, RegexOptions.IgnoreCase))
            {
                SetAlertMessage("Please Enter correct Email Address", "Register");
                return RedirectToAction("ApplicantRegistration");
            }
            else if (password.Trim() != cpassword.Trim())
            {
                SetAlertMessage("Password and Confirm Password not matched", "Register Response");
                return View("ApplicantRegistration");
            }
            else
            {
                LoginDetails _details = new LoginDetails();
                string _response = string.Empty;
                Enums.LoginMessage message = _details.GetLogin(usrName, password);
                if (message == Enums.LoginMessage.Authenticated)
                {
                    SetAlertMessage("user name already in our database, kindly change it or reset your account.", "Register");
                    return RedirectToAction("ApplicantRegistration");
                }
                string verificationCode = VerificationCodeGeneration.GenerateDeviceVerificationCode();
                ApplicantUser applicantModel = new ApplicantUser()
                {
                    AadharNumber = Adhaar,
                    ContactNo = contactno,
                    DOB = Convert.ToDateTime(dob),
                    Email = email,
                    FatherName = FName,
                    FullName = fullName,
                    Password = password,
                    UserName = usrName,
                    AllotmentNumber = AllotmentNumber,
                    SchemeName = SchemeName,
                    SchemeType = SchemeType,
                    SectorName = SectorName
                };
                if (applicantModel != null)
                {
                    SendMailFordeviceVerification(fullName, email, verificationCode, contactno);
                    Session["otp"] = verificationCode;
                    Session["ApplicantModel"] = applicantModel;
                    return RedirectToAction("ApplicantRegistration", new { actionName = "getotpscreen" });
                }
                else
                {
                    SetAlertMessage("User is already register", "Register");
                    return RedirectToAction("ApplicantRegistration");
                }
            }
        }

        [HttpPost]
        public ActionResult verifyOTP(string OTP)
        {
            if (Convert.ToString(Session["otp"]) == OTP)
            {
                var applicantModel = Session["ApplicantModel"] as ApplicantUser;

                RegisterApplicant(applicantModel.FullName, applicantModel.Email, applicantModel.ContactNo, applicantModel.FullName, applicantModel.AadharNumber, applicantModel.DOB.Value, applicantModel.UserName, applicantModel.Password, applicantModel.SchemeType, applicantModel.SchemeName, applicantModel.SectorName, applicantModel.AllotmentNumber);
                SetAlertMessage("Registration successful", "Register Response");
                return RedirectToAction("ApplicantLogin");
            }
            else
            {
                SetAlertMessage("OTP not matched", "Register");
                return RedirectToAction("ApplicantRegistration", new { actionName = "getotpscreen" });
            }
        }

        private async Task SendMailFordeviceVerification(string fullName, string email, string verificationCode, string mobilenumber)
        {
            await Task.Run(() =>
            {
                //Send Email
                Message msg = new Message()
                {
                    MessageTo = email,
                    MessageNameTo = fullName,
                    OTP = verificationCode,
                    Subject = "Verify Email",
                    Body = EmailHelper.GetDeviceVerificationEmail(fullName, verificationCode)
                };
                ISendMessageStrategy sendMessageStrategy = new SendMessageStrategyForEmail(msg);
                sendMessageStrategy.SendMessages();

                //Send SMS
                //msg.Body = "Hello " + string.Format("{0} {1}", firstname, lastname) + "\nAs you requested, here is a OTP " + verificationCode + " you can use it to verify your mobile number before 15 minutes.\n Regards:\n Patient Portal(RMLHIMS)";
                //msg.MessageTo = mobilenumber;
                //msg.MessageType = MessageType.OTP;
                //sendMessageStrategy = new SendMessageStrategyForSMS(msg);
                //sendMessageStrategy.SendMessages();
            });
        }
        private void RegisterApplicant(string fullName, string email, string contactno, string FName, string Adhaar, DateTime dob, string usrName, string password, string SchemeType, string SchemeName, string SectorName, string AllotmentNumber)
        {
            LoginDetails _details = new LoginDetails();
            Enums.CrudStatus message = _details.RegisterApplicant(fullName, email, contactno, FName, Adhaar, dob, usrName, password, SchemeType, SchemeName, SectorName, AllotmentNumber);
        }

        private void setUserClaim()
        {
            CustomPrincipalSerializeModel serializeModel = new CustomPrincipalSerializeModel();
            serializeModel.Id = UserData.UserId;
            serializeModel.FirstName = string.IsNullOrEmpty(UserData.FirstName) ? string.Empty : UserData.FirstName;
            serializeModel.MiddleName = string.IsNullOrEmpty(UserData.MiddleName) ? string.Empty : UserData.MiddleName;
            serializeModel.LastName = string.IsNullOrEmpty(UserData.LastName) ? string.Empty : UserData.LastName;
            serializeModel.LastName = string.IsNullOrEmpty(UserData.Username) ? string.Empty : UserData.Username;
            serializeModel.Email = string.IsNullOrEmpty(UserData.Email) ? string.Empty : UserData.Email;

            JavaScriptSerializer serializer = new JavaScriptSerializer();

            string userData = serializer.Serialize(serializeModel);

            FormsAuthenticationTicket authTicket = new FormsAuthenticationTicket(
                     1,
                     UserData.Email,
                     DateTime.Now,
                     DateTime.Now.AddMinutes(15),
                     false,
                     userData);

            string encTicket = FormsAuthentication.Encrypt(authTicket);
            HttpCookie faCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encTicket);
            Response.Cookies.Add(faCookie);
        }

    }
}