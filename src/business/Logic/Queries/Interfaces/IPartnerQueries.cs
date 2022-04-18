namespace Logic.Queries.Interfaces;
public interface IPartnerQueries
{
    Task<Partner?> GetByIdAsync(string orderId);
    IQueryable<Partner>? GetAllAsync();

}

