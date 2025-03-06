namespace InvoiceSystem.Models
{
    public class OverdueRequest
    {
        public decimal LateFee { get; set; }
        public int OverdueDays { get; set; }
    }
}
