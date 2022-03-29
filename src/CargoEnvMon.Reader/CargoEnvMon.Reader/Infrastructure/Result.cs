using System;

namespace CargoEnvMon.Reader.Infrastructure
{
    public class Result
    {
        public Result(bool isSuccess, string message, Exception exception)
        {
            IsSuccess = isSuccess;
            Message = message;
            Exception = exception;
        }

        public bool IsSuccess { get; }
        
        public string Message { get; }
        
        public Exception Exception { get; }

        public static Result Error(string error) => new(false, error, null);

        public static Result Success => new(false, null, null);
    }
}