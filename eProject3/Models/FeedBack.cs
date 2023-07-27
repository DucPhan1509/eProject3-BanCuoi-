using System;
using System.Collections.Generic;

namespace eProject3.Models
{
    public partial class FeedBack
    {
        public int FeedBack1 { get; set; }
        public int? AccountId { get; set; }
        public string? Contents { get; set; }

        public virtual TaiKhoan? Account { get; set; }
    }
}
