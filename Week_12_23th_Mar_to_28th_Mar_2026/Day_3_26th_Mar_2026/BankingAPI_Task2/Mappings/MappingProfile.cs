using AutoMapper;
using BankingAPI_Task2.DTOs;
using BankingAPI_Task2.Models;

namespace BankingAPI_Task2.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Account, AccountDTO>()
                .ForMember(
                    dest => dest.MaskedAccountNumber,
                    opt => opt.MapFrom(src => MaskAccountNumber(src.AccountNumber))
                );
        }

        /// <summary>
        /// Core Task 2 Logic:
        /// Masking is done HERE inside AutoMapper — NOT in the controller.
        /// 
        /// Rules:
        ///   - If account number has more than 4 digits → XXXXXX + last 4 digits
        ///   - If account number is 4 digits or less    → return as-is (short number safety)
        ///   - If account number is null/empty          → return "N/A"
        /// 
        /// Examples:
        ///   "9876541234"  →  "XXXXXX1234"
        ///   "1234"        →  "1234"   (too short to mask)
        ///   "123"         →  "123"    (too short to mask)
        ///   ""            →  "N/A"
        /// </summary>
        private static string MaskAccountNumber(string accountNumber)
        {
            if (string.IsNullOrEmpty(accountNumber))
                return "N/A";

            if (accountNumber.Length <= 4)
                return accountNumber; // too short — return safely as-is

            // Mask everything except the last 4 digits
            string lastFour = accountNumber.Substring(accountNumber.Length - 4);
            return "XXXXXX" + lastFour;
        }
    }
}
