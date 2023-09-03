namespace CoffeeClub_Core_Functions;

public abstract class FunctionBase<T>
{
    public FunctionBase(ILoggerFactory loggerFactory)
    {
        Logger = loggerFactory.CreateLogger<T>();

    }

    protected ILogger<T> Logger { get; }
}
