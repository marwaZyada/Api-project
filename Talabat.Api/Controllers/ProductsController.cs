using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Talabat.Api.DTOS;
using Talabat.Api.Errors;
using Talabat.Api.Helpers;
using Talabat.Core;
using Talabat.Core.Entities;
using Talabat.Core.Repository;
using Talabat.Core.Specifications;

namespace Talabat.Api.Controllers
{

	public class ProductsController :ApiBaseController
	{
		
private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
      

        public ProductsController(IUnitOfWork unitOfWork,IMapper mapper)

        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            
        }
		
		[HttpGet]
		public async Task<ActionResult<Pagination<ProductToReturnDto>>> GetProducts([FromQuery]ProductSpecParam productSpec)
		{
			var Spec = new ProductWithBrandAndTypeSpec(productSpec);
			var Products=await _unitOfWork.Repository<Product>().GetAllWithSpecAsync(Spec);
			var data=_mapper.Map<IReadOnlyList<Product>, IReadOnlyList<ProductToReturnDto>>(Products);
			var CountSpec=new ProductWithCountSpec(productSpec);
			var count = await _unitOfWork.Repository<Product>().GetCountWithSpecAsync(CountSpec);
			return Ok(new Pagination<ProductToReturnDto>(productSpec.PageSize,productSpec.PageIndex,data,count));
		}
		[HttpGet("{id}")]
		[ProducesResponseType(typeof(ProductToReturnDto), StatusCodes.Status200OK)]
		[ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
		public async Task<ActionResult<Product>> GetProduct(int id )
		{
            var Spec = new ProductWithBrandAndTypeSpec(id);
            var product = await _unitOfWork.Repository<Product>().GetByIdWithSpecAsync(Spec);
			if (product is null) return NotFound(new ApiResponse(404));
            var MappedProduct = _mapper.Map<Product,ProductToReturnDto>(product);
            return Ok(MappedProduct);
        }
		[HttpGet("brands")]
		public async Task<ActionResult<IReadOnlyList<ProductBrand>>> GetAllBrands()
		{
			var Brands= await _unitOfWork.Repository<ProductBrand>().GetAllAsync();
			return Ok(Brands);
		}
        [HttpGet("types")]
        public async Task<ActionResult<IReadOnlyList<ProductType>>> GetAllTypes()
        {
            var Types = await _unitOfWork.Repository<ProductType>().GetAllAsync();
            return Ok(Types);
        }


    }
}
