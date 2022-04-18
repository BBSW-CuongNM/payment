namespace Common.Models;
public class CommonInvalidResponseModel<T> : CommonResponseModel<T>
{
    public List<CommonErrorResponseModel> Errors { get; set; } = new List<CommonErrorResponseModel>();
}

public class CommonErrorResponseModel
{
    public string Key { get; set; } = string.Empty;
    public string Message { get; set; } = string.Empty;
}
