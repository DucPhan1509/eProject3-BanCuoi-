using System;
using System.Collections.Generic;

namespace eProject3.Models
{
    public partial class Provider
    {
        public Provider()
        {
            DichVus = new HashSet<DichVu>();
        }

        public int ProviderId { get; set; }
        public string? ProviderName { get; set; }

        public virtual ICollection<DichVu> DichVus { get; set; }
    }
}
