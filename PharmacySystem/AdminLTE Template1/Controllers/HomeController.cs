using AdminLTE_Template1.Models;
using System.Web.Mvc;
using System.Web.Security;
using System.Collections.Generic;
using System.Linq;
using System;
using System.Web;
using NLog;

namespace AdminLTE_Template1.Controllers
{
    public class HomeController : Controller
    {
        private PharmacySystemEntities db = new PharmacySystemEntities();
       
        public class NoCache : ActionFilterAttribute
        {
            public override void OnResultExecuting(ResultExecutingContext filterContext)
            {
                filterContext.HttpContext.Response.Cache.SetExpires(DateTime.UtcNow.AddDays(-1));
                filterContext.HttpContext.Response.Cache.SetValidUntilExpires(false);
                filterContext.HttpContext.Response.Cache.SetRevalidation(HttpCacheRevalidation.AllCaches);
                filterContext.HttpContext.Response.Cache.SetCacheability(HttpCacheability.NoCache);
                filterContext.HttpContext.Response.Cache.SetNoStore();

                base.OnResultExecuting(filterContext);
            }
        }
        [NoCache]
        public ActionResult Index()
        {
            if (Session["User"] == null)
            {
                ViewBag.UserAuthenticate = "1";
                return View("~/Views/Login/Index.cshtml");
            }
            else
            {               
                return View();
            }
        }


        public ActionResult FileUpload(HttpPostedFileBase file)
        {

            if (file != null)
            {

                string ImageName = System.IO.Path.GetFileName(file.FileName);
                string physicalPath = Server.MapPath("~/Content/img/" + ImageName);

                // save image in folder
                file.SaveAs(physicalPath);

                //save new record in database
                MedicineDetail newRecord = new MedicineDetail();
                newRecord.Dosage = Request.Form["Dosage"];
                newRecord.ExpiredDate = DateTime.Parse(Request.Form["ExpiredDate"]);
                newRecord.ManufacturedDate = DateTime.Parse(Request.Form["ManufacturedDate"]);
                newRecord.Image = physicalPath;
                newRecord.MedicineName = Request.Form["MedicineName"];
                newRecord.CategoryID = int.Parse(Request.Form["ID"]);

                db.MedicineDetails.Add(newRecord);
                try
                {
                    db.SaveChanges();
                }
                catch (System.Data.Entity.Validation.DbEntityValidationException dbEx)
                {
                    Exception raise = dbEx;
                    foreach (var validationErrors in dbEx.EntityValidationErrors)
                    {
                        foreach (var validationError in validationErrors.ValidationErrors)
                        {
                            string message = string.Format("{0}:{1}",
                                validationErrors.Entry.Entity.ToString(),
                                validationError.ErrorMessage);
                            // raise a new exception nesting  
                            // the current instance as InnerException  
                            raise = new InvalidOperationException(message, raise);
                        }
                    }
                    throw raise;
                }

            }
            //Display records
            return RedirectToAction("Index", "Medicines");
        }

        public ActionResult AnotherLink()
        {
            return View("Index");
        }

        public ActionResult Chat()
        {
            return View();
        }

        //Calender

        public ActionResult Index1()
        {
            return View();
        }

        public JsonResult GetEvents()
        {
            //Here MyDatabaseEntities is our entity datacontext (see Step 4)
            using (PharmacySystemEntities dc = new PharmacySystemEntities())
            {
                var v = dc.Demo_Events.OrderBy(a => a.StartAt).ToList();
                return new JsonResult { Data = v, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
        }

        [HttpPost]
        public JsonResult SaveEvent(Demo_Events evt)
        {
            bool status = false;
            using (PharmacySystemEntities dc = new PharmacySystemEntities())
            {
                if (evt.EndAt != null && evt.StartAt.TimeOfDay == new TimeSpan(0, 0, 0) &&
                    evt.EndAt.Value.TimeOfDay == new TimeSpan(0, 0, 0))
                {
                    evt.IsFullDay = true;
                }
                else
                {
                    evt.IsFullDay = false;
                }
                if (evt.EventID > 0)
                {
                    var v = dc.Demo_Events.Where(a => a.EventID.Equals(evt.EventID)).FirstOrDefault();
                    if (v != null)
                    {
                        v.Title = evt.Title;
                        v.Description = evt.Description;
                        v.StartAt = evt.StartAt;
                        v.EndAt = evt.EndAt;
                        v.IsFullDay = evt.IsFullDay;
                    }
                }
                else
                {
                    dc.Demo_Events.Add(evt);
                }
                dc.SaveChanges();
                status = true;
            }
            return new JsonResult { Data = new { status = status } };
        }

        [HttpPost]
        public JsonResult DeleteEvent(int eventID)
        {
            bool status = false;
            using (PharmacySystemEntities dc = new PharmacySystemEntities())
            {
                var v = dc.Demo_Events.Where(a => a.EventID.Equals(eventID)).FirstOrDefault();
                if (v != null)
                {
                    dc.Demo_Events.Remove(v);
                    dc.SaveChanges();
                    status = true;
                }
            }
            return new JsonResult { Data = new { status = status } };
        }
    }
}