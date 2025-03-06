namespace InvoiceSystem.Models
{
    public class Invoice
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public decimal Amount { get; set; }
        public decimal PaidAmount { get; set; } = 0;
        public DateTime DueDate { get; set; }
        public string Status { get; set; } = "pending";
    }
}
