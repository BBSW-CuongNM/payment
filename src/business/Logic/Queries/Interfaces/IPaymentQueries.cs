namespace Logic.Queries.Interfaces;

public interface IPaymentQueries
{
    Task<Payment> GetByIdAsync(string id);
    IQueryable<Payment>? GetAllAsync();
}

