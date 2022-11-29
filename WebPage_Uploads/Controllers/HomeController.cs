using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using WebPage_Uploads.DataDB;
using static System.Net.WebRequestMethods;

namespace WebPage_Uploads.Controllers
{
    
    public class HomeController : Controller
    {
        private azblobstorageEntities db = new azblobstorageEntities();
        [HttpPost]
        public async Task<ActionResult> UploadForm(HttpPostedFileBase ufile)
        {
            var record = db.BlobRecords;
            if (ufile != null)
            {

                ViewBag.filename = ufile.FileName;
                try
                {
                    var blob = await AzureBlobClient.UploadBlob(ufile, ufile?.FileName);
                    if (blob.id == 0)
                        db.BlobRecords.Add(blob);
                    else
                    {
                        var rec = db.BlobRecords.Find(blob.id);
                        rec.blob_name = blob.blob_name;
                        db.Entry(rec).State = EntityState.Modified;
                    }
                        db.SaveChanges();
                    //if (ModelState.IsValid)
                    //{
                    //    record.Add(blob);
                    //    db.SaveChanges();
                    //    ViewBag.status = 201;
                    //}
                    return View("About");
                }
                catch
                {
                    ViewBag.status = 409;
                }
            }
            else
            {
                //ViewBag.Message = ex.Message;
                ViewBag.status = 400;
                Console.WriteLine("Error: Invalid File");
            }

            return View("Index");
        }

        public ActionResult Form1(HttpPostedFileBase ufile, string filename)
        {
            ViewBag.filename = filename;
            //ViewBag.filepath = ufile.ContentLength>0;
            return View("Index");
        }
        public ActionResult Index()
        {
            
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}