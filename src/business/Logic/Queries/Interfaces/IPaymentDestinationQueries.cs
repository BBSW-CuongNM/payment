using Ultils.Model;

namespace Logic.Queries.Interfaces;
public interface IPaymentDestinationQueries
{
    Task<PaymentDestinationDto>? GetByIdAsync(string id);
    IQueryable<PaymentDestinationDto>? GetAllAsync();
    Task<IEnumerable<TreeList<PaymentDestinationDto>>>? GetAllTreeAsync();
}

