namespace Common.Models;

public class CommonAuditCommand
{
    public string UserName { get; set; } = string.Empty;
    public CommonAuditCommand SetAudit(string userName)
    {
        UserName = userName;
        return this;
    }
}
