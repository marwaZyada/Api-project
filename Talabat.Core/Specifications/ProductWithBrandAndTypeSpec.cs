using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;

namespace Talabat.Core.Specifications
{
   public class ProductWithBrandAndTypeSpec:BaseSpecification<Product>
    {
        public ProductWithBrandAndTypeSpec(ProductSpecParam productSpec):base(p=>
        (string.IsNullOrEmpty(productSpec.Search) || p.Name.ToLower().Contains(productSpec.Search)) &&
        (!productSpec.BrandId.HasValue ||p.ProductBrandId==productSpec.BrandId)&&
        (!productSpec.TypeId.HasValue ||p.ProductTypeId==productSpec.TypeId)
      
          
            )
        {
            Includes.Add(p => p.ProductBrand);
            Includes.Add(p => p.ProductType);
            if (!string.IsNullOrEmpty(productSpec.Sort))
            {
                switch (productSpec.Sort)
                {
                    case "PriceAsc":
                        ApplyOrderBy(p => p.Price);
                        break;
                    case "PriceDesc":
                        ApplyOrderByDescending(p => p.Price);
                        break;
                    default:
                        ApplyOrderBy(p => p.Name);
                        break;
                }
            }
            ApplyPagination(productSpec.PageSize*(productSpec.PageIndex-1),productSpec.PageSize);

        }
        public ProductWithBrandAndTypeSpec(int id):base(p=>p.Id==id)
        {
            Includes.Add(p => p.ProductBrand);
            Includes.Add(p => p.ProductType);
        }
    }
}
