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
    public class MoviesController : Controller
    {


        //============================================================================================================
        // Action för Movies-lista (administration av filmer).
        //
        // Parameter Sortorder:
        // --------------------
        //  genre_desc: sortera fallande per genre sen titel.
        //  title: sortera stigande per titel sen genre.
        //  title_desc: sortera fallande per titel sen genre.
        //  tom parameter (default): sortera stigande per genre sen titel.
        //============================================================================================================

        // GET: Movies
        public ActionResult Index(string sortOrder)
        {
            ViewBag.GenreSort = String.IsNullOrEmpty(sortOrder) ? "genre_desc" : "";
            ViewBag.TitleSort = String.IsNullOrEmpty(sortOrder) ? "title_desc" : "";

            using (MoviesContext db = new MoviesContext())
            {
                var movies = from m in db.Movies select m;

                switch (sortOrder)
                {
                    case "genre_desc":
                        movies = movies.OrderByDescending(m => m.genre).ThenByDescending(m => m.title);
                        break;
                    case "title":
                        movies = movies.OrderBy(m => m.title).ThenByDescending(m => m.genre);
                        break;
                    case "title_desc":
                        movies = movies.OrderByDescending(m => m.title).ThenByDescending(m => m.genre);
                        break;
                    default:
                        movies = movies.OrderBy(m => m.genre).ThenBy(m => m.title);
                        break;
                }

                return View(movies.ToList());
            }
        }



        //============================================================================================================
        // Movie-details (detaljer om specifik film).
        //
        // Parameter:
        // ----------
        //  id: MovieId (nyckel för film).
        //============================================================================================================

        // GET: Movies/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            using (MoviesContext db = new MoviesContext())
            {
                Movie movie = db.Movies.Find(id);
                if (movie == null)
                {
                    return HttpNotFound();
                }
                return View(movie);
            }

        }



        //============================================================================================================
        // Movie-Create: Lägg till ny film.
        //============================================================================================================

        // GET: Movies/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Movies/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "movieId,genre,title,length,numberOf")] Movie movie)
        {
            if (ModelState.IsValid)
            {
                using (MoviesContext db = new MoviesContext())
                {
                    db.Movies.Add(movie);
                    db.SaveChanges();
                }

                return RedirectToAction("Index");
            }

            return View(movie);
        }



        //============================================================================================================
        // Movie-Edit: Ändra på befintlig film.
        //
        // Parameter:
        // ----------
        //  id: MovieId (nyckel för film).
        //============================================================================================================

        // GET: Movies/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            using (MoviesContext db = new MoviesContext())
            {
                Movie movie = db.Movies.Find(id);
                if (movie == null)
                {
                    return HttpNotFound();
                }

                return View(movie);
            }

        }


        // POST: Movies/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "movieId,genre,title,length,numberOf")] Movie movie)
        {
            if (ModelState.IsValid)
            {
                using (MoviesContext db = new MoviesContext())
                {
                    db.Entry(movie).State = EntityState.Modified;
                    db.SaveChanges();
                }

                return RedirectToAction("Index");
            }

            return View(movie);
        }



        //============================================================================================================
        // Movie-details (detaljer om specifik film).
        //
        // Parameter:
        // ----------
        //  id: MovieId (nyckel för film).
        //============================================================================================================

        // GET: Movies/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            using (MoviesContext db = new MoviesContext())
            {
                Movie movie = db.Movies.Find(id);
                if (movie == null)
                {
                    return HttpNotFound();
                }
                return View(movie);
            }

        }


        // POST: Movies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            using (MoviesContext db = new MoviesContext())
            {
                Movie movie = db.Movies.Find(id);
                db.Movies.Remove(movie);
                db.SaveChanges();
            }

            return RedirectToAction("Index");
        }

    }
}
