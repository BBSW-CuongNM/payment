namespace Common.Models;
public class CommonHashedDataModel<T>
{
    public string? HashedData { get; set; }
    public string? Signature { get; set; }
    public T? Data { get; set; }
}