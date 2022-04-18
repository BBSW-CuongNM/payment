namespace Common.Models;
public class CommonCommandResultHasData<T> : CommonCommandResult
{
    public CommonCommandResultHasData<T> SetData(T data)
    {
        this.Data = data;
        return this;
    }
    public T? Data { get; set; }
}
