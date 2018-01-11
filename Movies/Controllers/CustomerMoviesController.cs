using Movies.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Movies.Controllers
{
    public class CustomerMoviesController : Controller
    {
        private CustomerMoviesViewModel customerMovies = new CustomerMoviesViewModel();


        //==============================================================================================================================
        // Lista all filmer som aktuell kund har hyrt samt övriga tillgängliga för uthyrning.
        //
        // Parameter:
        // ----------
        //  id: CustomerId (nyckel för kund).
        //==============================================================================================================================

        // GET: CustomerMovies
        public ActionResult Index(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            using (MoviesContext db = new MoviesContext())
            {
                customerMovies.customer = db.Customers.Find(id);


                if (customerMovies.customer == null)
                {
                    return HttpNotFound();
                }

                //-- Skapa lista med alla filmer inklusive antal uthyrda för respektive film.
                var moviesCustomerCount = db.Movies.Select(m => new { movie = m, count = m.customers.Count() }).OrderBy(o => o.movie.title).ToList();

                //-- Skapa lista med filmer som aktuell kund redan hyrt.
                customerMovies.moviesRented = (from movie in db.Movies where movie.customers.Any(c => c.customerId == id) select movie).OrderBy(o => o.title).ToList();

                //-- Skapa lista med tillgängliga filmer för uthyrning (alla exklusive redan hyrda filmer för aktuell kund samt filmer som nåt max antal hyrda).
                customerMovies.moviesAvailable = moviesCustomerCount.Where(i => i.count < i.movie.numberOf).Select(i => i.movie).Except(customerMovies.moviesRented).ToList();

                return View(customerMovies);
            }

        }



        //==============================================================================================================================
        // Hyr eller återlämna film.
        //
        // Parameter:
        // ----------
        //  customerId: nyckel för kund.
        //  movieId: nyckel för film.
        //==============================================================================================================================

        // POST: customerMovies/Index/5
        [HttpPost, ActionName("Index")]
        [ValidateAntiForgeryToken]
        public ActionResult Rent(int customerId, int movieId)
        {

            using (MoviesContext db = new MoviesContext())
            {
                Customer customer = db.Customers.Find(customerId);
                Movie movie = db.Movies.Find(movieId);

                //- Om filmen redan hyrd av kund, ta bort, annars lägg till.
                if (customer.movies.Contains(movie))
                    customer.movies.Remove(movie);

                else
                    customer.movies.Add(movie);

                db.SaveChanges();
            }

            return RedirectToAction("Index", new { id = customerId });

        }

    }
}