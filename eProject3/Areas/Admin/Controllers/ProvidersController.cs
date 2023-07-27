using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using eProject3.Models;
using AspNetCoreHero.ToastNotification.Abstractions;

namespace eProject3.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProvidersController : Controller
    {
        private readonly C2108L_Nhom6Context _context;
        public INotyfService _notyfService { get; }
        public ProvidersController(C2108L_Nhom6Context context, INotyfService notyfService)
        {
            _context = context;
            _notyfService = notyfService;
        }

        // GET: Admin/Providers
        public async Task<IActionResult> Index()
        {
              return _context.Providers != null ? 
                          View(await _context.Providers.ToListAsync()) :
                          Problem("Entity set 'C2108L_Nhom6Context.Providers'  is null.");
        }

        // GET: Admin/Providers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Providers == null)
            {
                return NotFound();
            }

            var provider = await _context.Providers
                .FirstOrDefaultAsync(m => m.ProviderId == id);
            if (provider == null)
            {
                return NotFound();
            }

            return View(provider);
        }

        // GET: Admin/Providers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/Providers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProviderId,ProviderName")] Provider provider)
        {
            if (ModelState.IsValid)
            {
                _context.Add(provider);
                await _context.SaveChangesAsync();
                _notyfService.Success("Success");
                return RedirectToAction(nameof(Index));
            }
            return View(provider);
        }

        // GET: Admin/Providers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Providers == null)
            {
                return NotFound();
            }

            var provider = await _context.Providers.FindAsync(id);
            if (provider == null)
            {
                return NotFound();
            }
            return View(provider);
        }

        // POST: Admin/Providers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ProviderId,ProviderName")] Provider provider)
        {
            if (id != provider.ProviderId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(provider);
                    _notyfService.Success("Success");
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProviderExists(provider.ProviderId))
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
            return View(provider);
        }

        // GET: Admin/Providers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Providers == null)
            {
                return NotFound();
            }

            var provider = await _context.Providers
                .FirstOrDefaultAsync(m => m.ProviderId == id);
            if (provider == null)
            {
                return NotFound();
            }

            return View(provider);
        }

        // POST: Admin/Providers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Providers == null)
            {
                return Problem("Entity set 'C2108L_Nhom6Context.Providers'  is null.");
            }
            var provider = await _context.Providers.FindAsync(id);
            if (provider != null)
            {
                _context.Providers.Remove(provider);
                _notyfService.Success("Success");
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProviderExists(int id)
        {
          return (_context.Providers?.Any(e => e.ProviderId == id)).GetValueOrDefault();
        }
    }
}
