namespace Logic.Queries.Implements;
public class PartnerQueries : IPartnerQueries
{
    private readonly ApplicationContext _applicationContext;
    public PartnerQueries(ApplicationContext applicationContext)
    {
        _applicationContext = applicationContext;
    }
    public Task<Partner?> GetByIdAsync(string id)
    {
        var result = new Partner();
        result = _applicationContext.Partners.Where(x => x.Id == id).FirstOrDefault();
        return Task.FromResult(result);
    }
    public IQueryable<Partner>? GetAllAsync()
    {
        return _applicationContext.Partners;
    }
}

