using System;
using System.Collections.Generic;

namespace eProject3.Models
{
    public partial class ChucVu
    {
        public ChucVu()
        {
            TaiKhoans = new HashSet<TaiKhoan>();
        }

        public int RoleId { get; set; }
        public string? Rol { get; set; }

        public virtual ICollection<TaiKhoan> TaiKhoans { get; set; }
    }
}
