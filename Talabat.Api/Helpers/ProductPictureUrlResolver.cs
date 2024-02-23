using AutoMapper;
using Talabat.Api.DTOS;
using Talabat.Core.Entities;

namespace Talabat.Api.Helpers
{
    public class ProductPictureUrlResolver : IValueResolver<Product, ProductToReturnDto, string>
    {
        private readonly IConfiguration _config;

        public ProductPictureUrlResolver(IConfiguration config)
        {
           _config = config;
        }
        public string Resolve(Product source, ProductToReturnDto destination, string destMember, ResolutionContext context)
        {
            if (!string.IsNullOrEmpty(source.PictureUrl))
            {
                return $"{_config["ApiBaseUrl"]}/{source.PictureUrl}";
            }
           return string.Empty;
        }
    }
}
