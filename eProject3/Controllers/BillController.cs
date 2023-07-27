using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Xml.Schema;
using System;
using System.Collections.Generic;
using eProject3.ModelsView;
using eProject3.Models;

namespace eProject3.Controllers
{
    public class BillController : Controller
    {
        public static List<BillViewModel> billList = new List<BillViewModel>();
        private readonly C2108L_Nhom6Context _context;
        public INotyfService _notyfService { get; }

        public BillController(C2108L_Nhom6Context context, INotyfService notyfService)
        {
            _context = context;
            _notyfService = notyfService;
        }

        [HttpPost]
        [Route("/api/bill/new")]
        public IActionResult NewBill(int MobileNumber, string Provider, int Price, bool Prepost)
        {
            // Clear the bill list
            billList.Clear();

            var bill = new BillViewModel { MobileNumber = MobileNumber, Provider = Provider, Price = Price , Prepost = Prepost };
            billList.Add(bill);
           
            return Json(new { success = true });

        }
        [Route("bill.html")]
        public IActionResult Index()
        {
            

            return View(billList);
        }


        [HttpPost]
        [Route("/api/bill/confirm")]
        public async Task<IActionResult> ConfirmBill([Bind("BillId,AccountId,Phone,DetailId,Total")] ThanhToan thanhToan)
        {
            // Add the bills in the list to the database
            foreach (var bill in billList)
            {
                // Add code to save the bill to the database here
                thanhToan.Phone = bill.MobileNumber;
                thanhToan.Total = bill.Price;
                thanhToan.BillType = bill.Prepost;
                _context.Add(thanhToan);
                await _context.SaveChangesAsync();
                _notyfService.Success("Success");

            }

            // Clear the bill list
            billList.Clear();

            // Redirect to the home page
            return RedirectToAction("Index", "Home");
        }


        [HttpPost]
        [Route("/api/bill/cancel")]
        public IActionResult CancelBill()
        {
            // Clear the bill list
            billList.Clear();

            // Redirect to the home page
            return RedirectToAction("Index", "Home");
        }
    }

    
}
