using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AdminLTE_Template1.Models;
using System.Data.Entity.Validation;

namespace AdminLTE_Template1.Controllers
{
    public class AdminViewController : Controller
    {
        private PharmacySystemEntities db = new PharmacySystemEntities();

        // GET: AdminView/Details/5
        public ActionResult Details(int? id = 1)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AdminLogin adminLogin = db.AdminLogins.Find(id);
            if (adminLogin == null)
            {
                return HttpNotFound();
            }
            return View(adminLogin);
        }

        // GET: AdminView/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AdminView/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,UserName,Password,DateOfBirth,MobileNo,Email,Image,CreatedOn")] AdminLogin adminLogin)
        {
            if (ModelState.IsValid)
            {
                db.AdminLogins.Add(adminLogin);
                db.SaveChanges();
                return RedirectToAction("detail");
            }

            return View(adminLogin);
        }

        // GET: AdminView/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AdminLogin adminLogin = db.AdminLogins.Find(id);
            if (adminLogin == null)
            {
                return HttpNotFound();
            }
            return View(adminLogin);
        }

        // POST: AdminView/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,UserName,DateOfBirth,MobileNo,Email,Image,CreatedOn")] AdminLogin adminLogin)
        {
            var v = db.AdminLogins.Where(a => a.ID == adminLogin.ID).FirstOrDefault();
            adminLogin.Password = v.Password;
            adminLogin.Image = v.Image  ;
            v.Password = adminLogin.Password ;
            v.UserName = adminLogin.UserName;
            v.DateOfBirth = adminLogin.DateOfBirth;
            v.MobileNo = adminLogin.MobileNo;
            v.Email = adminLogin.Email;
            v.Image = adminLogin.Image;
            v.CreatedOn = adminLogin.CreatedOn;

            try
            {
                if (ModelState.IsValid)
                {
                    //db.Entry(adminLogin).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Details");
                }
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

            }
            return View(adminLogin);
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
