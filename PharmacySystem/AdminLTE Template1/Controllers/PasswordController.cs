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
    public class PasswordController : Controller
    {
        private PharmacySystemEntities db = new PharmacySystemEntities();

        // GET: Password
        public ActionResult Index()
        {

            return View(db.Logins.ToList());
        }

        // GET: Password/Edit/5
        public ActionResult Edit(short? id=1)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Login login = db.Logins.Find(id);
            if (login == null)
            {
                return HttpNotFound();
            }
            return View(login);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,UserName,Password")] Login login)
        {
            if (ModelState.IsValid)
            {
                db.Entry(login).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(login);
        }    

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
