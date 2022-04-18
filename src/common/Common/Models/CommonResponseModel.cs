namespace Common.Models;
public class CommonResponseModel<T>
{
    public bool Success { get; set; }
    public string? Message { get; set; }
    public T? Data { get; set; }

    public virtual CommonResponseModel<T> SetResult(bool success, string messsage)
    {
        Success = success;
        Message = messsage;
        return this;
    }

    public virtual CommonResponseModel<T> SetData(T? data)
    {
        Data = data;
        return this;
    }
}