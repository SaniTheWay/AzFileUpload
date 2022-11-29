using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebPage_Uploads.DataDB;

namespace WebPage_Uploads.Controllers
{
    public class BlobRecordsController : Controller
    {
        private azblobstorageEntities db = new azblobstorageEntities();
        
        // GET: BlobRecords
        public ActionResult Index()
        {
            return View(db.BlobRecords.ToList());
        }

        // GET: BlobRecords/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BlobRecord blobRecord = db.BlobRecords.Find(id);
            if (blobRecord == null)
            {
                return HttpNotFound();
            }
            return View(blobRecord);
        }

        // GET: BlobRecords/Create
        public ActionResult Create()
        {
            return RedirectToAction("UploadForm","HomeController");
        }

        // POST: BlobRecords/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id, blob_name,date,url")] BlobRecord blobRecord)
        {
            if (ModelState.IsValid)
            {
                db.BlobRecords.Add(blobRecord);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(blobRecord);
        }

        // GET: BlobRecords/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BlobRecord blobRecord = db.BlobRecords.Find(id);
            if (blobRecord == null)
            {
                return HttpNotFound();
            }
            return View(blobRecord);
        }

        // POST: BlobRecords/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,blob_name,date,url")] BlobRecord blobRecord)
        {
            if (ModelState.IsValid)
            {
                db.Entry(blobRecord).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(blobRecord);
        }

        // GET: BlobRecords/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BlobRecord blobRecord = db.BlobRecords.Find(id);
            if (blobRecord == null)
            {
                return HttpNotFound();
            }
            return View(blobRecord);
        }

        // POST: BlobRecords/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            BlobRecord blobRecord = db.BlobRecords.Find(id);
            db.BlobRecords.Remove(blobRecord);
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
