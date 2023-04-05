namespace AccountAPI.Response
{
    public class ProductResponse
    {
        public ProductResponse()
        {
            Errors = new List<string>();
        }
        public bool Success { get; set; } = true;
        public string? Message { get; set; }
        public List<string>? Errors { get; set; }
    }
}
