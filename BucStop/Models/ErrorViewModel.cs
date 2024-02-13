namespace BucStop.Models
{
    //Gets error ID and returns it if needed
    public class ErrorViewModel
    {
        public string? RequestId { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}