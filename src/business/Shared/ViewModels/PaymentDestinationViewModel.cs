namespace Shared.ViewModels;
public class PaymentDestinationViewModel
{
    public string Id { get; set; } 
    public string? ExternalId { get; set; }
    public string? Group { get; set; } 
    public string? ParentId { get; set; } 
    public string? Name { get; set; }
    public string? OtherName { get; set; } 
    public string? Image { get; set; } 
    public int SortIndex { get; set; }
    public string? PartnerId { get; set; }
}
