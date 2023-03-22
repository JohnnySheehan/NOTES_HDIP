using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NOTES_HDIP.Data;
using NOTES_HDIP.Models;

namespace NOTES_HDIP.Controllers
{
    public class NoteSpacesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public NoteSpacesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: NoteSpaces
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.NoteSpaces.Include(n => n.User);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: NoteSpaces/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.NoteSpaces == null)
            {
                return NotFound();
            }

            var noteSpace = await _context.NoteSpaces
                .Include(n => n.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (noteSpace == null)
            {
                return NotFound();
            }

            return View(noteSpace);
        }

        // GET: NoteSpaces/Create
        public IActionResult Create()
        {
            //ViewData["UserID"] = new SelectList(_context.ApplicationUsers, "Id", "Id");
            return View();
        }

        // POST: NoteSpaces/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Description,UserID")] NoteSpace noteSpace)
        {
            if (ModelState.IsValid)
            {
                _context.Add(noteSpace);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            //ViewData["UserID"] = new SelectList(_context.ApplicationUsers, "Id", "Id", noteSpace.UserID);
            return View(noteSpace);
        }

        // GET: NoteSpaces/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.NoteSpaces == null)
            {
                return NotFound();
            }

            var noteSpace = await _context.NoteSpaces.FindAsync(id);
            if (noteSpace == null)
            {
                return NotFound();
            }
            //ViewData["UserID"] = new SelectList(_context.ApplicationUsers, "Id", "Id", noteSpace.UserID);
            return View(noteSpace);
        }

        // POST: NoteSpaces/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description,UserID")] NoteSpace noteSpace)
        {
            if (id != noteSpace.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(noteSpace);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!NoteSpaceExists(noteSpace.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
           // ViewData["UserID"] = new SelectList(_context.ApplicationUsers, "Id", "Id", noteSpace.UserID);
            return View(noteSpace);
        }

        // GET: NoteSpaces/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.NoteSpaces == null)
            {
                return NotFound();
            }

            var noteSpace = await _context.NoteSpaces
                .Include(n => n.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (noteSpace == null)
            {
                return NotFound();
            }

            return View(noteSpace);
        }

        // POST: NoteSpaces/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.NoteSpaces == null)
            {
                return Problem("Entity set 'ApplicationDbContext.NoteSpaces'  is null.");
            }
            var noteSpace = await _context.NoteSpaces.FindAsync(id);
            if (noteSpace != null)
            {
                _context.NoteSpaces.Remove(noteSpace);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool NoteSpaceExists(int id)
        {
          return _context.NoteSpaces.Any(e => e.Id == id);
        }
    }
}
