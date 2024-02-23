using AutoMapper;
using Talabat.Api.DTOS;
using Talabat.Core.Entities;
//using Talabat.Core.Entities.Identity;
using Talabat.Core.Entities.Order_Aggregation;

namespace Talabat.Api.Helpers
{
    public class MappingProfiles:Profile
    {
        public MappingProfiles()
        {
            CreateMap<Product, ProductToReturnDto>()
                .ForMember(d => d.ProductBrand, o => o.MapFrom(s => s.ProductBrand.Name))
                .ForMember(d => d.ProductType, o => o.MapFrom(s => s.ProductType.Name))
                .ForMember(d=>d.PictureUrl,o=>o.MapFrom<ProductPictureUrlResolver>());
            CreateMap< Talabat.Core.Entities.Identity.Address, AddressDto >().ReverseMap();
            CreateMap< AddressDto, Talabat.Core.Entities.Order_Aggregation.Address>();
            CreateMap<OrderDto, Order>();
            CreateMap<CustomerBasketDto,CustomerBasket>();
            CreateMap<BasketItemDto,BasketItem>();
        }
    }
}
