namespace Common.Models;
public class CommonPagingResponseModel<T> : CommonResponseModel<T>
{
    public int PageIndex { get; set; }
    public int TotalCount { get; set; }
    public int PageSize { get; set; }
    public int TotalPage { get; set; }
}
