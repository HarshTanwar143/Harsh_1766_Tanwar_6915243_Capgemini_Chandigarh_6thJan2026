using AutoMapper;
using BankingAPI.DTOs;
using BankingAPI.Models;

namespace BankingAPI.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Account, AccountDTO>()
                .ForMember(dest => dest.MaskedAccountNumber, opt =>
                    opt.MapFrom(src =>
                        src.AccountNumber.Length >= 4
                            ? "XXXXXX" + src.AccountNumber.Substring(src.AccountNumber.Length - 4)
                            : src.AccountNumber));
        }
    }
}
