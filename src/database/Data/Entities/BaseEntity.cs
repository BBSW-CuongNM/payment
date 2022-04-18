namespace Data.Entities;
public class BaseEntity
{
    public string DataTransactionId { get; set; } = string.Empty;
    public string? CreatedBy { get; set; }
    public string? LastUpdatedBy { get; set; }
    public DateTime? CreatedAt { get; set; }
    public DateTime? LastUpdatedAt { get; set; }
    /// <summary>
    /// Cờ báo xóa, soft deleted
    /// true (1): Đã xóa
    /// false (0): Chưa xóa
    /// </summary>
    public bool IsDeleted { get; set; }
    public void InitWhenCreated()
    {
        CreatedBy = "UserDefault";
        CreatedAt = DateTime.Now;
    }

    public void InitWhenUpdated()
    {
        LastUpdatedBy = "UserDefault";
        LastUpdatedAt = DateTime.Now;
    }
}