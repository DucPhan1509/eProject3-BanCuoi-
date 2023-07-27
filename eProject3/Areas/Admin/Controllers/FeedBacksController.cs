using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using eProject3.Models;

namespace eProject3.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class FeedBacksController : Controller
    {
        private readonly C2108L_Nhom6Context _context;

        public FeedBacksController(C2108L_Nhom6Context context)
        {
            _context = context;
        }

        // GET: Admin/FeedBacks
        public async Task<IActionResult> Index()
        {
            var c2108L_Nhom6Context = _context.FeedBacks.Include(f => f.Account);
            return View(await c2108L_Nhom6Context.ToListAsync());
        }

        // GET: Admin/FeedBacks/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.FeedBacks == null)
            {
                return NotFound();
            }

            var feedBack = await _context.FeedBacks
                .Include(f => f.Account)
                .FirstOrDefaultAsync(m => m.FeedBack1 == id);
            if (feedBack == null)
            {
                return NotFound();
            }

            return View(feedBack);
        }

        // GET: Admin/FeedBacks/Create
        public IActionResult Create()
        {
            ViewData["AccountId"] = new SelectList(_context.TaiKhoans, "AccountId", "AccountId");
            return View();
        }

        // POST: Admin/FeedBacks/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FeedBack1,AccountId,Contents")] FeedBack feedBack)
        {
            if (ModelState.IsValid)
            {
                _context.Add(feedBack);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AccountId"] = new SelectList(_context.TaiKhoans, "AccountId", "AccountId", feedBack.AccountId);
            return View(feedBack);
        }

        // GET: Admin/FeedBacks/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.FeedBacks == null)
            {
                return NotFound();
            }

            var feedBack = await _context.FeedBacks.FindAsync(id);
            if (feedBack == null)
            {
                return NotFound();
            }
            ViewData["AccountId"] = new SelectList(_context.TaiKhoans, "AccountId", "AccountId", feedBack.AccountId);
            return View(feedBack);
        }

        // POST: Admin/FeedBacks/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("FeedBack1,AccountId,Contents")] FeedBack feedBack)
        {
            if (id != feedBack.FeedBack1)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(feedBack);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FeedBackExists(feedBack.FeedBack1))
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
            ViewData["AccountId"] = new SelectList(_context.TaiKhoans, "AccountId", "AccountId", feedBack.AccountId);
            return View(feedBack);
        }

        // GET: Admin/FeedBacks/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.FeedBacks == null)
            {
                return NotFound();
            }

            var feedBack = await _context.FeedBacks
                .Include(f => f.Account)
                .FirstOrDefaultAsync(m => m.FeedBack1 == id);
            if (feedBack == null)
            {
                return NotFound();
            }

            return View(feedBack);
        }

        // POST: Admin/FeedBacks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.FeedBacks == null)
            {
                return Problem("Entity set 'C2108L_Nhom6Context.FeedBacks'  is null.");
            }
            var feedBack = await _context.FeedBacks.FindAsync(id);
            if (feedBack != null)
            {
                _context.FeedBacks.Remove(feedBack);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FeedBackExists(int id)
        {
          return (_context.FeedBacks?.Any(e => e.FeedBack1 == id)).GetValueOrDefault();
        }
    }
}
