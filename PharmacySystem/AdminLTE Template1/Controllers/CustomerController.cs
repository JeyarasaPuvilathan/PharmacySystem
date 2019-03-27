using AdminLTE_Template1.Models;
using System;
using System.Collections.Generic;
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
    public class CustomerController : Controller
    {
        public static bool mailSent = false;
        // GET: Customer
        [HttpGet]
        public ActionResult Registration()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Registration([Bind(Exclude = "IsEmailVerified,ActivationCode")] CustomerLogin customer)
        {
            bool Status = false;
            string message = "";
            //
            // Model Validation 
            if (customer.Email==null)
            {
                ViewBag.Emty = "Email is required";
                return View(customer);

            }

                #region //Email is already Exist 
                var isExist = IsEmailExist(customer.Email);
                if (isExist)
                {              
                ViewBag.Emty = "Email already exist";
                //ModelState.AddModelError("EmailExist", "Email already exist");
                return View(customer);
                }
                #endregion

                #region Generate Activation Code 
                customer.ActivationCode = Guid.NewGuid();
            #endregion

            #region  Password Hashing 
            if (customer.Password==null)
            {
                ViewBag.Password = "Password is required";
                return View();
            }

            string pattern = @"^(?=.*[A-Z])(?=.*[a-z])(?=.*[0-9]).{6,20}$";
            Match result = Regex.Match(customer.Password, pattern);

            if (!result.Success)
            {
                ViewBag.Password = "Passwords must use at least three of the four available character types: lowercase letters, uppercase letters & numbers and 6 digits.";
                return View();
            }


            string pattern1 = @"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$";
            Match result1 = Regex.Match(customer.Email, pattern1);

            if (!result1.Success)
            {
                ViewBag.Emty = "Invalid Email Address";
                return View();
            }


            customer.Password = Crypto.Hash(customer.Password);
                //customer.ConfirmPassword = Crypto.Hash(customer.ConfirmPassword); //
                #endregion
                customer.IsEmailVerified = false;

                #region Save to Database
                
                //Send Email to User
                SendVerificationLinkEmail(customer.Email, customer.ActivationCode.ToString());
                    message = "Registration successfully done. Account activation link " +
                        " has been sent to your email id:" + customer.Email;
                    Status = true;
                if (mailSent == false)
                {
                    ViewBag.noSentEmail = "True";
                    ViewBag.EmailNetConMsg = "There is no Internet connection";
                    return View();

                }

            using (PharmacySystemEntities dc = new PharmacySystemEntities())
            {
                if (customer.Email.ToLower().Contains('@'))
                {
                    try
                    {

                        dc.CustomerLogins.Add(customer);
                        dc.SaveChanges();
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
                    ViewBag.EmailValid = "Invalid Email Address";
                    return View();
                }



            }
                #endregion
           

            ViewBag.Message = message;
            ViewBag.Status = Status;
            return View(customer);
        }

        [NonAction]
        public bool IsEmailExist(string email)
        {
            using (PharmacySystemEntities dc = new PharmacySystemEntities())
            {
                var v = dc.CustomerLogins.Where(a => a.Email == email).FirstOrDefault();
                return v != null;
            }
        }

        [NonAction]
        public void SendVerificationLinkEmail(string emailID, string activationCode)
        {
            var verifyUrl = "/Customer/VerifyAccount/" + activationCode;
            var link = Request.Url.AbsoluteUri.Replace(Request.Url.PathAndQuery, verifyUrl);

            var fromEmail = new MailAddress("johnsaninfosystem@gmail.com", "Pharmacy System");
            var toEmail = new MailAddress(emailID);
            var fromEmailPassword = "dialog077";
            string subject = "Your account is successfully created!";

            string body = "<br/><br/>Your account is" +
                " successfully created. Please click on the below link to verify your account" +
                " <br/><br/><a href='" + link + "'>" + link + "</a> ";

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
                try {
                    smtp.Send(message);
                    mailSent = true;
                }
                catch (Exception)
                {
                    mailSent = false;
                }
        }

        [HttpGet]
        public ActionResult VerifyAccount(string id)
        {
            bool Status = false;
            using (PharmacySystemEntities dc = new PharmacySystemEntities())
            {
                dc.Configuration.ValidateOnSaveEnabled = false; // This line I have added here to avoid 
                                                                // Confirm password does not match issue on save changes
                var v = dc.CustomerLogins.Where(a => a.ActivationCode == new Guid(id)).FirstOrDefault();
                if (v != null)
                {
                    v.IsEmailVerified = true;
                    dc.SaveChanges();
                    Status = true;
                }
                else
                {
                    ViewBag.Message = "Invalid Request";
                }
            }
            ViewBag.Status = Status;
            return View();
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginCustomer login, string ReturnUrl = "")
        {
            string message = "";
            using (PharmacySystemEntities dc = new PharmacySystemEntities())
            {
                
                var v = dc.CustomerLogins.Where(a => a.Email == login.Email).FirstOrDefault();
                if (v != null)
                {
                    if (v.IsEmailVerified == false)
                    {
                        ViewBag.n = 1;
                        message = "Incorrect EmailID or Password";
                        ViewBag.Message = message;
                        return View("Login");
                    }
                    if (string.Compare(Crypto.Hash(login.Password), v.Password) == 0)
                    {
                        int timeout = login.RememberMe ? 525600 : 20; // 525600 min = 1 year
                        var ticket = new FormsAuthenticationTicket(login.Email, login.RememberMe, timeout);
                        string encrypted = FormsAuthentication.Encrypt(ticket);
                        var cookie = new HttpCookie(FormsAuthentication.FormsCookieName, encrypted);
                        cookie.Expires = DateTime.Now.AddMinutes(timeout);
                        cookie.HttpOnly = true;
                        Response.Cookies.Add(cookie);


                        if (Url.IsLocalUrl(ReturnUrl))
                        {
                            return Redirect(ReturnUrl);
                        }
                        else
                        {
                            Session["Customer"] = v.Email;
                            return RedirectToAction("Index", "CustomerHome");
                        }
                    }
                    else
                    {
                        ViewBag.n = 1;
                        message = "Incorrect EmailID or Password";
                    }
                }
                else
                {
                    ViewBag.n = 1;
                    message = "Incorrect EmailID or Password";
                }
            }
            ViewBag.Message = message;
            return View();
        }

        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            Session.Abandon(); // it will clear the session at the end of request
            return RedirectToAction("Login", "Customer");
        }
    }
}