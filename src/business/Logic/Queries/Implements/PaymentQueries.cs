namespace Logic.Queries.Implements;
public class PaymentQueries : IPaymentQueries
{
    private readonly ApplicationContext applicationContext;
    public PaymentQueries(ApplicationContext applicationContext)
    {
        this.applicationContext = applicationContext;
    }
    public Task<Payment> GetByIdAsync(string id)
    {
        var result = applicationContext.Payments.Where(x => x.Id == id).FirstOrDefault();
        return Task.FromResult(result);
    }
    public IQueryable<Payment>? GetAllAsync()
    {
        return applicationContext.Payments;
    }
}

