namespace SGR.Domain.Base
{
    public class OperationResult<T>
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; } = string.Empty;
        public T? Data { get; set; }

        private OperationResult(bool isSuccess, string message, T? data = default)
        {
            IsSuccess = isSuccess;
            Message = message;
            Data = data;
        }

        public OperationResult()
        {
        }

        public static OperationResult<T> Success(T data)
        {
            return new OperationResult<T>(true, "Success", data);
        }

        public static OperationResult<T> Success(string message, T? data = default)
        {
            return new OperationResult<T>(true, message, data);
        }

        public static OperationResult<T> Failure(string message)
        {
            return new OperationResult<T>(false, message);
        }
    }
}
