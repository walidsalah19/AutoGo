using AutoGo.Application.Users.Customers.Command.CreateCustomer;
using AutoGo.Application.Users.Customers.Command.UpdateCustomer;
using AutoGo.Application.Users.Customers.Dtos;
using AutoGo.Domain.Models;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoGo.Application.Users.Dealer.Commands.CreateDealer;
using AutoGo.Application.Users.Dealer.Dtos;

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
                .ForMember(des => des.Id, src => src.MapFrom(sr => Guid.NewGuid()))
                .ForMember(des => des.IsActive, src => src.MapFrom(sr => true));

            CreateMap<CreateCustomerCommand, Customer>()
                .ForMember(des => des.DateOfBirth, src => src.MapFrom(sr => sr.DateOfBirth))
                .ForMember(des => des.City, src => src.MapFrom(sr => sr.City))
                .ForMember(des => des.Country, src => src.MapFrom(sr => sr.Country));

         

            CreateMap<Customer, CustomerDto>()
               .ForMember(des => des.CustomerId, src => src.MapFrom(sr => sr.userId))
               .ForMember(des => des.UserName, src => src.MapFrom(sr => sr.user.UserName))
               .ForMember(des => des.Email, src => src.MapFrom(sr => sr.user.Email))
               .ForMember(des => des.Address, src => src.MapFrom(sr => sr.user.Address))
               .ForMember(des => des.PhoneNumber, src => src.MapFrom(sr => sr.user.PhoneNumber))
               .ForMember(des => des.DateOfBirth, src => src.MapFrom(sr => sr.DateOfBirth))
               .ForMember(des => des.City, src => src.MapFrom(sr => sr.City))
               .ForMember(des => des.Country, src => src.MapFrom(sr => sr.Country))
               .ForMember(des => des.IsActive, src => src.MapFrom(sr => sr.user.IsActive));

            CreateMap<CreateDealerCommand, ApplicationUser>()
                .ForMember(des => des.UserName, src => src.MapFrom(sr => sr.UserName))
                .ForMember(des => des.Email, src => src.MapFrom(sr => sr.Email))
                .ForMember(des => des.PhoneNumber, src => src.MapFrom(sr => sr.PhoneNumber))
                .ForMember(des => des.Address, src => src.MapFrom(sr => sr.Address))
                .ForMember(des => des.Id, src => src.MapFrom(sr => Guid.NewGuid()))
                .ForMember(des => des.IsActive, src => src.MapFrom(sr => true));

            CreateMap<CreateDealerCommand, Dealer>()
                .ForMember(des => des.ShowroomName, src => src.MapFrom(sr => sr.ShowroomName))
                .ForMember(des => des.Latitude, src => src.MapFrom(sr => sr.Latitude))
                .ForMember(des => des.Longitude, src => src.MapFrom(sr => sr.Longitude))
                .ForMember(des => des.WebsiteUrl, src => src.MapFrom(sr => sr.WebsiteUrl))
                .ForMember(des => des.Description, src => src.MapFrom(sr => sr.Description))
                .ForMember(des => des.TaxNumber, src => src.MapFrom(sr => sr.TaxNumber))
                .ForMember(des => des.LicenseNumber, src => src.MapFrom(sr => sr.LicenseNumber))
                .ForMember(des => des.EstablishedYear, src => src.MapFrom(sr => sr.EstablishedYear))
                .ForMember(des => des.TotalVehicles, src => src.MapFrom(sr => 0));
            CreateMap<CreateDealerCommand, DealerDto>()
                .ForMember(des => des.UserName, src => src.MapFrom(sr => sr.UserName))
                .ForMember(des => des.Email, src => src.MapFrom(sr => sr.Email))
                .ForMember(des => des.PhoneNumber, src => src.MapFrom(sr => sr.PhoneNumber))
                .ForMember(des => des.Address, src => src.MapFrom(sr => sr.Address))
                .ForMember(des => des.IsActive, src => src.MapFrom(sr => true))
                .ForMember(des => des.ShowroomName, src => src.MapFrom(sr => sr.ShowroomName))
                .ForMember(des => des.Latitude, src => src.MapFrom(sr => sr.Latitude))
                .ForMember(des => des.Longitude, src => src.MapFrom(sr => sr.Longitude))
                .ForMember(des => des.WebsiteUrl, src => src.MapFrom(sr => sr.WebsiteUrl))
                .ForMember(des => des.Description, src => src.MapFrom(sr => sr.Description))
                .ForMember(des => des.TaxNumber, src => src.MapFrom(sr => sr.TaxNumber))
                .ForMember(des => des.LicenseNumber, src => src.MapFrom(sr => sr.LicenseNumber))
                .ForMember(des => des.EstablishedYear, src => src.MapFrom(sr => sr.EstablishedYear))
                .ForMember(des => des.TotalVehicles, src => src.MapFrom(sr => 0));


            CreateMap<Dealer, DealerDto>()
                .ForMember(des => des.UserName, src => src.MapFrom(sr => sr.User.UserName))
                .ForMember(des => des.Email, src => src.MapFrom(sr => sr.User.Email))
                .ForMember(des => des.PhoneNumber, src => src.MapFrom(sr => sr.User.PhoneNumber))
                .ForMember(des => des.Address, src => src.MapFrom(sr => sr.User.Address))
                ;


        }
    }
}
