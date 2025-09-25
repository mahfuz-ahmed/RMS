using AutoMapper;
using FinancialManagementSystem.Core;

namespace FinancialManagementSystem.Application
{
    public class ChartOfAccountProfile : Profile
    {
        public ChartOfAccountProfile()
        {
            // Mapping for insert
            CreateMap<ChartOfAccountCreateDto, ChartOfAccount>()
                .ForMember(dest => dest.ParentAccountId, opt => opt.MapFrom(src => src.ParentId));
            
            // Mapping for update
            CreateMap<ChartOfAccountUpdateDto, ChartOfAccount>()
                .ForMember(dest => dest.ParentAccountId, opt => opt.MapFrom(src => src.ParentId))
                .ForAllMembers(opt => opt.Condition((src, dest, srcMember) => srcMember != null));

            // Mapping for get
            CreateMap<ChartOfAccount, ChartOfAccountReadDto>()
                .ForMember(dest => dest.ParentId,
                    opt => opt.MapFrom(src => src.ParentAccountId))
                .ForMember(dest => dest.ParentAccountName,
                    opt => opt.MapFrom(src => src.ParentAccount != null ? src.ParentAccount.AccountName : null));
        }
    }
}
