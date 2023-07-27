using System;
using System.Collections.Generic;

namespace eProject3.Models
{
    public partial class ThanhToan
    {
        public int BillId { get; set; }
        public int? AccountId { get; set; }
        public int? Phone { get; set; }
        public int? DetailId { get; set; }
        public double? Total { get; set; }
        public bool BillType { get; set; }
        public int? ServiceId { get; set; }


        public virtual TaiKhoan? Account { get; set; }
        public virtual ThongTin? Detail { get; set; }
        public virtual DichVu? DichVu { get; set; }
    }
}
