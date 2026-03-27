namespace BankingAPI_Task2.Models
{
    public class Account
    {
        public int Id { get; set; }
        public string AccountHolderName { get; set; } = string.Empty;

        // Full account number — never exposed directly in API response
        public string AccountNumber { get; set; } = string.Empty;

        public decimal Balance { get; set; }
        public string UserId { get; set; } = string.Empty;
    }
}
