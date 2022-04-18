namespace Common.Models;
public class CommonCommandResult
{
    public CommonCommandResult SetResult(bool success, string message)
    {
        this.Success = success;
        this.Message = message;
        return this;
    }
    public bool Success { get; set; }
    public string? Message { get; set; }
}
