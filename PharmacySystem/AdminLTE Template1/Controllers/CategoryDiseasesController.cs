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
    public class CategoryDiseasesController : Controller
    {
        private PharmacySystemEntities db = new PharmacySystemEntities();

        // GET: CategoryDiseases
        public ActionResult Index()
        {
            return View(db.CategoryDiseases.ToList());
        }

        // GET: CategoryDiseases/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CategoryDisease categoryDisease = db.CategoryDiseases.Find(id);
            if (categoryDisease == null)
            {
                return HttpNotFound();
            }
            return View(categoryDisease);
        }

        // GET: CategoryDiseases/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CategoryDiseases/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "C_ID,CategoryName")] CategoryDisease categoryDisease)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.CategoryDiseases.Add(categoryDisease);
                    db.SaveChanges();
                    return RedirectToAction("create", "Medicines");
                }

                return View(categoryDisease);
            }
            catch (Exception)
            {

                ViewData["Exception"] = "Cant Create";
                return View("CommonError");
            }
        }

        // GET: CategoryDiseases/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CategoryDisease categoryDisease = db.CategoryDiseases.Find(id);
            if (categoryDisease == null)
            {
                return HttpNotFound();
            }
            return View(categoryDisease);
        }

        // POST: CategoryDiseases/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "C_ID,CategoryName")] CategoryDisease categoryDisease)
        {
            if (ModelState.IsValid)
            {
                db.Entry(categoryDisease).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(categoryDisease);
        }

        // GET: CategoryDiseases/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CategoryDisease categoryDisease = db.CategoryDiseases.Find(id);
            if (categoryDisease == null)
            {
                return HttpNotFound();
            }
            return View(categoryDisease);
        }

        // POST: CategoryDiseases/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CategoryDisease categoryDisease = db.CategoryDiseases.Find(id);
            db.CategoryDiseases.Remove(categoryDisease);
            db.SaveChanges();
            return RedirectToAction("Index");
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
