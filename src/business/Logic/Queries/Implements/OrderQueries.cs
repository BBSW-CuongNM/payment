namespace Logic.Queries.Implements;
public class OrderQueries : IOrderQueries
{
    private readonly ApplicationContext _applicationContext;
    public OrderQueries(ApplicationContext applicationContext)
    {
        _applicationContext = applicationContext;
    }

    public Task<Order?> GetByServiceOrderIdAsync(string serviceOrderId)
    {
        var result = new Order();
        result = _applicationContext.Orders.Where(x => x.ServiceOrderId == serviceOrderId).FirstOrDefault();
        return Task.FromResult(result);
    }
    public Task<Order?> GetByIdAsync(string orderId)
    {
        var result = new Order();
        result = _applicationContext.Orders.Where(x => x.Id == orderId).FirstOrDefault();
        return Task.FromResult(result);
    }
    public IQueryable<Order> GetAllAsync()
    {
        return _applicationContext.Orders;
    }
}
