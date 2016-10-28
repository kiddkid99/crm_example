using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CRM.Models;
using CRM.ViewModels;
using Microsoft.AspNet.Identity.EntityFramework;

namespace CRM.Controllers
{
    public class CustomerController : Controller
    {
        private CRMEntities db = new CRMEntities();

        // GET: /Customer/
        public ActionResult Index()
        {
            return View(db.Customers.ToList());
        }

        // GET: /Customer/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customers customers = db.Customers.Find(id);
            if (customers == null)
            {
                return HttpNotFound();
            }
            return View(customers);
        }

        // GET: /Customer/Create
        public ActionResult Create()
        {
            var customer = new Customers();
            PopuplateCustomerSupply(customer);
            return View();
        }

        private void PopuplateCustomerSupply(Customers customer)
        {
            var allSupply = db.Supply;
            var customerSupply = new HashSet<int>(customer.Supply.Select(s => s.SupplyID));
            var viewModel = new List<UsedSupplyData>();

            foreach (var model in allSupply)
            {
                viewModel.Add(new UsedSupplyData()
                {
                    SupplyID = model.SupplyID,
                    SupplyName = model.SupplyName,
                    Used = customerSupply.Contains(model.SupplyID)
                });
            }
            ViewBag.Supplier = viewModel;
        }

        // POST: /Customer/Create
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CompanyName,RegistrationNumber,ManagerName,Phone,Email,Fax,Address,AccountName,AccountNumber,Bank,BankCode,Branch,BranchCode")] Customers customers, String[] selectedSupplier)
        {
            if (selectedSupplier != null)
            {
                foreach (var supplyID in selectedSupplier)
                {
                    var supplyToAdd = db.Supply.Find(int.Parse(supplyID));
                    customers.Supply.Add(supplyToAdd);
                }
            }

            if (ModelState.IsValid)
            {
                db.Customers.Add(customers);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            PopuplateCustomerSupply(customers);
            return View(customers);
        }

        // GET: /Customer/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Customers customers = db.Customers.Find(id);
            if (customers == null)
            {
                return HttpNotFound();
            }

            PopuplateCustomerSupply(customers);
            return View(customers);
        }

        // POST: /Customer/Edit/5
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int? id, FormCollection form, String[] selectedSupplier)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var customer = db.Customers
                .Include(c => c.Supply)
                .Where(c => c.CustomerID == id)
                .SingleOrDefault();

            if (TryUpdateModel(customer, "", form.AllKeys, new string[] { "CustomerID" }) && ModelState.IsValid)
            {
                customer.Supply.Clear();
                
                if (selectedSupplier != null)
                {
                    foreach (var supplyID in selectedSupplier)
                    {
                        var supplyToAdd = db.Supply.Find(int.Parse(supplyID));
                        customer.Supply.Add(supplyToAdd);
                    }
                }

                db.Entry(customer).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            PopuplateCustomerSupply(customer);
            return View(customer);
        }

        // GET: /Customer/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customers customers = db.Customers.Find(id);
            if (customers == null)
            {
                return HttpNotFound();
            }
            return View(customers);
        }

        // POST: /Customer/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Customers customers = db.Customers.Find(id);
            db.Customers.Remove(customers);
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
