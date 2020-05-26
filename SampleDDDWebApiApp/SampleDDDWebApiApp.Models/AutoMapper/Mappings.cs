using AutoMapper;
using SampleDDDWebApiApp.Models.Entities;
using SampleDDDWebApiApp.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace SampleDDDWebApiApp.Models.AutoMapper
{
    public static class Mappings
    {
        public static MapperConfiguration CreateConfiguration()
        {
            return new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<User, UserView>()
                .ForMember(dest => dest.User, opt => opt.MapFrom(src => src.UserName))
                .ReverseMap();


                cfg.CreateMap<UserMoney, BalanceView>()
                .ForMember(dest => dest.Amount, opt => opt.MapFrom(src => src.Amount))
                .ForMember(dest => dest.Currency, opt => opt.MapFrom(src => src.Currency.Name))
                .ReverseMap();
            });
        }
    }
}
