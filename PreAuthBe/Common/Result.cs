namespace PreAuthBe.Common;

public class Result
{
    public bool IsSuccess { get; }
    public string? Error { get; }
    
    protected Result(bool isSuccess, string? error)
    {
        IsSuccess = isSuccess;
        Error = error;
    }
    public static Result Ok() => new(true, null);
    public static Result Fail(string message) => new(false, message);
}
public class Result<T> : Result
{
    public T? Value { get; }

    protected Result(T? value, bool isSuccess, string? error) 
        : base(isSuccess, error)
    {
        Value = value;
    }

    public static Result<T> Ok(T value) => new(value, true, null);
    public static new Result<T> Fail(string message) => new(default, false, message);
}