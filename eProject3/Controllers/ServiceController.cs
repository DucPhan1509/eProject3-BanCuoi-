
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Http;

using System.Xml.Schema;
using System;
using System.Collections.Generic;
using eProject3.ModelsView;
using eProject3.Models;
using AspNetCoreHero.ToastNotification.Notyf;
using System.Web.Helpers;

namespace eProject3.Controllers
{
    public class ServiceController : Controller
    {
        private readonly C2108L_Nhom6Context _context;
        public static List<PlanBill> PlanList = new List<PlanBill>();
        public INotyfService _notyfService { get; }

        public ServiceController(C2108L_Nhom6Context context, INotyfService notyfService)
        {
            _context = context;
            _notyfService = notyfService;
        }

        

        [Route("planlist.html")]
        public async Task<IActionResult> IndexAsync()
        {
            var c2108L_Nhom6Context = _context.DichVus.Include(d => d.Cat).Include(d => d.Provider);
            return View(await c2108L_Nhom6Context.ToListAsync());
        }


        [HttpPost]
        [Route("/api/bill/plan")]
        public IActionResult NewPlanBill(int ServiceId)
        {
            PlanList.Clear();
            // Find the DichVu object with the specified ServiceId
            var dichVu = _context.DichVus
                .Include(d => d.Cat)
                .Include(d => d.Provider)
                .FirstOrDefault(d => d.ServiceId == ServiceId);


            if (dichVu == null)
            {
                // Return an error message if the DichVu object is not found
                return Json(new { success = false, message = "DichVu not found" });
            }

            // Add the PlanBill object to the PlanList
            PlanList.Add(new PlanBill { dichVu = dichVu });

            return Json(new { success = true });

        }

        [Route("planbill.html")]
        public IActionResult PlanRecharge()
        {


            return View(PlanList);
        }


        [HttpPost]
        [Route("/api/plan/confirm")]
        public async Task<IActionResult> ConfirmBill([Bind("BillId,AccountId,Phone,DetailId,BillType,ServiceId,Total")] ThanhToan thanhToan)
        {
            // Add the bills in the list to the database
            foreach (var planbill in PlanList)
            {
                // Add code to save the bill to the database here
                thanhToan.Phone = int.Parse(Request.Form["Phone"]);
                thanhToan.Total = planbill.Total;
                thanhToan.ServiceId = planbill.dichVu.ServiceId;
                thanhToan.BillType = false;
                _context.Add(thanhToan);
                await _context.SaveChangesAsync();
                _notyfService.Success("Success");

            }

            // Clear the bill list
            PlanList.Clear();

            // Redirect to the home page
            return RedirectToAction("Index", "Home");
        }


        [HttpPost]
        [Route("/api/plan/cancel")]
        public IActionResult CancelBill()
        {
            // Clear the bill list
            PlanList.Clear();

            // Redirect to the home page
            return RedirectToAction("Index", "Home");
        }
    }
}

