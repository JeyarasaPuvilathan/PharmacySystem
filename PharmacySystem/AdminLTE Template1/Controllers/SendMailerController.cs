using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AdminLTE_Template1.Models;
using System.Net.Mail;
using System.IO;
using System.Net;

namespace AdminLTE_Template1.Controllers
{
    public class SendMailerController : Controller
    {
        // GET: SendMailer
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(MailModel objModelMail, HttpPostedFileBase fileUploader)
        {
            if (ModelState.IsValid)
            {
                string from = "johnsaninfosystem@gmail.com";
                using (MailMessage mail = new MailMessage(from, objModelMail.To))
                {
                    mail.Subject = objModelMail.Subject;
                    mail.Body = objModelMail.Body;
                    if (fileUploader != null)
                    {
                        string fileName = Path.GetFileName(fileUploader.FileName);
                        mail.Attachments.Add(new Attachment(fileUploader.InputStream, fileName));
                    }
                    mail.IsBodyHtml = false;
                    SmtpClient smtp = new SmtpClient();
                    smtp.Host = "smtp.gmail.com";
                    smtp.EnableSsl = true;
                    NetworkCredential networkCredential = new NetworkCredential(from, "dialog077");
                    smtp.UseDefaultCredentials = true;
                    smtp.Credentials = networkCredential;
                    smtp.Port = 587;
                    try {
                        smtp.Send(mail);
                    }
                    catch (Exception)
                    {
                        ViewBag.EmailNetConMsg = "There is no Internet connection";
                        return View();
                    }
                    ViewBag.Alert =1;
                    ViewBag.Message = "Sent";
                    return View("Index", objModelMail);
                }
            }
            else
            {
                ViewBag.Alert = 2;
                ViewBag.Message = "Failed";
                return View();
            }
        }
    }
}
   