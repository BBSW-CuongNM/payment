
using Ultils.Model;

namespace API.Controllers;
[Route("api/v{version:apiVersion}/payment_destinations")]
[ApiController]
[ApiVersion("1.0")]
[ApiExplorerSettings(GroupName = "v1")]
public class PaymentDestinationsController : ControllerBase
{
    private readonly ILogger<PaymentDestinationsController> logger;
    private readonly IMediator mediator;
    private readonly IPaymentDestinationQueries paymentDestinationQueries;

    public PaymentDestinationsController(ILogger<PaymentDestinationsController> logger, IMediator mediator, IPaymentDestinationQueries paymentDestinationQueries)
    {
        this.logger = logger;
        this.mediator = mediator;
        this.paymentDestinationQueries = paymentDestinationQueries;
    }
    [HttpPost]
    [ProducesResponseType(typeof(CommonResponseModel<PaymentDestination>), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    public async Task<ActionResult<CommonResponseModel<PaymentDestination>>> Create([FromBody] CreatePaymentDestinationCommand paymentDestination)
    {
        if (paymentDestination == null)
            return BadRequest();
        var response = new CommonResponseModel<PaymentDestination>();
        var result = await mediator.Send(paymentDestination);
        return Ok(result.Success ? response.SetData(result.Data).SetResult(result.Success, result.Message ?? String.Empty)
                 : response.SetData(null).SetResult(result.Success, result.Message ?? String.Empty)
            );
    }
    [HttpPut]
    [ProducesResponseType(typeof(CommonResponseModel<PaymentDestination>), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    public async Task<ActionResult<CommonResponseModel<PaymentDestination>>> Update([FromBody] UpdatePaymentDestinationCommand paymentDestination)
    {
        if (paymentDestination == null)
            return BadRequest();
        var response = new CommonResponseModel<PaymentDestination>();
        var result = await mediator.Send(paymentDestination);
        return Ok(result.Success ? response.SetData(result.Data).SetResult(result.Success, result.Message ?? String.Empty)
                  : response.SetData(null).SetResult(result.Success, result.Message ?? String.Empty)
             );
    }
    [HttpDelete]
    [ProducesResponseType(typeof(CommonResponseModel<bool>), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    public async Task<ActionResult<CommonResponseModel<bool>>> Delete([FromBody] DeletePaymentDestinationCommand paymentDestination)
    {
        if (paymentDestination == null)
            return BadRequest();
        var response = new CommonResponseModel<bool>();
        var result = await mediator.Send(paymentDestination);
        return Ok(result.Success ? response.SetData(true).SetResult(result.Success, result.Message ?? String.Empty)
                   : response.SetData(false).SetResult(result.Success, result.Message ?? String.Empty)
              );
    }
    [HttpGet()]
    [ProducesResponseType(typeof(CommonResponseModel<IEnumerable<PaymentDestinationDto>>), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    public async Task<ActionResult<CommonResponseModel<IEnumerable<PaymentDestinationDto>>>> GetAll()
    {
        var response = new CommonResponseModel<IEnumerable<PaymentDestinationDto>>();
        var result =  paymentDestinationQueries.GetAllAsync();
        return Ok(response.SetResult(true, String.Empty).SetData(result));
    }
    [HttpGet("tree")]
    [ProducesResponseType(typeof(CommonResponseModel<IEnumerable<TreeList<PaymentDestinationDto>>>), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    public async Task<ActionResult<CommonResponseModel<IEnumerable<TreeList<PaymentDestinationDto>>>>> GetTreeAll()
    {
        var response = new CommonResponseModel<IEnumerable<TreeList<PaymentDestinationDto>>>();
        var result = await paymentDestinationQueries.GetAllTreeAsync();
        return Ok(response.SetResult(true, String.Empty).SetData(result));
    }
}