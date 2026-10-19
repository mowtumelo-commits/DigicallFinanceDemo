namespace DigicallDemo.Models
{
    public class Claim
    {
        public int Id { get; set; }
        public int ClientId { get; set; }  // <--- THIS WAS MISSING - FOREIGN KEY
        public string Description { get; set; } = "";
        public decimal ClaimAmount { get; set; }
        public string Status { get; set; } = "Submitted";
        public DateTime DateSubmitted { get; set; } = DateTime.Now;
    }
}