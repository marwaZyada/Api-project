namespace Talabat.Api.Errors
{
    public class ApiResponse
    {
        public int StatusCode { get; set; }
        public string? Message { get; set; }

        public ApiResponse(int SC,string? M=null)
        {
            StatusCode=SC;
            Message=M??GetMessageError(SC);
            
        }

        private string? GetMessageError(int StatusCode)
        {
            return StatusCode switch
            {
                404 => "Not Found",
                401 => "Not Authorized",
                400 => "Bad Request",
                500 => "Server Error",
                _ => null
            };
        }
    }
}
