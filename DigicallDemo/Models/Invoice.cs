namespace DigicallDemo.Models
{
    public class Invoice
    {
        public int Id { get; set; }
        public int ClientId { get; set; }
        public string InvoiceNumber { get; set; } = "";
        public decimal Amount { get; set; }
        public string Status { get; set; } = "Pending";
        public DateTime DueDate { get; set; } = DateTime.Now;
    }
}