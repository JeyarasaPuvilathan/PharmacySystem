using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AdminLTE_Template1.Models;
using PagedList;
using System.IO;

namespace AdminLTE_Template1.Controllers
{
    public class MedicinesController : Controller
    {
        private PharmacySystemEntities db = new PharmacySystemEntities();

        public ActionResult Index(string option, string search, int? pageNumber)
        {
            var totMedicineCount = db.MedicineDetails.Count();
            ViewData["TotalMedicines"] = totMedicineCount;
            //if a user choose the radio button option as Subject  
            if (option == "MedicineName")
            {
                //if (String.IsNullOrEmpty(search))
                //{
                //    ViewBag. EmptySearchID = 1;
                //    ViewData["ErrorMessage"] = "pls enter the word to search";
                //    return View(db.MedicineDetails.Where(x => x.MedicineName.Contains(search) || search == null).ToList().ToPagedList(pageNumber ?? 1, 9));
                // }
                return View(db.MedicineDetails.Where(x => x.MedicineName.Contains(search) || search == null).ToList().ToPagedList(pageNumber ?? 1, 9));
            }

            else
            {

                //if (String.IsNullOrEmpty(search))
                //{
                //    ViewBag.EmptySearchID = 1;
                //    ViewData["ErrorMessage"] = "pls enter the word to search";
                //    return View();
                //}
                return View(db.MedicineDetails.Where(x => x.CategoryDisease.CategoryName.Contains(search) || search == null).ToList().ToPagedList(pageNumber ?? 1, 9));
            }
        }

        // GET: Medicines/Details/5
        public ActionResult Details(short? id)
        {
            if (id == null)
            {
                ViewData["Exception"] = "Datas are Not Exists";
                return View("CommonError");
            }
            MedicineDetail medicineDetail = db.MedicineDetails.Find(id);
            if (medicineDetail == null)
            {
                ViewData["Exception"] = "Datas are Not Exist";
                return View("CommonError");
            }
            return View(medicineDetail);
        }

        // GET: Medicines/Create
        public ActionResult Create()
        {
            ViewBag.CategoryID = new SelectList(db.CategoryDiseases, "C_ID", "CategoryName");
            return View();
        }

        // POST: Medicines/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(MedicineDetail medicineDetail, HttpPostedFileBase file)
        {
            try
            {
                //if (ModelState.IsValid)
                //{
                if (file != null)
                {
                    string pic = System.IO.Path.GetFileName(file.FileName);
                    string savePath = Server.MapPath("~/Content/img/");
                    //string path = System.IO.Path.Combine(Server.MapPath( pic));
                    // file is uploaded
                    MedicineDetail s = new MedicineDetail();

                    s.ManufacturedDate = medicineDetail.ManufacturedDate;
                    s.ExpiredDate = medicineDetail.ExpiredDate;
                    s.Dosage = medicineDetail.Dosage;
                    s.MedicineName = medicineDetail.MedicineName;
                    s.CategoryID = medicineDetail.CategoryID;
                    s.Price = medicineDetail.Price;
                    s.Description = medicineDetail.Description;
                    s.Quantity = medicineDetail.Quantity;
                    s.AddMonth = DateTime.Now.ToString("MMMM");

                    s.Image = pic;
                    file.SaveAs(Path.Combine(savePath, pic));
                    //file.SaveAs(pic);
                    db.MedicineDetails.Add(s);
                    try
                    {
                        db.SaveChanges();
                    }
                    catch (Exception)
                    {
                        return View();
                    }


                    return RedirectToAction("Index");
                }
                // }

                ViewBag.CategoryID = new SelectList(db.CategoryDiseases, "C_ID", "CategoryName", medicineDetail.CategoryID);
                return View(medicineDetail);
            }
            catch (Exception)
            {

                ViewData["Exception"] = "Cant Create";
                //return View("CommonError");
            }
            ViewBag.CategoryID = new SelectList(db.CategoryDiseases, "C_ID", "CategoryName", medicineDetail.CategoryID);
            return View(medicineDetail);
        }

        // GET: Medicines/Edit/5
        public ActionResult Edit(short? id)
        {
            if (id == null)
            {
                ViewData["Exception"] = "Datas are Not Exists";
                return View("CommonError");
            }
            MedicineDetail medicineDetail = db.MedicineDetails.Find(id);
            if (medicineDetail == null)
            {
                ViewData["Exception"] = "Datas are Not Exists";
                return View("CommonError");
            }
            ViewBag.CategoryID = new SelectList(db.CategoryDiseases, "C_ID", "CategoryName", medicineDetail.CategoryID);
            return View(medicineDetail);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "M_ID,ManufacturedDate,ExpiredDate,Dosage,Image,MedicineName,CategoryID,Price,Description,Quantity")] MedicineDetail medicineDetail)
        {
            if (medicineDetail == null)
            {
                ViewData["Exception"] = "Datas are Not Exists";
                return View("CommonError");
            }

            if (ModelState.IsValid)
            {
                db.Entry(medicineDetail).State = EntityState.Modified;
                try
                {
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch (Exception)
                {
                    ViewData["Exception"] = "Cant Update";
                    //return View("CommonError");
                }

            }
            ViewBag.CategoryID = new SelectList(db.CategoryDiseases, "C_ID", "CategoryName", medicineDetail.CategoryID);
            return View(medicineDetail);
        }
        // GET: Medicines/Delete/5
        public ActionResult Delete(short? id)
        {
            if (id == null)
            {
                ViewData["Exception"] = "Datas are Not Exists";
                return View("CommonError");
            }
            MedicineDetail medicineDetail = db.MedicineDetails.Find(id);
            if (medicineDetail == null)
            {
                ViewData["Exception"] = "Datas are Not Exists";
                //return View("CommonError");
            }
            return View(medicineDetail);
        }

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit(MedicineDetail medicineDetail)
        //{
        //    if (medicineDetail == null)
        //    {
        //        ViewData["Exception"] = "Datas are Not Exists";
        //        return View("CommonError");
        //    }

        //    MedicineDetail s = new MedicineDetail();
        //    s.Image = medicineDetail.Image;
        //    s.ManufacturedDate = medicineDetail.ManufacturedDate;
        //    s.ExpiredDate = medicineDetail.ExpiredDate;
        //    s.Dosage = medicineDetail.Dosage;
        //    s.MedicineName = medicineDetail.MedicineName;
        //    s.Description = medicineDetail.Description;
        //    s.Price = medicineDetail.Price;
        //    s.Quantity = medicineDetail.Quantity;
        //    s.CategoryID = medicineDetail.CategoryID;
        //    db.MedicineDetails.Add(s);
        //    try
        //    {
        //        db.SaveChanges();
        //    }
        //    catch (Exception)
        //    {
        //        ViewData["Exception"] = "Cant Update";
        //        return View("CommonError");
        //    }

        //    // after successfully uploading redirect the user
        //    return RedirectToAction("Index", "Home");
        //}

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(short id)
        {
            MedicineDetail medicineDetail = db.MedicineDetails.Find(id);
            try
            {

                db.MedicineDetails.Remove(medicineDetail);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                ViewData["Exception"] = "Cant deleted";
                return View("CommonError");

            }
        }

        public ActionResult DateExpaired()
        {
            var list = db.MedicineDetails.SqlQuery("select * from dbo.MedicineDetails where GETDATE() > ExpiredDate").ToList();
            return View(list);
        }

        public ActionResult MedicineSort()
        {
            var list = db.MedicineDetails.SqlQuery("select * from dbo.MedicineDetails where GETDATE() < ExpiredDate order by ExpiredDate ASC").ToList();
            return View(list);
        }


        public ActionResult DateToBeExpaired()
        {
            var list = db.MedicineDetails.SqlQuery(" SELECT* FROM dbo.MedicineDetails WHERE ExpiredDate BETWEEN GETDATE()  AND DATEADD(day,30,GETDATE())").ToList();
            return View(list);
        }



        [HttpPost]
        public JsonResult DeleteInPopup(int m_ID)
        {
            MedicineDetail medicine = db.MedicineDetails.SingleOrDefault(x => x.IsDeleted == false && x.M_ID == m_ID);
            bool result = false;
            if (medicine != null)
            {
                medicine.IsDeleted = true;
                db.SaveChanges();
                result = true;
            }
            return Json(result, JsonRequestBehavior.AllowGet);
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
