using AdminLTE_Template1.Models;
using NLog;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace AdminLTE_Template1.Controllers
{

    public class LoginController : Controller
    {
        public static int countOfFailedAttempt = 0;
        public static string variableCheckForCode= null;
        public static string blockSameUsername = null;
        public static string blockSameEmail = null;
        public static string blockSameUsername2 = null;
        public static string block = "OFF";
        public static int UserId = 0;
        public static bool mailSent = false;

        PharmacySystemEntities db = new PharmacySystemEntities();
        private static Logger logger = LogManager.GetCurrentClassLogger();

        public ActionResult ChangePassword()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ChangePassword(CustomAdminLogin model,string oldpass)
        {
            AdminLogin user = db.AdminLogins.Where(x => x.ID== UserId).SingleOrDefault();
            if (user.Password == Crypto.Hash(oldpass))
            {
                string pattern = @"^(?=.*[A-Z])(?=.*[a-z])(?=.*[0-9]).{6,20}$";
                Match result = Regex.Match(model.Password, pattern);

                if (!result.Success)
                {
                    ViewBag.Samepassword = "Passwords must use be contain at least 6 character and at least one character of  lowercase letters, uppercase letters and numbers.";
                    return View();
                }

                else if (user.Password== Crypto.Hash(model.Password))
                {
                    ViewBag.Samepassword = "Cant use same password";
                    return View();

                }
                user.Password = Crypto.Hash(model.Password);
                try
                {
                    db.SaveChanges();
                    SendConfirmationEmailPasswordChanged(user.Email);
                    return RedirectToAction("index", "Home");
                }
                catch (DbEntityValidationException e)
                {
                    foreach (var eve in e.EntityValidationErrors)
                    {
                        Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                            eve.Entry.Entity.GetType().Name, eve.Entry.State); 
                        foreach (var ve in eve.ValidationErrors)
                        {
                            Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                                ve.PropertyName, ve.ErrorMessage);
                           
                        }
                    }
                    return View();
                }

            }
            else
            {
                ViewBag.passwordFail = "Wrong Password";
                return View();
            }


        }

        public ActionResult Index()
        {
            ViewBag.disabled = block;
            return View();
        }

        [HttpPost]
        public ActionResult Index(AdminLogin model, string returnUrl)
        {
            if (model.Password == null)
            {
                logger.Error("You have try to attempt login with Unvalid data");

                ViewBag.passwordEmpty = "Username & Password are required";
                return View();
            }
            
            Session["User"] = null;
            string nPassword = Crypto.Hash(model.Password);
            var dataItem = db.AdminLogins.Where(x => x.UserName == model.UserName && x.Password == nPassword).FirstOrDefault();
            
            

            if (countOfFailedAttempt == 2 && blockSameUsername2==model.UserName && blockSameEmail == model.Email)
            {
                
                ViewData["ErrorMessage"] = "Access Denied ";
                ViewBag.disabled = "ON";
                var v = db.AdminLogins.Where(a => a.UserName == blockSameUsername2).FirstOrDefault();
                if (v != null)
                {
                    v.IsBlocked = true;
                    logger.Error("You account is blocked");
                    db.SaveChanges();
                    return View();

                }
                else
                {
                    return View();
                }
                
            }
            else if (countOfFailedAttempt == 1 && dataItem == null && blockSameUsername == model.UserName)
            {
                //ViewData["ErrorMessage"] = "You Have One more Attempt.If Not You'll Unable to Access This Site ";
                ViewData["ErrorMessage"] = "UserName or Password is incorrect. Try again ";
                if (blockSameUsername == model.UserName && blockSameEmail == model.Email)
                {
                    blockSameUsername2 = blockSameUsername;
                    countOfFailedAttempt++;
                }
                logger.Error("You have try to attempt login with Unvalid data");
                return View();
            }
            else if (string.IsNullOrEmpty(model.UserName) || string.IsNullOrEmpty(model.Password))
            {
                if (blockSameUsername == model.UserName)
                {
                    countOfFailedAttempt++;
                }

                blockSameUsername = model.UserName;
                var dataItem1 = db.AdminLogins.Where(x => x.UserName == blockSameUsername).FirstOrDefault();
                if (dataItem1.IsBlocked == true)
                {
                    ViewBag.disabled = "ON";
                    logger.Error("You have try to attempt login which already blocked account");
                    return View();
                }
                ViewData["ErrorMessage"] = "UserName and Password Fields are Required. Try again ";
                if (countOfFailedAttempt == 3)
                {
                    block = "ON";
                    ViewBag.disabled = block;
                    ViewData["ErrorMessage"] = "No More Attempt ";               
                }
                //countOfFailedAttempt++;
                logger.Error("You have try to attempt login with Unvalid data");
                return View();
            }

            else if (dataItem == null)
            {
                if (blockSameUsername == model.UserName)
                {
                    countOfFailedAttempt++;
                }
                blockSameUsername = model.UserName;
                blockSameEmail = model.Email;
                var dataItem1 = db.AdminLogins.Where(x => x.UserName == blockSameUsername ).FirstOrDefault();
                if (dataItem1 != null)
                {
                    if (dataItem1.IsBlocked == true)
                    {
                        ViewBag.disabled = "ON";
                        return View();
                    }
                }
                logger.Error("You have try to attempt login with Unvalid data");
                ViewData["ErrorMessage"] = "UserName or Password is incorrect. Try again";
                //if (countOfFailedAttempt == 3)
                //{
                //    ViewData["ErrorMessage"] = "No More Attempt ";
                //}
                //countOfFailedAttempt++;
                return View();
            }

            else
            {
                FormsAuthentication.SetAuthCookie(dataItem.UserName, false);
                if (Url.IsLocalUrl(returnUrl) && returnUrl.Length > 1 && returnUrl.StartsWith("/")
                         && !returnUrl.StartsWith("//") && !returnUrl.StartsWith("/\\"))
                {
                    return Redirect(returnUrl);
                }

                else
                {
                    Session["User"] = model.UserName;
                    var data = db.AdminLogins.Where(x => x.UserName == dataItem.UserName).SingleOrDefault();
                    UserId = data.ID;
                    countOfFailedAttempt = 0 ;
                    logger.Info("Successfully Login.You have visited the Home Page.");
                    return RedirectToAction("Index", "Home");
                }
            }
        }
        
        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            Session.Abandon(); // it will clear the session at the end of request
            return RedirectToAction("index", "Login");
        }

        public ActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ForgotPassword(string email)
        {
            if (string.IsNullOrEmpty(email))
            {
                ViewBag.NoEmail = "Email Required";
                return View();
            }
                AdminLogin userForgotEmail = db.AdminLogins.Where(x => x.Email == email.Trim()).SingleOrDefault();
            if (userForgotEmail == null)
            {
                ViewBag.NoEmail = "Email not exists";
                return View();
            }
            string NewPassword = GenerateRandomPassword(8);
            UserId = userForgotEmail.ID;      
            SendVerificationLinkEmail(email, NewPassword);
            if (mailSent == false)
            {
                ViewBag.noSentEmail = "True";
                ViewBag.EmailNetConMsg = "There is no Internet connection";
                return View();

            }
            return View("GetSecurityCode");
        }

        private string GenerateRandomPassword(int length)
        {
            string allowedChars = "0123456789";
            char[] chars = new char[length];
            Random rd = new Random();
            for (int i = 0; i < length; i++)
            {
                chars[i] = allowedChars[rd.Next(0, allowedChars.Length)];
            }
            variableCheckForCode = new string(chars);
            return new string(chars);
        }

        [NonAction]
        public void SendVerificationLinkEmail(string emailID, string code)
        {
            var fromEmail = new MailAddress("johnsaninfosystem@gmail.com", "Pharmacy System");
            var toEmail = new MailAddress(emailID);
            var fromEmailPassword = "dialog077"; // Replace with actual password
            string subject = "Account Password Reset";

            string body = "<br/><h2>Paasword Reset Code</h2><br/>Please use this code to reset the password for the  account " +
                emailID.Substring(0, 3) + "*****" + emailID.Substring(emailID.IndexOf("@")) +
                 "<br/><br/>Here is your code :<b> " + code + "</b>";

            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(fromEmail.Address, fromEmailPassword)
            };

            using (var message = new MailMessage(fromEmail, toEmail)
            {
                Subject = subject,
                Body = body,
                IsBodyHtml = true
            })
                try
                {
                    smtp.Send(message);
                    mailSent = true;
                }
                catch (Exception)
                {
                    mailSent = false;
                }
        }

        public ActionResult GetSecurityCode()
        {
            return View();
        }

        public ActionResult CheckForSecurityKey(string securityKey)
        {
            if (string.IsNullOrEmpty(securityKey))
            {
                ViewBag.WrongSecurityKey = "Required";
                return View("GetSecurityCode");
            }
            securityKey = securityKey.Trim();
            ViewBag.InsertTokenMust = "OFF";
            if (securityKey == variableCheckForCode)
            {
                ViewBag.InsertTokenMust = "ON";
                //return View("PasswordChangeAfterForgot");
                return View("PasswordChangeAfterForgot");
            }
            ViewBag.WrongSecurityKey = "Code is not correct.Try Again";
            return View("GetSecurityCode");
        }

        public ActionResult PasswordChangeAfterForgot(CustomAdminLogin userData)
        {
            ViewBag.InsertTokenMust = "ON";
            AdminLogin user = db.AdminLogins.Where(x => x.ID == UserId).SingleOrDefault();
           
            if (userData.Password==null)
            {
                ViewBag.Samepassword = "Passwords is Required";
                return View();
            }

            string pattern = @"^(?=.*[A-Z])(?=.*[a-z])(?=.*[0-9]).{6,20}$";
            Match result = Regex.Match(userData.Password, pattern);

            if (!result.Success)
            {
                ViewBag.Samepassword = "Passwords must use be contain at least 6 character and at least one character of  lowercase letters, uppercase letters and numbers.";
                return View();
            }
            else if (user.Password == Crypto.Hash(userData.Password))
            {
                ViewBag.Samepassword = "This password used recently.Choose New One";
                return View();

            }
            else if (userData.Password != userData.ConfirmPassword)
            {
                ViewBag.NotConfirmPassword = "The password and confirmation password do not match.";
                return View();

            }
            user.Password = Crypto.Hash(userData.Password);
            user.IsBlocked = false;
            //db.Entry(userData).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
                Session["User"] = user.UserName;
                countOfFailedAttempt = 0;
                SendConfirmationEmailPasswordChanged(user.Email);
                return RedirectToAction("index", "Home");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return View();
            }
            
            //ViewBag.InsertTokenMust = "ON";
            //AdminLogin user1 = db.AdminLogins.Where(x => x.ID == UserId).SingleOrDefault();
            //return View(user1);
            //AdminLogin user = db.AdminLogins.Where(x => x.ID == UserId).SingleOrDefault();

            //if (user.Password == Crypto.Hash(userData.Password))
            //{
            //    ViewBag.passwordSame = "This Password has been used in recent time.Choose another";
            //    return View();

            //}
            //user.Password = Crypto.Hash(userData.Password);
            //try
            //{
            //    db.SaveChanges();
            //    SendConfirmationEmailPasswordChanged(user.Email);
            //    return RedirectToAction("index", "Home");
            //}
            //catch (Exception)
            //{
            //    return View();
            //}
           
        }

        [HttpPost]
        public ActionResult PasswordSave([Bind(Include = "Password")] CustomAdminLogin userData)
        {           

            AdminLogin user = db.AdminLogins.Where(x => x.ID == UserId).SingleOrDefault();
            if (user.Password == Crypto.Hash(userData.Password))
            {
                ViewBag.Samepassword = "This password used recently.Choose New One";
                return View();

            }
            user.Password = Crypto.Hash(userData.Password);
            user.IsBlocked = false;
            //db.Entry(userData).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
                Session["User"] = user.UserName;
                countOfFailedAttempt = 0;
                SendConfirmationEmailPasswordChanged(user.Email);
                return RedirectToAction("index", "Home");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            return View("CommonError");
        }

        [NonAction]
        public void SendConfirmationEmailPasswordChanged(string emailID)
        {
            var fromEmail = new MailAddress("johnsaninfosystem@gmail.com", "Pharmacy System");
            var toEmail = new MailAddress(emailID);
            var fromEmailPassword = "dialog077";
            string subject = "Account Password Changed";

            string body = "<br/><h2>Your Paasword Changed</h2><br/>Your  account " +
                emailID.Substring(0, 3) + "*****" + emailID.Substring(emailID.IndexOf("@")) +
                 "<br/><br/>was just changed ";

            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(fromEmail.Address, fromEmailPassword)
            };

            using (var message = new MailMessage(fromEmail, toEmail)
            {
                Subject = subject,
                Body = body,
                IsBodyHtml = true
            })
                try
                {
                    smtp.Send(message);
                    mailSent = true;
                }
                catch (Exception)
                {
                    mailSent = false;
                }
        }
    }
}