using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using eProject3.Models;

namespace eProject3.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class DichVusController : Controller
    {
        private readonly C2108L_Nhom6Context _context;
        public INotyfService _notyfService { get; }

        public DichVusController(C2108L_Nhom6Context context, INotyfService notyfService)
        {
            _context = context;
            _notyfService = notyfService;
        }

        // GET: Admin/DichVus
        public async Task<IActionResult> Index()
        {
            var c2108L_Nhom6Context = _context.DichVus.Include(d => d.Cat).Include(d => d.Provider);
            return View(await c2108L_Nhom6Context.ToListAsync());
        }

        // GET: Admin/DichVus/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.DichVus == null)
            {
                return NotFound();
            }

            var dichVu = await _context.DichVus
                .Include(d => d.Cat)
                .Include(d => d.Provider)
                .FirstOrDefaultAsync(m => m.ServiceId == id);
            if (dichVu == null)
            {
                return NotFound();
            }

            return View(dichVu);
        }

        // GET: Admin/DichVus/Create
        public IActionResult Create()
        {
            ViewData["CatId"] = new SelectList(_context.Loais, "CatId", "CatId");
            ViewData["ProviderId"] = new SelectList(_context.Providers, "ProviderId", "ProviderId");
            return View();
        }

        // POST: Admin/DichVus/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ServiceId,SerName,CatId,ProviderId,Description,Price,Thumb")] DichVu dichVu, Microsoft.AspNetCore.Http.IFormFile fThumb/*HttpPostedFileBase fThumb*/)
        {
            if (ModelState.IsValid)
            {
                if (fThumb != null && fThumb.Length > 0)
                {
                    dichVu.Thumb = Path.GetFileName(fThumb.FileName);
                }
                _context.Add(dichVu);
                await _context.SaveChangesAsync();
                _notyfService.Success("Success");
                return RedirectToAction(nameof(Index));
            }
            ViewData["CatId"] = new SelectList(_context.Loais, "CatId", "CatId", dichVu.CatId);
            ViewData["ProviderId"] = new SelectList(_context.Providers, "ProviderId", "ProviderId", dichVu.ProviderId);
            return View(dichVu);
        }

        // GET: Admin/DichVus/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.DichVus == null)
            {
                return NotFound();
            }

            var dichVu = await _context.DichVus.FindAsync(id);
            if (dichVu == null)
            {
                return NotFound();
            }
            ViewData["CatId"] = new SelectList(_context.Loais, "CatId", "CatId", dichVu.CatId);
            ViewData["ProviderId"] = new SelectList(_context.Providers, "ProviderId", "ProviderId", dichVu.ProviderId);
            return View(dichVu);
        }

        // POST: Admin/DichVus/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ServiceId,SerName,CatId,ProviderId,Description,Price,Thumb")] DichVu dichVu, Microsoft.AspNetCore.Http.IFormFile fThumb)
        {
            if (id != dichVu.ServiceId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (fThumb != null && fThumb.Length > 0)
                    {
                        dichVu.Thumb = Path.GetFileName(fThumb.FileName);
                    }
                    _context.Update(dichVu);
                    _notyfService.Success("Success");
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DichVuExists(dichVu.ServiceId))
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
            ViewData["CatId"] = new SelectList(_context.Loais, "CatId", "CatId", dichVu.CatId);
            ViewData["ProviderId"] = new SelectList(_context.Providers, "ProviderId", "ProviderId", dichVu.ProviderId);
            return View(dichVu);
        }

        // GET: Admin/DichVus/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.DichVus == null)
            {
                return NotFound();
            }

            var dichVu = await _context.DichVus
                .Include(d => d.Cat)
                .Include(d => d.Provider)
                .FirstOrDefaultAsync(m => m.ServiceId == id);
            if (dichVu == null)
            {
                return NotFound();
            }

            return View(dichVu);
        }

        // POST: Admin/DichVus/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.DichVus == null)
            {
                return Problem("Entity set 'C2108L_Nhom6Context.DichVus'  is null.");
            }
            var dichVu = await _context.DichVus.FindAsync(id);
            if (dichVu != null)
            {
                _context.DichVus.Remove(dichVu);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DichVuExists(int id)
        {
          return (_context.DichVus?.Any(e => e.ServiceId == id)).GetValueOrDefault();
        }
    }
}
