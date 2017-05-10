
namespace Domain.Validation
{
    public class ValidationError
    {
        public string Message { get; set; }
        public ValidationError(string message)
        {
            this.Message = message;
        }
    }
}
