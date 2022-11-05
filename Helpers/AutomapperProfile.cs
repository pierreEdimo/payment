using AutoMapper;
using payment.Dto;
using payment.model;

namespace payment.Helpers;

public class AutomapperProfile : Profile
{
    public AutomapperProfile()
    {
        CreateMap<Wallet, WalletDto>().ReverseMap();

        CreateMap<CreateWalletDto, Wallet>().ReverseMap();

        CreateMap<ActivityDto, Activity>().ReverseMap();

        CreateMap<CreateActivityDto, Activity>().ReverseMap();
    }
}