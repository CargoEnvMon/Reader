namespace CargoEnvMon.Reader.Infrastructure
{
    public class Result
    {
        public Result(bool isSuccess, string message)
        {
            IsSuccess = isSuccess;
            Message = message;
        }

        public bool IsSuccess { get; }
        
        public string Message { get; }
        
        public static Result Error(string error) => new(false, error);

        public static Result Success => new(true, "");
    }
}