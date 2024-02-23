using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;

namespace Talabat.Core.Specifications
{
    public class ProductWithCountSpec:BaseSpecification<Product>
    {
        public ProductWithCountSpec(ProductSpecParam productSpec) : base(p =>
        (string.IsNullOrEmpty(productSpec.Search) || p.Name.ToLower().Contains(productSpec.Search)) &&
      (!productSpec.BrandId.HasValue || p.ProductBrandId == productSpec.BrandId) &&
      (!productSpec.TypeId.HasValue || p.ProductTypeId == productSpec.TypeId)


          )
        { }
    }
}
