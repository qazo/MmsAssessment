namespace MmsTodo.Core;

public class Result(string message, bool isSuccessful)
{
    public string Message { get; set; } = message;
    public bool IsSuccessful { get; set; } = isSuccessful;

    public static Result Success(string message = "Success")
        => new(message, true);

    public static Result Fail(string message)
        => new(message, false);
    
    public static Result Fail(Exception ex)
        => new(ex.ToString(), false);
}

public class Result<T>(string message, bool isSuccessful, T? value = default) : Result(message, isSuccessful)
{
    public T? Value { get; } = value;
    
    public static Result<T> Success(T value, string message = "Success")
        => new(message, true, value);

    public new static Result<T> Fail(string message)
        => new(message, false);
    
    public new static Result<T> Fail(Exception ex)
        => new(ex.ToString(), false);
}