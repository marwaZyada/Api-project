using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Talabat.Core.Entities;
using Talabat.Core.Entities.Order_Aggregation;

namespace Talabat.Repository.Data
{
	public static class StoreContextSeed
	{
		public static async Task SeedAsync(StoreContext dbContext)
		{
			if (!dbContext.productBrands.Any())
			{
				var BrandsData = File.ReadAllText("../Talabat.Repository/Data/DataSeed/brands.json");
				var Brands = JsonSerializer.Deserialize<List<ProductBrand>>(BrandsData);
				if (Brands?.Count > 0)
				{
					foreach (var item in Brands)
					await	dbContext.Set<ProductBrand>().AddAsync(item);
				await	dbContext.SaveChangesAsync();
				}
			}
			if (!dbContext.ProductTypes.Any())
			{
				var TypessData = File.ReadAllText("../Talabat.Repository/Data/DataSeed/types.json");
				var Typess = JsonSerializer.Deserialize<List<ProductType>>(TypessData);
				if (Typess?.Count > 0)
				{
					foreach (var item in Typess)
						await dbContext.Set<ProductType>().AddAsync(item);
					await dbContext.SaveChangesAsync();
				}
			}
			if (!dbContext.Products.Any())
            {
				var ProductsData = File.ReadAllText("../Talabat.Repository/Data/DataSeed/products.json");
				var Products = JsonSerializer.Deserialize<List<Product>>(ProductsData);
				if (Products?.Count > 0)
				{
					foreach (var item in Products)
						await dbContext.Set<Product>().AddAsync(item);
					await dbContext.SaveChangesAsync();
				}
			}
			if (!dbContext.DeliveryMethods.Any())
			{
				var methodData = File.ReadAllText("../Talabat.Repository/Data/DataSeed/delivery.json");
				var methods=JsonSerializer.Deserialize<List<DeliveryMethod>>(methodData);
                if (methods?.Count > 0)
                {
                    foreach (var item in methods)
                        await dbContext.Set<DeliveryMethod>().AddAsync(item);
                    await dbContext.SaveChangesAsync();
                }
            }
		}
	}
}
