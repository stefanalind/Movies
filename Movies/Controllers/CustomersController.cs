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


        //============================================================================================================
        // Action för Customer-lista (administration av kunder).
        //
        // Parameter Sortorder:
        // --------------------
        //  lastname_desc: sortera fallande per efternamn sen förnamn.
        //  firstname: sortera stigande per förnamn sen efternamn.
        //  firstname_desc: sortera fallande per förnamn sen efternamn.
        //  tom parameter (default): sortera stigande per efternamn sen förnamn.
        //============================================================================================================

        // GET: Customers
        public ActionResult Index(string sortOrder)
        {
            ViewBag.LastNameSort = String.IsNullOrEmpty(sortOrder) ? "lastname_desc" : "";
            ViewBag.FirstNameSort = String.IsNullOrEmpty(sortOrder) ? "firstname_desc" : "";

            using (MoviesContext db = new MoviesContext())
            {
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

        }



        //============================================================================================================
        // Customer-details (detaljer om specifik kund).
        //
        // Parameter:
        // ----------
        //  id: CustomerId (nyckel för kund).
        //============================================================================================================

        // GET: Customers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            using (MoviesContext db = new MoviesContext())
            {
                Customer customer = db.Customers.Find(id);
                if (customer == null)
                {
                    return HttpNotFound();
                }
                return View(customer);
            }

        }



        //============================================================================================================
        // Customer-Create: Lägg till ny kund.
        //============================================================================================================

        // GET: Customers/Create
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
                using (MoviesContext db = new MoviesContext())
                {
                    db.Customers.Add(customer);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }

            return View(customer);
        }



        //============================================================================================================
        // Customer-Edit: Ändra på befintlig kund.
        //
        // Parameter:
        // ----------
        //  id: CustomerId (nyckel för kund).
        //============================================================================================================

        // GET: Customers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            using (MoviesContext db = new MoviesContext())
            {
                Customer customer = db.Customers.Find(id);
                if (customer == null)
                {
                    return HttpNotFound();
                }

                return View(customer);
            }

        }


        // POST: Customers/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "customerId,firstName,lastName")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                using (MoviesContext db = new MoviesContext())
                {
                    db.Entry(customer).State = EntityState.Modified;
                    db.SaveChanges();
                }

                return RedirectToAction("Index");

            }

            return View(customer);

        }



        //============================================================================================================
        // Customer-Delete: Ta bort kund.
        //
        // Parameter:
        // ----------
        //  id: CustomerId (nyckel för kund).
        //============================================================================================================

        // GET: Customers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            using (MoviesContext db = new MoviesContext())
            {
                Customer customer = db.Customers.Find(id);
                if (customer == null)
                {
                    return HttpNotFound();
                }

                return View(customer);
            }
        }

        // POST: Customers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            using (MoviesContext db = new MoviesContext())
            {
                Customer customer = db.Customers.Find(id);
                db.Customers.Remove(customer);
                db.SaveChanges();
            }

            return RedirectToAction("Index");

        }

    }
}
