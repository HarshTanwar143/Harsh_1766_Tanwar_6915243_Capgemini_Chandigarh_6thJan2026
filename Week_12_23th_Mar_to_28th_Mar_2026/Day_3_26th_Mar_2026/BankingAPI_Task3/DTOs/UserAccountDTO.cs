namespace BankingAPI_Task3.DTOs
{
    /// <summary>
    /// User DTO — limited details only.
    /// Balance and full account number are hidden.
    /// Only returned when JWT role claim = "User"
    /// </summary>
    public class UserAccountDTO
    {
        public string AccountHolderName { get; set; } = string.Empty;
        public string MaskedAccountNumber { get; set; } = string.Empty;  // XXXXXX1234
        // Balance is intentionally NOT here
        // Email is intentionally NOT here
    }
}
