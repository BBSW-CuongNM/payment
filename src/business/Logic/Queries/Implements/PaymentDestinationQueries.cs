
using Ultils.Model;

namespace Logic.Queries.Implements;
public class PaymentDestinationQueries : IPaymentDestinationQueries
{
    private readonly ApplicationContext applicationContext;
    private readonly IMapper mapper;

    public PaymentDestinationQueries(ApplicationContext applicationContext, IMapper mapper)
    {
        this.applicationContext = applicationContext;
        this.mapper = mapper;
    }
    public Task<PaymentDestinationDto>? GetByIdAsync(string id)
    {
        var result = new PaymentDestinationDto();
        var data = applicationContext.PaymentDestionations.Where(x => x.Id == id).FirstOrDefault();
        result = mapper.Map<PaymentDestinationDto>(data);
        return Task.FromResult(result);
    }
    public IQueryable<PaymentDestinationDto>? GetAllAsync()
    {
        var data = applicationContext.PaymentDestionations.Select(x => mapper.Map<PaymentDestinationDto>(x));
        return data;

    }
    public Task<IEnumerable<TreeList<PaymentDestinationDto>>>? GetAllTreeAsync()
    {
        var resultDto = new List<PaymentDestinationDto>();
        var data = applicationContext.PaymentDestionations.ToList();
        resultDto = mapper.Map<List<PaymentDestinationDto>>(data);
        var result = resultDto.GenerateTree(item => item.ExternalId, item => item.ParentId,"");
        return Task.FromResult(result);
    }
}
