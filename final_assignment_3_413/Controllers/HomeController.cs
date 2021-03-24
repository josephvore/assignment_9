using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using final_assignment_3_413.Models;
using Microsoft.EntityFrameworkCore;

namespace final_assignment_3_413.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private MovieDbContext _context;

        private IMovieRepository _repository;

        public static int MovieStaticID;

        public HomeController(ILogger<HomeController> logger, MovieDbContext context, IMovieRepository repository)
        {
            _logger = logger;
            _repository = repository;
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult MyPodcasts()
        {
            return View();
        }

        [HttpGet]
        public IActionResult NewMovie()
        {
            return View();
        }

        [HttpPost]
        public IActionResult NewMovie(NewMovie newMovie)
        {

            if (ModelState.IsValid)
            {
                _context.NewMovie.Add(newMovie);
                _context.SaveChanges();
                return View("ListMovies", _context.NewMovie);
            }

            return View("NewMovie");
        }

        [HttpPost]
        public IActionResult EditMovies(int id)
        {
            MovieStaticID = id;
            return View("EditMovies", new MoviesViewModel
            {
                NewMovieModel = _context.NewMovie.Single(x => x.MovieID == MovieStaticID),
                ID = MovieStaticID
            });
        }

        public ViewResult ListMovies()
        {
            return View(_context.NewMovie);
        }

        [HttpPost]
        public IActionResult UpdateMovies(MoviesViewModel model)
        {
            if (ModelState.IsValid)
            {
                var movie = _context.NewMovie.Single(x => x.MovieID == MovieStaticID);
                _context.Entry(movie).Property(x => x.Category).CurrentValue = model.NewMovieModel.Category;
                _context.Entry(movie).Property(x => x.Title).CurrentValue = model.NewMovieModel.Title;
                _context.Entry(movie).Property(x => x.Year).CurrentValue = model.NewMovieModel.Year;
                _context.Entry(movie).Property(x => x.Director).CurrentValue = model.NewMovieModel.Director;
                _context.Entry(movie).Property(x => x.Rating).CurrentValue = model.NewMovieModel.Rating;
                _context.Entry(movie).Property(x => x.Edited).CurrentValue = model.NewMovieModel.Edited;
                _context.Entry(movie).Property(x => x.LentTo).CurrentValue = model.NewMovieModel.LentTo;
                _context.Entry(movie).Property(x => x.Notes).CurrentValue = model.NewMovieModel.Notes;
                _context.SaveChanges();
                return RedirectToAction("ListMovies");
            }
            else
            {
                return View(new MoviesViewModel
                {
                    NewMovieModel = _context.NewMovie.Single(x => x.MovieID == MovieStaticID), 
                    ID = MovieStaticID
                });
            }
        }

        public IActionResult DeleteMovies(int id)
        {
            _context.Remove(_context.NewMovie.Single(x => x.MovieID == id));
            _context.SaveChanges();
            return RedirectToAction("ListMovies");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
