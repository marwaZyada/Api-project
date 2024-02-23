namespace Talabat.Api.Errors
{
    public class ApiExceptionResponse:ApiResponse
    {
        public string? Details { get; set; }
        public ApiExceptionResponse(int SC, string? M = null,string? details=null) : base(SC, M)
        {
            Details= details;
        }

        
    }
}
