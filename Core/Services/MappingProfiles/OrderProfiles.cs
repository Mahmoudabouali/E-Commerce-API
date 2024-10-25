using AutoMapper;
using Domain.Entities.OrderEntities;
using Shared.OrderModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserAddress = Domain.Entities.Identity.Address;

namespace Services.MappingProfiles
{
    public class OrderProfiles : Profile
    {
        public OrderProfiles() 
        {
            CreateMap<Address, AddressDTO>().ReverseMap();
            CreateMap<OrderItem, OrderItemDTO>()
                .ForMember(d => d.ProductId
                , option => option.MapFrom(s => s.Product.ProductId))
                .ForMember(d => d.PictureUrl
                , option => option.MapFrom(s => s.Product.PictureUrl))
                .ForMember(d => d.ProductName
                , option => option.MapFrom(s => s.Product.ProductName));


            CreateMap<Order, OrderResult>()
                .ForMember(d => d.paymentStatus,
                option => option.MapFrom(s => s.paymentStatus.ToString()))
                .ForMember(d => d.deliveryMethod,
                option => option.MapFrom(s => s.deliveryMethod.ShortName))
                .ForMember(d => d.Total,
                option => option.MapFrom(s => s.Subtotal + s.deliveryMethod.Price));

            CreateMap<DeliveryMethod, DeliveryMethodResult>();


            CreateMap<AddressDTO, UserAddress>().ReverseMap();
        }
    }
}
