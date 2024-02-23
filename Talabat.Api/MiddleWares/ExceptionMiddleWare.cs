using Microsoft.Extensions.Hosting;
using System.Net;
using System.Text.Json;
using Talabat.Api.Errors;

namespace Talabat.Api.MiddleWares
{
    public class ExceptionMiddleWare
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleWare> _logger;
        private readonly IHostEnvironment _env;

        public ExceptionMiddleWare(RequestDelegate Next,ILogger<ExceptionMiddleWare> logger,IHostEnvironment env)
        {
            _next = Next;
            _logger = logger;
            _env = env;
        }
        public async Task InvokeAsync(HttpContext context)
        {

            try
            {
                await _next.Invoke(context);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex.Message);
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                
                    var response = _env.IsDevelopment()? new ApiExceptionResponse(500,ex.Message,ex.StackTrace.ToString()): new ApiExceptionResponse(500);
                  
             
             
              
                var options = new JsonSerializerOptions()
                {
                   PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                };
                var jsonResponse = JsonSerializer.Serialize(response,options);
                context.Response.WriteAsync(jsonResponse);
            }
        }
    }
}
