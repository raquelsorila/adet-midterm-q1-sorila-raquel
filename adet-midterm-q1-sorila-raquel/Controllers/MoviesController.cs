using app.repository;
using app.service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MvcMovie.Data;
using MvcMovie.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcMovie.Controllers
{
    public class MoviesController : Controller
    {
        private readonly MvcMovieContext _context;
        private readonly MovieDbContext _db;
        private readonly IMovieService _movieService;

        public MoviesController(MvcMovieContext context, MovieDbContext db, IMovieService movieService)
        {
            _context = context;
            _movieService = movieService;
            _db = db;

        }

        // GET: Movies
        public async Task<IActionResult> Index(string movieGenre, string searchString)
        {
            IQueryable<string> genreQuery = from m in _db.Movies
                                            orderby m.Genre
                                            select m.Genre;

            var movies = from m in _db.Movies
                         select m;

            if (!string.IsNullOrEmpty(searchString))
            {
                movies = movies.Where(s => s.Title.ToLower().Contains(searchString.ToLower()));
            }

            if (!string.IsNullOrWhiteSpace(movieGenre))
            {
                movies = movies.Where(x => x.Genre.ToLower() == movieGenre.ToLower());
            }

            var movieGenreVM = new app.domain.MovieGenreViewModel
            {
                Genres = new SelectList(await genreQuery.Distinct().ToListAsync()),
                Movies = await movies.ToListAsync()
            };

            return View(movieGenreVM);
        }

        [HttpPost]
        public string Index(string searchString, bool notUsed)
        {
            return $"From [HttpPost] Index: filter on {searchString}";
        }

        // GET: Movies/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movie = _movieService.GetMovieById(new app.service.Movies.Query.GetMovieById.GetMovieByIdQuery
            {
                ID = id
            });

            if (movie == null)
            {
                return NotFound();
            }

            return View(movie);
        }

        #region Create
        // GET: Movies/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Movies/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(app.domain.Movie movie)
        {
            if (ModelState.IsValid)
            {
                _movieService.CreateMovie(new app.service.Movies.Commands.CreateMovie.CreateMovieCommand
                {
                    Title = movie.Title,
                    ReleaseDate = movie.ReleaseDate,
                    Genre = movie.Genre,
                    Rating = movie.Rating,
                    Price = movie.Price,
                    DirectorID = movie.DirectorID
                });
                return RedirectToAction(nameof(Index));
            }
            return View(movie);
        }


        #endregion

        #region Edit
        // GET: Movies/Edit/5
        public IActionResult Edit(int? id)
        {

            ViewData["Directors"] = CreateDirectorDropdown();


            if (id == null)
            {
                return NotFound();
            }

            var movie = _movieService.GetMovieById(new app.service.Movies.Query.GetMovieById.GetMovieByIdQuery 
            { 
                ID = id
            });
            if (movie == null)
            {
                return NotFound();
            }

            return View(movie);
        }

    

        // POST: Movies/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, app.service.Movies.Query.GetMovieById.GetMovieByIdResult movie)
        {

           
            if (id != movie.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                
                _movieService.EditMovie(new app.service.Movies.Commands.EditMovie.EditMovieCommand
                {
                    ID = movie.ID,
                    Title = movie.Title,
                    ReleaseDate = movie.ReleaseDate,
                    Genre = movie.Genre,
                    Rating = movie.Rating,
                    Price = movie.Price,
                    DirectorID = movie.DirectorID
                });
                
                
                return RedirectToAction(nameof(Index));
            }

            ViewData["Directors"] = CreateDirectorDropdown();

            return View(movie);
        }


        #endregion


        // GET: Movies/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movie = _movieService.GetMovieById(new app.service.Movies.Query.GetMovieById.GetMovieByIdQuery
            {
                ID = id
            });
            if (movie == null)
            {
                return NotFound();
            }

            return View(movie);
        }

        // POST: Movies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var movie = await _db.Movies.FindAsync(id);
            _db.Movies.Remove(movie);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MovieExists(int id)
        {
            return _db.Movies.Any(e => e.ID == id);
        }


        private SelectList CreateDirectorDropdown()
        {
            var directors = _db.Directors.AsNoTracking().ToArray();

            var selectList = new SelectList(
                directors.Select(i => new SelectListItem { Text = i.Name, Value = i.ID.ToString() }),
                 "Value",
                "Text");
            return selectList;
        }
    }
}