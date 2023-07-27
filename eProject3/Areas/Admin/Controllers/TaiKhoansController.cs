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
    public class TaiKhoansController : Controller
    {
        private readonly C2108L_Nhom6Context _context;
        public INotyfService _notyfService { get; }
        public TaiKhoansController(C2108L_Nhom6Context context, INotyfService notyfService)
        {
            _context = context;
            _notyfService = notyfService;
        }

        // GET: Admin/TaiKhoans
        public async Task<IActionResult> Index()
        {
            ViewData["QuyenTruyCap"] = new SelectList(_context.ChucVus, "RoleId", "Rol");

            List<SelectListItem> Disturb = new List<SelectListItem>();
            Disturb.Add(new SelectListItem() { Text = "On", Value = "1" });
            Disturb.Add(new SelectListItem() { Text = "Off", Value = "0" });
            ViewData["Disturb"] = Disturb;

            List<SelectListItem> CallerTunes = new List<SelectListItem>();
            CallerTunes.Add(new SelectListItem() { Text = "Active", Value = "1" });
            CallerTunes.Add(new SelectListItem() { Text = "Block", Value = "0" });
            ViewData["CallerTunes"] = CallerTunes;

            var DBContext = _context.TaiKhoans.Include(a => a.Role);
            return View(await DBContext.ToListAsync());
            //var c2108L_Nhom6Context = _context.TaiKhoans.Include(t => t.Role);
            //return View(await c2108L_Nhom6Context.ToListAsync());
        }

        // GET: Admin/TaiKhoans/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.TaiKhoans == null)
            {
                return NotFound();
            }

            var taiKhoan = await _context.TaiKhoans
                .Include(t => t.Role)
                .FirstOrDefaultAsync(m => m.AccountId == id);
            if (taiKhoan == null)
            {
                return NotFound();
            }

            return View(taiKhoan);
        }

        // GET: Admin/TaiKhoans/Create
        public IActionResult Create()
        {
            ViewData["RoleId"] = new SelectList(_context.ChucVus, "RoleId", "RoleId");
            return View();
        }

        // POST: Admin/TaiKhoans/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AccountId,Phone,Email,Password,DoNotDisturb,CallerTunes,RoleId")] TaiKhoan taiKhoan)
        {
            if (ModelState.IsValid)
            {
                _context.Add(taiKhoan);
                await _context.SaveChangesAsync();
                _notyfService.Success("Success");
                return RedirectToAction(nameof(Index));
            }
            ViewData["RoleId"] = new SelectList(_context.ChucVus, "RoleId", "RoleId", taiKhoan.RoleId);
            return View(taiKhoan);
        }

        // GET: Admin/TaiKhoans/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.TaiKhoans == null)
            {
                return NotFound();
            }

            var taiKhoan = await _context.TaiKhoans.FindAsync(id);
            if (taiKhoan == null)
            {
                return NotFound();
            }
            ViewData["RoleId"] = new SelectList(_context.ChucVus, "RoleId", "RoleId", taiKhoan.RoleId);
            return View(taiKhoan);
        }

        // POST: Admin/TaiKhoans/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("AccountId,Phone,Email,Password,DoNotDisturb,CallerTunes,RoleId")] TaiKhoan taiKhoan)
        {
            if (id != taiKhoan.AccountId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(taiKhoan);
                    _notyfService.Success("Success");
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TaiKhoanExists(taiKhoan.AccountId))
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
            ViewData["RoleId"] = new SelectList(_context.ChucVus, "RoleId", "RoleId", taiKhoan.RoleId);
            return View(taiKhoan);
        }

        // GET: Admin/TaiKhoans/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.TaiKhoans == null)
            {
                return NotFound();
            }

            var taiKhoan = await _context.TaiKhoans
                .Include(t => t.Role)
                .FirstOrDefaultAsync(m => m.AccountId == id);
            if (taiKhoan == null)
            {
                return NotFound();
            }

            return View(taiKhoan);
        }

        // POST: Admin/TaiKhoans/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.TaiKhoans == null)
            {
                return Problem("Entity set 'C2108L_Nhom6Context.TaiKhoans'  is null.");
            }
            var taiKhoan = await _context.TaiKhoans.FindAsync(id);
            if (taiKhoan != null)
            {
                _context.TaiKhoans.Remove(taiKhoan);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TaiKhoanExists(int id)
        {
          return (_context.TaiKhoans?.Any(e => e.AccountId == id)).GetValueOrDefault();
        }
    }
}
