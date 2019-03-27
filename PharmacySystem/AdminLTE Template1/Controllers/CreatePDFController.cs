using AdminLTE_Template1.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace AdminLTE_Template1.Controllers
{
    public class CreatePDFController : Controller
    {
        PharmacySystemEntities db = new PharmacySystemEntities();
        // GET: CreatePDF
        public ActionResult Index(string variable)
        {
            List<MedicineDetail> medicines = new List<MedicineDetail>();
            medicines = db.MedicineDetails.ToList();
        //    var studentMarks = new List<MarksCard>()
        //{
        //   new MarksCard(){RollNo = 101, Subject = "C#",FullMarks = 100, Obtained = 90},
        //   new MarksCard() {RollNo = 101, Subject = "asp.net", FullMarks = 100, Obtained = 80},
        //   new MarksCard() {RollNo =101, Subject = "MVC", FullMarks = 100,Obtained = 100},
        //   new MarksCard() {RollNo = 101, Subject = "SQL Server", FullMarks = 100, Obtained = 75},
        //};
            return new RazorPDF.PdfResult(medicines, "Index");
        }

        public ActionResult CharterColumn()
        {
            var _context = new PharmacySystemEntities();

            ArrayList xValue = new ArrayList();
            ArrayList yValue = new ArrayList();

            var results = (from c in _context.MedicineDetails select c);

            results.ToList().ForEach(rs => xValue.Add(rs.MedicineName));
            results.ToList().ForEach(rs => yValue.Add(rs.Quantity));

            new Chart(width: 600, height: 400, theme: ChartTheme.Yellow)
            .AddTitle("Medicines Details [Column Chart]")
           .AddSeries("Default", chartType: "column", xValue: xValue, yValues: yValue)
                  .Write("bmp");

            return null;
        }



    }
}