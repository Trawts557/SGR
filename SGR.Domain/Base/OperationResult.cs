
namespace SGR.Domain.Base
{
    public class OperationResult
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; } = string.Empty;

        private OperationResult(bool isSuccess, string message)
        {
            IsSuccess = isSuccess;
            Message = message;
        }

        public OperationResult()
        {
        }

        public static OperationResult Success()
        {
            return new OperationResult(true, "Success");
        }

        public static OperationResult Success(string message)
        {
            return new OperationResult(true, message);
        }

        public static OperationResult Failure(string message)
        {
            return new OperationResult(false, message);
        }
    }
}
