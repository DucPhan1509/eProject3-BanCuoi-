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
    public class ChucVusController : Controller
    {
        private readonly C2108L_Nhom6Context _context;

        public ChucVusController(C2108L_Nhom6Context context)
        {
            _context = context;
        }

        // GET: Admin/ChucVus
        public async Task<IActionResult> Index()
        {
              return _context.ChucVus != null ? 
                          View(await _context.ChucVus.ToListAsync()) :
                          Problem("Entity set 'C2108L_Nhom6Context.ChucVus'  is null.");
        }

        // GET: Admin/ChucVus/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.ChucVus == null)
            {
                return NotFound();
            }

            var chucVu = await _context.ChucVus
                .FirstOrDefaultAsync(m => m.RoleId == id);
            if (chucVu == null)
            {
                return NotFound();
            }

            return View(chucVu);
        }

        // GET: Admin/ChucVus/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/ChucVus/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("RoleId,Rol")] ChucVu chucVu)
        {
            if (ModelState.IsValid)
            {
                _context.Add(chucVu);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(chucVu);
        }

        // GET: Admin/ChucVus/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.ChucVus == null)
            {
                return NotFound();
            }

            var chucVu = await _context.ChucVus.FindAsync(id);
            if (chucVu == null)
            {
                return NotFound();
            }
            return View(chucVu);
        }

        // POST: Admin/ChucVus/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("RoleId,Rol")] ChucVu chucVu)
        {
            if (id != chucVu.RoleId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(chucVu);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ChucVuExists(chucVu.RoleId))
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
            return View(chucVu);
        }

        // GET: Admin/ChucVus/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.ChucVus == null)
            {
                return NotFound();
            }

            var chucVu = await _context.ChucVus
                .FirstOrDefaultAsync(m => m.RoleId == id);
            if (chucVu == null)
            {
                return NotFound();
            }

            return View(chucVu);
        }

        // POST: Admin/ChucVus/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.ChucVus == null)
            {
                return Problem("Entity set 'C2108L_Nhom6Context.ChucVus'  is null.");
            }
            var chucVu = await _context.ChucVus.FindAsync(id);
            if (chucVu != null)
            {
                _context.ChucVus.Remove(chucVu);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ChucVuExists(int id)
        {
          return (_context.ChucVus?.Any(e => e.RoleId == id)).GetValueOrDefault();
        }
    }
}
