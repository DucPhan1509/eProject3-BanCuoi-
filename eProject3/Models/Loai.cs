using System;
using System.Collections.Generic;

namespace eProject3.Models
{
    public partial class Loai
    {
        public Loai()
        {
            DichVus = new HashSet<DichVu>();
        }

        public int CatId { get; set; }
        public string? CatName { get; set; }

        public virtual ICollection<DichVu> DichVus { get; set; }
    }
}
