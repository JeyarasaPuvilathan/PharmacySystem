using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AdminLTE_Template1.Models;

namespace AdminLTE_Template1.Controllers
{
    public class MapsController : Controller
    {
        private PharmacySystemEntities db = new PharmacySystemEntities();

        // GET: Maps    
        public ActionResult Index()
        {
            return View(db.Maps.ToList());
        }

        #region [Map]
        [HttpPost]
        public JsonResult GetMap()
        {
            var data1 = Map();
            return Json(data1, JsonRequestBehavior.AllowGet);
        }
        public IEnumerable<Map> Map()
        {

            return (from p in db.Maps
                    select new
                    {
                        Name = p.Name,
                        Latitude = p.Latitude,
                        Longtitude = p.Longtitude,
                        Location = p.Location,
                        Description = p.Description,
                        ID = p.ID
                    }).ToList()
                .Select(res => new Map
                {
                    Name = res.Name,
                    Latitude = res.Latitude,
                    Longtitude = res.Longtitude,
                    Location = res.Location,
                    Description = res.Description,
                    ID = res.ID


                });

        }
        #endregion    
    }
}
