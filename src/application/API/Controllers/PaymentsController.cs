
using Shared.Models;

namespace API.Controllers;

[Route("api/v{version:apiVersion}/payments")]
[ApiController]
[ApiVersion("1.0")]
[ApiExplorerSettings(GroupName = "v1")]
public class PaymentsController : ControllerBase
{
    private readonly ILogger<PaymentsController> logger;
    private readonly IMediator mediator;
    private readonly IPaymentQueries paymentQueries;

    public PaymentsController(ILogger<PaymentsController> logger, IMediator mediator, IPaymentQueries paymentQueries)
    {
        this.logger = logger;
        this.mediator = mediator;
        this.paymentQueries = paymentQueries;
    }

    [HttpPost]
    [ProducesResponseType(typeof(CommonResponseModel<PaymentResponeModel>), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    public async Task<ActionResult<CommonResponseModel<PaymentDto>>> Create([FromBody] CreatePaymentCommand payment)
    {
        if (payment == null)
            return BadRequest();
        var response = new CommonResponseModel<PaymentResponeModel>();
        var result = await mediator.Send(payment);
        return Ok(result.Success ? response.SetData(result.Data).SetResult(result.Success, result.Message ?? String.Empty)
                 : response.SetData(null).SetResult(result.Success, result.Message ?? String.Empty)
            );
    }
    [HttpPost("transfer")]
    [ProducesResponseType(typeof(CommonResponseModel<PaymentResponeModel>), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    public async Task<ActionResult<CommonResponseModel<PaymentDto>>> CreateTransfer([FromBody] CreatePaymentTransferCommand payment)
    {
        if (payment == null)
            return BadRequest();
        var response = new CommonResponseModel<PaymentResponeModel>();
        var result = await mediator.Send(payment);
        return Ok(result.Success ? response.SetData(result.Data).SetResult(result.Success, result.Message ?? String.Empty)
                 : response.SetData(null).SetResult(result.Success, result.Message ?? String.Empty)
            );
    }
    [HttpGet()]
    [ProducesResponseType(typeof(CommonResponseModel<IEnumerable<Payment>>), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    public async Task<ActionResult<CommonResponseModel<IEnumerable<Payment>>>> GetAll()
    {
        var response = new CommonResponseModel<IEnumerable<Payment>>();
        var result = paymentQueries.GetAllAsync();
        return Ok(response.SetResult(true, String.Empty).SetData(result));
    }

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(CommonResponseModel<Payment?>), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    public async Task<ActionResult<CommonResponseModel<Payment?>>> GetById(string id)
    {
        if (string.IsNullOrEmpty(id))
        {
            return BadRequest();
        }
        var response = new CommonResponseModel<Payment>();
        var result = await paymentQueries.GetByIdAsync(id);
        return Ok(response.SetResult(true, String.Empty).SetData(result));
    }
}
