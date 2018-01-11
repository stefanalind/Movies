using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Movies.Models;

namespace Movies.Controllers
{
    public class CustomersController : Controller
    {
        private MoviesContext db = new MoviesContext();


        // GET: Customers
        //============================================================================================================
        // Action för Cutomer-lista.
        // Parameter Sortorder:
        // --------------------
        //  lastname_desc: sortera fallande per efternamn sen förnamn.
        //  firstname: sortera stigande per förnamn sen efternamn.
        //  firstname_desc: sortera fallande per förnamn sen efternamn.
        //  tom parameter (default): sortera stigande per efternamn sen förnamn.
        //============================================================================================================

        public ActionResult Index(string sortOrder)
        {
            ViewBag.LastNameSort = String.IsNullOrEmpty(sortOrder) ? "lastname_desc" : "";
            ViewBag.FirstNameSort = String.IsNullOrEmpty(sortOrder) ? "firstname_desc" : "";

            var customers = from c in db.Customers select c;

            switch (sortOrder)
            {
                case "lastname_desc":
                    customers = customers.OrderByDescending(c => c.lastName).ThenByDescending(c => c.firstName);
                    break;
                case "firstname":
                    customers = customers.OrderBy(c => c.firstName).ThenByDescending(c => c.lastName);
                    break;
                case "firstname_desc":
                    customers = customers.OrderByDescending(c => c.firstName).ThenByDescending(c => c.lastName);
                    break;
                default:
                    customers = customers.OrderBy(c => c.lastName).ThenBy(c => c.firstName);
                    break;
            }

            return View(customers.ToList());


        }



        // GET: Customers/Details/5
        //============================================================================================================
        // Customer-details.
        // Parameter:
        // ----------
        //  id: CustomerId (nyckel för kund).
        //============================================================================================================

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }



        // GET: Customers/Create
        //============================================================================================================
        // Customer-Create: GET och POST.
        //============================================================================================================

        public ActionResult Create()
        {
            return View();
        }


        // POST: Customers/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "customerId,firstName,lastName")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                db.Customers.Add(customer);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(customer);
        }



        // GET: Customers/Edit/5
        //============================================================================================================
        // Customer-Edit: GET och POST.
        // Parameter:
        // ----------
        //  id: CustomerId (nyckel för kund).
        //============================================================================================================
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }


        // POST: Customers/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "customerId,firstName,lastName")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                db.Entry(customer).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(customer);
        }




        // GET: Customers/Delete/5
        //============================================================================================================
        // Customer-Delete: GET och POST.
        // Parameter:
        // ----------
        //  id: CustomerId (nyckel för kund).
        //============================================================================================================
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        // POST: Customers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Customer customer = db.Customers.Find(id);
            db.Customers.Remove(customer);
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
