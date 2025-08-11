using JetBrains.Annotations;
using Recommerce.Infrastructure.Exceptions;

namespace Recommerce.Infrastructure.Rop;

[PublicAPI]
public class Result
{
    public bool IsSuccess { get; }
    public bool IsFailed => !IsSuccess;
    public IEnumerable<string> Messages { get; }
    public BaseException Exception { get; set; }

    public Result(bool isSuccess, BaseException exception, params string[] messages)
    {
        IsSuccess = isSuccess;
        Exception = exception;
        Messages = messages;
    }
    
    public static Result Success()
    {
        return new Result(true, default!);
    }

    public static Result Success(params string[] messages)
    {
        return new Result(true, null!, messages);
    }

    public static Result Failed(params string[] messages)
    {
        return new Result(false, null!, messages);
    }

    public static Result Failed(BaseException exception)
    {
        return new Result(false, exception);
    }

    public static Result Failed(BaseException exception, params string[] messages)
    {
        return new Result(false, exception, messages);
    }
    
    // wrap Custom Exceptions into Result
    public static implicit operator Result(BaseException exception) =>
        new(false,exception);
}

[PublicAPI]
public class Result<TSuccess> : Result
{
    public TSuccess Data { get; }

    private Result(bool isSuccess, TSuccess value, BaseException exception, params string[] messages)
        : base(isSuccess, exception, messages)
    {
        Data = value;
    }

    public static Result<TSuccess> Success(TSuccess value)
    {
        return new Result<TSuccess>(true, value, null!);
    }

    public static Result<TSuccess> Success(TSuccess value, params string[] messages)
    {
        return new Result<TSuccess>(true, value, null!, messages);
    }

    public new static Result<TSuccess> Failed(params string[] messages)
    {
        return new Result<TSuccess>(false, default!, null!, messages);
    }

    public new static Result<TSuccess> Failed(BaseException exception)
    {
        return new Result<TSuccess>(false, default!, exception);
    }

    public new static Result<TSuccess> Failed(BaseException exception, params string[] messages)
    {
        return new Result<TSuccess>(false, default!, exception, messages);
    }

    
    // Implicit Operators
    
    // convert success TSuccess to Result<TSuccess> 
    public static implicit operator Result<TSuccess>(TSuccess value) => new(true, value, null!);

    // wrap Custom Exceptions into Result
    public static implicit operator Result<TSuccess>(BaseException exception) =>
        new(false, default!, exception);
}