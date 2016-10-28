using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CRM.Models;

namespace CRM.Controllers
{
    public class SupplyController : Controller
    {
        private CRMEntities db = new CRMEntities();

        // GET: /Supply/
        public ActionResult Index()
        {
            return PartialView(db.Supply.ToList());
        }

        // GET: /Supply/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Supply supply = db.Supply.Find(id);
            if (supply == null)
            {
                return HttpNotFound();
            }

            return PartialView(supply);
        }

        // GET: /Supply/Create
        public ActionResult Create()
        {
            return PartialView();
        }

        // POST: /Supply/Create
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="SupplyName")] Supply supply)
        {
            if (ModelState.IsValid)
            {
                db.Supply.Add(supply);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return PartialView(supply);
        }

        // GET: /Supply/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Supply supply = db.Supply.Find(id);
            if (supply == null)
            {
                return HttpNotFound();
            }
            return PartialView(supply);
        }

        // POST: /Supply/Edit/5
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="SupplyID,SupplyName")] Supply supply)
        {
            if (ModelState.IsValid)
            {
                db.Entry(supply).State = EntityState.Modified;
                db.SaveChanges();
                
                return RedirectToAction("Index");
            }
            return PartialView(supply);
        }

        // GET: /Supply/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Supply supply = db.Supply.Find(id);
            if (supply == null)
            {
                return HttpNotFound();
            }
            return PartialView(supply);
        }

        // POST: /Supply/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Supply supply = db.Supply.Find(id);
            db.Supply.Remove(supply);
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
