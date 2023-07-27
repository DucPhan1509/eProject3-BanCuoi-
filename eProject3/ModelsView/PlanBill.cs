using eProject3.Models;

namespace eProject3.ModelsView
{
    public class PlanBill
    {
        public DichVu dichVu { get; set; }
        public int Phone { get; set; }
        public double Total => dichVu.Price.Value;
    }
}
