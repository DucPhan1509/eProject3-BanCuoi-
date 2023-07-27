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
    public class ThongTinsController : Controller
    {
        private readonly C2108L_Nhom6Context _context;

        public ThongTinsController(C2108L_Nhom6Context context)
        {
            _context = context;
        }

        // GET: Admin/ThongTins
        public async Task<IActionResult> Index()
        {
            var c2108L_Nhom6Context = _context.ThongTins.Include(t => t.Service);
            return View(await c2108L_Nhom6Context.ToListAsync());
        }

        // GET: Admin/ThongTins/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.ThongTins == null)
            {
                return NotFound();
            }

            var thongTin = await _context.ThongTins
                .Include(t => t.Service)
                .FirstOrDefaultAsync(m => m.DetailId == id);
            if (thongTin == null)
            {
                return NotFound();
            }

            return View(thongTin);
        }

        // GET: Admin/ThongTins/Create
        public IActionResult Create()
        {
            ViewData["ServiceId"] = new SelectList(_context.DichVus, "ServiceId", "ServiceId");
            return View();
        }

        // POST: Admin/ThongTins/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DetailId,ServiceId,Price")] ThongTin thongTin)
        {
            if (ModelState.IsValid)
            {
                _context.Add(thongTin);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ServiceId"] = new SelectList(_context.DichVus, "ServiceId", "ServiceId", thongTin.ServiceId);
            return View(thongTin);
        }

        // GET: Admin/ThongTins/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.ThongTins == null)
            {
                return NotFound();
            }

            var thongTin = await _context.ThongTins.FindAsync(id);
            if (thongTin == null)
            {
                return NotFound();
            }
            ViewData["ServiceId"] = new SelectList(_context.DichVus, "ServiceId", "ServiceId", thongTin.ServiceId);
            return View(thongTin);
        }

        // POST: Admin/ThongTins/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("DetailId,ServiceId,Price")] ThongTin thongTin)
        {
            if (id != thongTin.DetailId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(thongTin);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ThongTinExists(thongTin.DetailId))
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
            ViewData["ServiceId"] = new SelectList(_context.DichVus, "ServiceId", "ServiceId", thongTin.ServiceId);
            return View(thongTin);
        }

        // GET: Admin/ThongTins/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.ThongTins == null)
            {
                return NotFound();
            }

            var thongTin = await _context.ThongTins
                .Include(t => t.Service)
                .FirstOrDefaultAsync(m => m.DetailId == id);
            if (thongTin == null)
            {
                return NotFound();
            }

            return View(thongTin);
        }

        // POST: Admin/ThongTins/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.ThongTins == null)
            {
                return Problem("Entity set 'C2108L_Nhom6Context.ThongTins'  is null.");
            }
            var thongTin = await _context.ThongTins.FindAsync(id);
            if (thongTin != null)
            {
                _context.ThongTins.Remove(thongTin);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ThongTinExists(int id)
        {
          return (_context.ThongTins?.Any(e => e.DetailId == id)).GetValueOrDefault();
        }
    }
}
