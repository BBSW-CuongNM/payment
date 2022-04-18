namespace Logic.Queries.Interfaces;
public interface IOrderQueries
{
    Task<Order?> GetByServiceOrderIdAsync(string serviceOrderId);
    Task<Order?> GetByIdAsync(string orderId);
    IQueryable<Order> GetAllAsync();
}

