using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AdminLTE_Template1.Models;
using System.IO;

namespace AdminLTE_Template1.Controllers
{
    public class ImageController : Controller
    {
        private PharmacySystemEntities db = new PharmacySystemEntities();

        // GET: Image
        public ActionResult Index()
        {
            var medicineDetails = db.MedicineDetails.Include(m => m.CategoryDisease);
            return View(medicineDetails.ToList());
        }

        // GET: Image/Details/5
        public ActionResult Details(short? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MedicineDetail medicineDetail = db.MedicineDetails.Find(id);
            if (medicineDetail == null)
            {
                return HttpNotFound();
            }
            return View(medicineDetail);
        }

        // GET: Image/Create
        public ActionResult Create()
        {
            ViewBag.CategoryID = new SelectList(db.CategoryDiseases, "C_ID", "CategoryName");
            return View();
        }

        // POST: Image/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create( MedicineDetail medicineDetail,HttpPostedFileBase file)
        {
            
            if (file != null)
            {
                string pic = System.IO.Path.GetFileName(file.FileName);
                string path = System.IO.Path.Combine(
                                       Server.MapPath("~/Content/img/"), pic);
                file.SaveAs(path);
                // file is uploaded
                MedicineDetail newRecord = new MedicineDetail();
                newRecord.Dosage = medicineDetail.Dosage;
                newRecord.ExpiredDate = medicineDetail.ExpiredDate;
                newRecord.ManufacturedDate = medicineDetail.ManufacturedDate;
                newRecord.Image = path;
                newRecord.MedicineName = medicineDetail.MedicineName;
                newRecord.CategoryID = medicineDetail.CategoryID;
                db.MedicineDetails.Add(newRecord);
                db.SaveChanges();
                

                // save the image path path to the database or you can send image 
                // directly to database
                // in-case if you want to store byte[] ie. for DB
                using (MemoryStream ms = new MemoryStream())
                {

                    file.InputStream.CopyTo(ms);
                    byte[] array = ms.GetBuffer();
                }

            }
            // after successfully uploading redirect the user
            return RedirectToAction("Index", "Home");
        }
        //public ActionResult FileUpload(HttpPostedFileBase file)
        //{
        //    UniversityDatasEntities db = new UniversityDatasEntities();
        //    if (file != null)
        //    {
        //        string pic = System.IO.Path.GetFileName(file.FileName);
        //        string path = System.IO.Path.Combine(
        //                               Server.MapPath("~/Content/images/"), pic);
        //        // file is uploaded
        //        sample s = new sample();
        //        s.path = path;
        //        db.samples.Add(s);
        //        db.SaveChanges();
        //        file.SaveAs(path);

        //        // save the image path path to the database or you can send image 
        //        // directly to database
        //        // in-case if you want to store byte[] ie. for DB
        //        using (MemoryStream ms = new MemoryStream())
        //        {

        //            file.InputStream.CopyTo(ms);
        //            byte[] array = ms.GetBuffer();
        //        }

        //    }
        //    // after successfully uploading redirect the user
        //    return RedirectToAction("Index", "Home");
        //}
        // GET: Image/Edit/5
        public ActionResult Edit(short? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MedicineDetail medicineDetail = db.MedicineDetails.Find(id);
            if (medicineDetail == null)
            {
                return HttpNotFound();
            }
            ViewBag.CategoryID = new SelectList(db.CategoryDiseases, "C_ID", "CategoryName", medicineDetail.CategoryID);
            return View(medicineDetail);
        }

        // POST: Image/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "M_ID,Location,ManufacturedDate,ExpiredDate,Dosage,Image,MedicineName,CategoryID")] MedicineDetail medicineDetail)
        {
            if (ModelState.IsValid)
            {
                db.Entry(medicineDetail).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CategoryID = new SelectList(db.CategoryDiseases, "C_ID", "CategoryName", medicineDetail.CategoryID);
            return View(medicineDetail);
        }

        // GET: Image/Delete/5
        public ActionResult Delete(short? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MedicineDetail medicineDetail = db.MedicineDetails.Find(id);
            if (medicineDetail == null)
            {
                return HttpNotFound();
            }
            return View(medicineDetail);
        }

        // POST: Image/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(short id)
        {
            MedicineDetail medicineDetail = db.MedicineDetails.Find(id);
            db.MedicineDetails.Remove(medicineDetail);
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
