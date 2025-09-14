using AutoGo.Application.Users.Customers.Command.CreateCustomer;
using AutoGo.Application.Users.Customers.Dtos;
using AutoGo.Domain.Models;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoGo.Application.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CreateCustomerCommand, ApplicationUser>()
                .ForMember(des => des.UserName, src => src.MapFrom(sr => sr.FullName))
                .ForMember(des => des.Email, src => src.MapFrom(sr => sr.Email))
                .ForMember(des => des.PhoneNumber, src => src.MapFrom(sr => sr.PhoneNumber))
                .ForMember(des => des.Address, src => src.MapFrom(sr => sr.Address))
                .ForMember(des => des.Id, src => src.MapFrom(sr => Guid.NewGuid()));

            CreateMap<CreateCustomerCommand, Customer>()
                .ForMember(des => des.DateOfBirth, src => src.MapFrom(sr => sr.DateOfBirth))
                .ForMember(des => des.City, src => src.MapFrom(sr => sr.City))
                .ForMember(des => des.Country, src => src.MapFrom(sr => sr.Country))
                .ForMember(des => des.IsActive, src => src.MapFrom(sr =>true));


            CreateMap<Customer, CustomerDto>()
               .ForMember(des => des.FullName, src => src.MapFrom(sr => sr.user.FullName))
               .ForMember(des => des.Email, src => src.MapFrom(sr => sr.user.Email))
               .ForMember(des => des.Address, src => src.MapFrom(sr => sr.user.Address))
               .ForMember(des => des.PhoneNumber, src => src.MapFrom(sr => sr.user.PhoneNumber))
               .ForMember(des => des.DateOfBirth, src => src.MapFrom(sr => sr.DateOfBirth))
               .ForMember(des => des.City, src => src.MapFrom(sr => sr.City))
               .ForMember(des => des.Country, src => src.MapFrom(sr => sr.Country))
               .ForMember(des => des.IsActive, src => src.MapFrom(sr => sr.IsActive));



        }
    }
}
