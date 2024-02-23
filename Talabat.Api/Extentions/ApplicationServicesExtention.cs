using Microsoft.AspNetCore.Mvc;
using Talabat.Api.Helpers;
using Talabat.Core.Repository;
using Talabat.Repository.Data;
using Talabat.Repository;
using Microsoft.EntityFrameworkCore;
using Talabat.Api.Errors;
using Talabat.Core.Repositories;
using Talabat.Core;
using Talabat.Core.Services;
using Talabat.Services;

namespace Talabat.Api.Extentions
{
    public static class ApplicationServicesExtention
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection Services)
        {
         
            //Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            Services.AddScoped<IUnitOfWork, UnitOfWork>();
            Services.AddScoped<IOrderService,OrderService>();  
           Services.AddAutoMapper(typeof(MappingProfiles));
            Services.AddScoped(typeof(IBasketRepository), typeof(BasketRepository));
            Services.Configure<ApiBehaviorOptions>(Options =>
            {
                Options.InvalidModelStateResponseFactory = (actionContext) =>
                {
                    var Error = actionContext.ModelState.Where(d => d.Value.Errors.Count() > 0)
                    .SelectMany(E => E.Value.Errors).Select(e => e.ErrorMessage).ToList();
                    var validationErrorResponse = new ApiValidationResponse()
                    {
                        Errors = Error
                    };
                    return new BadRequestObjectResult(validationErrorResponse);
                };
            });
            
            return Services;
        }
    }
}
