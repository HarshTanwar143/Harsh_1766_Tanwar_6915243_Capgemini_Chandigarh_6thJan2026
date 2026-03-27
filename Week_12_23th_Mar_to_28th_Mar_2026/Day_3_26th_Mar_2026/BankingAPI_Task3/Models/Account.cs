namespace BankingAPI_Task3.Models
{
    public class Account
    {
        public int Id { get; set; }
        public string AccountHolderName { get; set; } = string.Empty;
        public string AccountNumber { get; set; } = string.Empty;
        public decimal Balance { get; set; }
        public string Email { get; set; } = string.Empty;
        public string UserId { get; set; } = string.Empty;
    }
}
