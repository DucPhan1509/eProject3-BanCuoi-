using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using eProject3.Models;
using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PagedList.Core;

namespace eProject3.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class LoaisController : Controller
    {
        private readonly C2108L_Nhom6Context _context;
        public INotyfService _notyfService { get; }
        public LoaisController(C2108L_Nhom6Context context, INotyfService notyfService)
        {
            _context = context;
            _notyfService = notyfService;
        }

        // GET: Admin/Loais
        public async Task<IActionResult> Index(int? page)
        {
            var pageNumber = page == null || page >= 0 ? 1 : page.Value;
            var pageSize = 20;
            var IsCat = _context.Loais
                .AsNoTracking()
                .OrderByDescending(x => x.CatId);
            PagedList<Loai> models = new PagedList<Loai>(IsCat, pageNumber, pageSize);
            ViewBag.CurrentPage = pageNumber;
            return View(models);
        }

        // GET: Admin/Loais/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Loais == null)
            {
                return NotFound();
            }

            var loai = await _context.Loais
                .FirstOrDefaultAsync(m => m.CatId == id);
            if (loai == null)
            {
                return NotFound();
            }

            return View(loai);
        }

        // GET: Admin/Loais/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/Loais/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CatId,CatName")] Loai loai)
        {
            if (ModelState.IsValid)
            {
                _context.Add(loai);
                await _context.SaveChangesAsync();
                _notyfService.Success("Success");
                return RedirectToAction(nameof(Index));
            }
            return View(loai);
        }

        // GET: Admin/Loais/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Loais == null)
            {
                return NotFound();
            }

            var loai = await _context.Loais.FindAsync(id);
            if (loai == null)
            {
                return NotFound();
            }
            return View(loai);
        }

        // POST: Admin/Loais/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CatId,CatName")] Loai loai)
        {
            if (id != loai.CatId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(loai);
                    await _context.SaveChangesAsync();
                    _notyfService.Success("Success");
                    
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LoaiExists(loai.CatId))
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
            return View(loai);
        }

        // GET: Admin/Loais/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Loais == null)
            {
                return NotFound();
            }

            var loai = await _context.Loais
                .FirstOrDefaultAsync(m => m.CatId == id);
            if (loai == null)
            {
                return NotFound();
            }

            return View(loai);
        }

        // POST: Admin/Loais/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Loais == null)
            {
                return Problem("Entity set 'C2108L_Nhom6Context.Loais'  is null.");
            }
            var loai = await _context.Loais.FindAsync(id);
            if (loai != null)
            {
                _context.Loais.Remove(loai);
                _notyfService.Success("Success");
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LoaiExists(int id)
        {
          return (_context.Loais?.Any(e => e.CatId == id)).GetValueOrDefault();
        }
    }
}
