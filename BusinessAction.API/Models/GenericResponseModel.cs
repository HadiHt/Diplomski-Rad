namespace BusinessAction.API.Models
{
    public class GenericResponseModel
    {
        public StatusCode Status { get; set; }
        public string Error { get; set; }
    }
    public enum StatusCode
    {
        Success = 0,
        Error = 1
    }
}
