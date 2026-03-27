using AutoMapper;
using BankingAPI_Task3.DTOs;
using BankingAPI_Task3.Models;

namespace BankingAPI_Task3.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Admin mapping — all fields passed through directly, nothing hidden
            CreateMap<Account, AdminAccountDTO>();

            // User mapping — mask account number, exclude balance & email
            CreateMap<Account, UserAccountDTO>()
                .ForMember(
                    dest => dest.MaskedAccountNumber,
                    opt => opt.MapFrom(src =>
                        src.AccountNumber.Length > 4
                            ? "XXXXXX" + src.AccountNumber.Substring(src.AccountNumber.Length - 4)
                            : src.AccountNumber)
                );
        }
    }
}
