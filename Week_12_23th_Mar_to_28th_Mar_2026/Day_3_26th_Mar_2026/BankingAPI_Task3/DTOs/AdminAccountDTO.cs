namespace BankingAPI_Task3.DTOs
{
    /// <summary>
    /// Admin DTO — full account details, nothing hidden.
    /// Only returned when JWT role claim = "Admin"
    /// </summary>
    public class AdminAccountDTO
    {
        public string AccountHolderName { get; set; } = string.Empty;
        public string AccountNumber { get; set; } = string.Empty;   // full number
        public decimal Balance { get; set; }                         // full balance
        public string Email { get; set; } = string.Empty;
    }
}
