using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVCexample.Models;
using MVCexample.ViewModel;
using System.Data.Entity;

namespace MVCexample.Controllers
{
    [Authorize]
    public class MoviesController : Controller
    {
        private ApplicationDbContext DbContext = null;
        public MoviesController()
        {
            DbContext = new ApplicationDbContext();


        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                DbContext.Dispose();
            }
        }


        // GET: Movies

        [AllowAnonymous]
        public ActionResult Index()
        {
            var movies = DbContext.Movies.ToList();
            return View(movies);
        }
        //public ActionResult Display()
        //{
        //    Movie movie = new Movie();
        //    movie.ID = 1;
        //    movie.MovieName = "Kappaan";
        //    return View(movie);
        //}
        //public ActionResult Display()
        //{
        //    CustomerMovieViewModel vm = new CustomerMovieViewModel();
        //    vm.Movies = new Movie { ID = 1, MovieName = "Kappaan" };
        //    vm.Customers = new List<Customer>
        //    {
        //        new Customer{ID=1,CustomerName="Viji"},
        //        new Customer{ID=2,CustomerName="Kavitha"},
        //        new Customer{ID=3,CustomerName="Kani"}
        //    };


        //    return View(vm);
        //}
        //public ActionResult DisplayCustomer()
        //{
        //    CustomerMovieViewModel vm1 = new CustomerMovieViewModel();
        //    vm1.Customers1 = new Customer { CustomerName = "Vijayalakshmi" };
        //    vm1.Movies1 = new List<Movie>
        //    {
        //        new Movie{ID=1,MovieName="ABC"},
        //        new Movie{ID=2,MovieName="XYZ"}
        //    };

        //    return View(vm1);
        //}
        //public List<Movie> GetMovies()
        //{
        //    List<Movie> movies = new List<Movie>
        //    {
        //       new Movie{ID=1,MovieName="Spider Man",Genre="Super Hero",ReleaseDate=Convert.ToDateTime("17/07/2000"),DateAdded=Convert.ToDateTime("11/07/2004")},
        //       new Movie{ID=2,MovieName="Bat Man",Genre="Crime/Action",ReleaseDate=Convert.ToDateTime("17/11/1990"),DateAdded=Convert.ToDateTime("11/05/2011")},
        //       new Movie{ID=3,MovieName="Avengers",Genre="Super Hero",ReleaseDate=Convert.ToDateTime("24/07/2010"),DateAdded=Convert.ToDateTime("22/10/1994")},
        //       new Movie{ID=4,MovieName="Final Destination",Genre="Thriller",ReleaseDate=Convert.ToDateTime("25/07/2015"),DateAdded=Convert.ToDateTime("23/10/2008")}
        //    };

        //    return movies;
        //}

        public ActionResult MovieDetails(int id)
        {
            var movies = DbContext.Movies.Include(g => g.Genre).ToList().SingleOrDefault(a => a.ID == id);
            return View(movies);
        }
        [HttpGet]
        public ActionResult Create()
        {
            var Movies = new Movie();

            ViewBag.GenreID = GetGenreName();

            return View(Movies);
        }
        [HttpPost]
        [ValidateAntiForgeryToken()]
        public ActionResult Create(Movie MovieFromView)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.GenreID = GetGenreName();
                return View(MovieFromView);
            }

            DbContext.Movies.Add(MovieFromView);
            DbContext.SaveChanges();

            return RedirectToAction("Index", "Movies");
        }
        [HttpGet]
        public ActionResult EditMovie(int id)
        {
            var Movies = DbContext.Movies.SingleOrDefault(c => c.ID == id);
            if (Movies != null)
            {
                ViewBag.GenreID = GetGenreName();
                return View(Movies);
            }
            return HttpNotFound("Invalid Movie ID");
        }

        [HttpPost]
        [ValidateAntiForgeryToken()]
        public ActionResult EditMovie(Movie movieFromView)
        {
            if (ModelState.IsValid)
            {
                var movieInDB = DbContext.Movies.FirstOrDefault(c => c.ID == movieFromView.ID);

                movieInDB.MovieName = movieFromView.MovieName;
                movieInDB.ReleaseDate = movieFromView.ReleaseDate;
                movieInDB.DateAdded = movieFromView.DateAdded;
                movieInDB.AvailableStock = movieFromView.AvailableStock;
                movieInDB.GenreID = movieFromView.GenreID;

                DbContext.SaveChanges();
                return RedirectToAction("Index", "Movies");

            }
            else
            {
                ViewBag.GenreID = GetGenreName();
                return View(movieFromView);
            }

        }
        [HttpGet]
        public ActionResult RemoveMovie(int id)
        {
            var Movies = DbContext.Movies.FirstOrDefault(c => c.ID == id);
            if (Movies != null)
            {
                return View(Movies);

            }
            return HttpNotFound("Invalid Movie ID");
        }
        [HttpPost]
        [ValidateAntiForgeryToken()]
        public ActionResult RemoveMovie(Movie movieFromView)
        {
            var moviesInDB = DbContext.Movies.FirstOrDefault(c => c.ID == movieFromView.ID);

            DbContext.Movies.Remove(moviesInDB);
            DbContext.SaveChanges();

            return RedirectToAction("Index", "Movies");
           
        }

        public IEnumerable<SelectListItem> GetGenreName()
        {
            var genre = DbContext.Genres.AsEnumerable().Select(m => new SelectListItem()
            {
                Text = m.Name,
                Value = m.ID.ToString()
            }).ToList();

            genre.Insert(0, new SelectListItem { Text = "---Select---", Value = "0", Disabled = true, Selected = true });

            return genre;
        }
    }
}