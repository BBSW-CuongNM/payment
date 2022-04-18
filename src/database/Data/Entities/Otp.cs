namespace Data.Entities;
public class Otp : BaseEntity
{
    public long? Id { get; set; }
    public string? Sessions { get; set; }
    public string? Value { get; set; }
    public string? Email { get; set; }
    public string? Phone { get; set; }
    public bool IsSuccess { get; set; }
    public bool IsDelete { get; set; }
    public DateTime TimeOut { get; set; }
}

