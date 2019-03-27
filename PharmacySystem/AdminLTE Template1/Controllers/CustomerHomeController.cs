using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AdminLTE_Template1.Models;

namespace AdminLTE_Template1.Controllers
{
    public class CustomerHomeController : Controller
    {
        PharmacySystemEntities db = new PharmacySystemEntities();
        // GET: CustomerHome
        [NoCache]
        public ActionResult Index()
        {
            if (Session["Customer"] == null)
            {
                ViewBag.CustomerAuthenticate = "1";
                return View("~/Views/Customer/Login.cshtml");
            }
            else
                return View();
        }

        public ActionResult DiseaseType1(int? a)
        {
            var list = db.MedicineDetails.SqlQuery("select * from dbo.MedicineDetails where CategoryID="+a).ToList();
            return View(list);
        }

        public ActionResult DiseaseType2(int? a)
        {
            var list = db.MedicineDetails.SqlQuery("select * from dbo.MedicineDetails where CategoryID=" + a).ToList();
            return View(list);
        }
        public ActionResult DiseaseType3(int? a)
        {
            var list = db.MedicineDetails.SqlQuery("select * from dbo.MedicineDetails where CategoryID=" + a).ToList();
            return View(list);
        }

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
    }
}