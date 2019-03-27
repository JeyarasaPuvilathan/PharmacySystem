using AdminLTE_Template1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AdminLTE_Template1.Controllers
{
    public class NotificationController : Controller
    {
        PharmacySystemEntities db = new PharmacySystemEntities();
        // GET: Notification
        public ActionResult Index()
        {
            var list = db.MedicineDetails.SqlQuery("select * from dbo.MedicineDetails where CategoryID=1").ToList();
            return View(list);
            //return View();
        }

        public JsonResult GetNotificationContacts()
        {
            //var notificationRegisterTime = Session["LastUpdated"] != null ? Convert.ToDateTime(Session["LastUpdated"]) : DateTime.Now;
            NotificationComponent NC = new NotificationComponent();
            var list = NC.GetContacts(2);
            //update session here for get only new added contacts (notification)
            //Session["LastUpdate"] = DateTime.Now;
            return new JsonResult { Data = list, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
    }
}