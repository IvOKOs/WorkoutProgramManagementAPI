namespace WorkoutProgramManagementAPI.Shared.Result
{
    public class Result
    {
        protected Result(bool isSuccess, Error error)
        {
            if(isSuccess && error != Error.None ||
                !isSuccess && error == Error.None)
            {
                throw new ArgumentException($"Invalid error. {nameof(error)}.");
            }

            IsSuccess = isSuccess;
            Error = error;
        }

        public bool IsSuccess { get; init; }
        public bool IsFailure => !IsSuccess;
        public Error Error {  get; init; }

        public static Result Success() => new Result(true, Error.None);
        public static Result Failure(Error error) => new Result(false, error);
    }

    public class Result<T> : Result
    {
        private Result(T value) : base(true, Error.None)
        {
            Value = value;
        }

        private Result(Error error) : base(false, error)
        {
            Value = default;
        }

        public T? Value { get; private set; }

        public static Result<T> Success(T value) => new Result<T>(value);
        public new static Result<T> Failure(Error error) => new Result<T>(error);
    }
}
