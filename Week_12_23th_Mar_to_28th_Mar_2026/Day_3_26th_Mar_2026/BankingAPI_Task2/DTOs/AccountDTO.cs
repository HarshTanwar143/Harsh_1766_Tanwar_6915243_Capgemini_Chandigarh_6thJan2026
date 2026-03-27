namespace BankingAPI_Task2.DTOs
{
    /// <summary>
    /// Task 2 DTO — only exposes the masked account number.
    /// Masking is done inside AutoMapper, NOT in the controller.
    /// </summary>
    public class AccountDTO
    {
        public string AccountHolderName { get; set; } = string.Empty;

        // Will be formatted as XXXXXX1234 by AutoMapper
        public string MaskedAccountNumber { get; set; } = string.Empty;
    }
}
