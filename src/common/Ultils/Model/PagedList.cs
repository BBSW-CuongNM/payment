﻿namespace Ultils.Model;
public class PagedList<T>
{
    public int PageIndex { get; set; }
    public int TotalCount { get; set; }
    public int PageSize { get; set; }
    public int TotalPage { get; set; }
    public List<T> Items { get; set; } = new List<T>();
}
