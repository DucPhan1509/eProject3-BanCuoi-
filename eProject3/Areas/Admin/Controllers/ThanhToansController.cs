using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using eProject3.Models;
using AspNetCoreHero.ToastNotification.Notyf;

namespace eProject3.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ThanhToansController : Controller
    {
        private readonly C2108L_Nhom6Context _context;

        public ThanhToansController(C2108L_Nhom6Context context)
        {
            _context = context;
            //_notyfService = notyfService;
        }

        // GET: Admin/ThanhToans
        public async Task<IActionResult> Index()
        {
            var c2108L_Nhom6Context = _context.ThanhToans.Include(t => t.Account).Include(t => t.Detail);
            return View(await c2108L_Nhom6Context.ToListAsync());
        }

        // GET: Admin/ThanhToans/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.ThanhToans == null)
            {
                return NotFound();
            }

            var thanhToan = await _context.ThanhToans
                .Include(t => t.Account)
                .Include(t => t.Detail)
                .FirstOrDefaultAsync(m => m.BillId == id);
            if (thanhToan == null)
            {
                return NotFound();
            }

            return View(thanhToan);
        }

        // GET: Admin/ThanhToans/Create
        public IActionResult Create()
        {
            ViewData["AccountId"] = new SelectList(_context.TaiKhoans, "AccountId", "AccountId");
            ViewData["DetailId"] = new SelectList(_context.ThongTins, "DetailId", "DetailId");
            return View();
        }

        // POST: Admin/ThanhToans/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("BillId,AccountId,Phone,DetailId,Total")] ThanhToan thanhToan)
        {
            if (ModelState.IsValid)
            {
                _context.Add(thanhToan);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AccountId"] = new SelectList(_context.TaiKhoans, "AccountId", "AccountId", thanhToan.AccountId);
            ViewData["DetailId"] = new SelectList(_context.ThongTins, "DetailId", "DetailId", thanhToan.DetailId);
            return View(thanhToan);
        }

        // GET: Admin/ThanhToans/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.ThanhToans == null)
            {
                return NotFound();
            }

            var thanhToan = await _context.ThanhToans.FindAsync(id);
            if (thanhToan == null)
            {
                return NotFound();
            }
            ViewData["AccountId"] = new SelectList(_context.TaiKhoans, "AccountId", "AccountId", thanhToan.AccountId);
            ViewData["DetailId"] = new SelectList(_context.ThongTins, "DetailId", "DetailId", thanhToan.DetailId);
            return View(thanhToan);
        }

        // POST: Admin/ThanhToans/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("BillId,AccountId,Phone,DetailId,Total")] ThanhToan thanhToan)
        {
            if (id != thanhToan.BillId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(thanhToan);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ThanhToanExists(thanhToan.BillId))
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
            ViewData["AccountId"] = new SelectList(_context.TaiKhoans, "AccountId", "AccountId", thanhToan.AccountId);
            ViewData["DetailId"] = new SelectList(_context.ThongTins, "DetailId", "DetailId", thanhToan.DetailId);
            return View(thanhToan);
        }

        // GET: Admin/ThanhToans/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.ThanhToans == null)
            {
                return NotFound();
            }

            var thanhToan = await _context.ThanhToans
                .Include(t => t.Account)
                .Include(t => t.Detail)
                .FirstOrDefaultAsync(m => m.BillId == id);
            if (thanhToan == null)
            {
                return NotFound();
            }

            return View(thanhToan);
        }

        // POST: Admin/ThanhToans/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.ThanhToans == null)
            {
                return Problem("Entity set 'C2108L_Nhom6Context.ThanhToans'  is null.");
            }
            var thanhToan = await _context.ThanhToans.FindAsync(id);
            if (thanhToan != null)
            {
                _context.ThanhToans.Remove(thanhToan);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ThanhToanExists(int id)
        {
          return (_context.ThanhToans?.Any(e => e.BillId == id)).GetValueOrDefault();
        }
    }
}
