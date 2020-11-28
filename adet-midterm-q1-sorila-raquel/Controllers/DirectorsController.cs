using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using app.repository;
using app.service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MvcMovie.Data;
using MvcMovie.Models;
using app.domain;

namespace MvcMovie.Controllers
{
    public class DirectorsController : Controller
    {
        private readonly MvcMovieContext _context;
        private readonly IDirectorService _directorService;
        private readonly MovieDbContext _db;

        public DirectorsController(MvcMovieContext context, IDirectorService directorService, MovieDbContext db)
        {
            _context = context;
            _directorService = directorService;
            _db = db;
        }

        // GET: Directors
        public async Task<IActionResult> Index()
        {
            return View(await _db.Directors.ToListAsync());
            //return View(await _context.Directors.ToListAsync());
        }

        // GET: Directors/Details/5
        public IActionResult Details(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var director = _directorService.GetDirectorById(new app.service.Directors.Query.GetDirectorById.GetDirectorByIdQuery
            {
                ID = id
            });
            if (director == null)
            {
                return NotFound();
            }

            return View(director);
        }

        // GET: Directors/Create
       
        public IActionResult Create()
        {
            return View();
        }

        // POST: Directors/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(app.domain.Director director)
        {
            if (ModelState.IsValid)
            {
                _directorService.CreateDirector(new app.service.Directors.Commands.CreateDirector.CreateDirectorCommand
                {
                    Name = director.Name
                });


                return RedirectToAction(nameof(Index));
            }
            return View(director);
        }

        // GET: Directors/Edit/5
        public  IActionResult Edit(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var director = _directorService.GetDirectorById(new app.service.Directors.Query.GetDirectorById.GetDirectorByIdQuery
            {
                ID = id
            });

            if (director == null)
            {
                return NotFound();
            }
            return View(director);
        }

        // POST: Directors/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public  IActionResult Edit(int id, app.service.Directors.Query.GetDirectorById.GetDirectorByIdResult director)
        {
            if (id != director.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _directorService.EditDirector(new app.service.Directors.Commands.EditDirector.EditDirectorCommand
                {
                    ID = director.ID,
                    Name = director.Name
                });

                return RedirectToAction(nameof(Index));
            }
            return View(director);
        }

        //GET: Directors/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var director = await _db.Directors
                .FirstOrDefaultAsync(m => m.ID == id);
            if (director == null)
            {
                return NotFound();
            }

            return View(director);
        }

        // POST: Directors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var director = await _db.Directors.FindAsync(id);
            var movie = await _db.Movies.Where(i => i.DirectorID == id).ToListAsync();
            foreach (var mov in movie)
            {
                mov.DirectorID = null;

            }
            _db.Directors.Remove(director);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DirectorExists(int id)
        {
            return _context.Directors.Any(e => e.ID == id);
        }
    }
}
