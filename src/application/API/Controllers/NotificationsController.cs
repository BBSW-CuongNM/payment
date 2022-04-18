using Shared.Models;

namespace API.Controllers;
[Route("api/v{version:apiVersion}/notifications")]
[ApiController]
[ApiVersion("1.0")]
[ApiExplorerSettings(GroupName = "v1")]
public class NotificationsController : Controller
{
    private readonly ILogger<PaymentsController> logger;
    private readonly IMediator mediator;

    public NotificationsController(ILogger<PaymentsController> logger, IMediator mediator)
    {
        this.logger = logger;
        this.mediator = mediator;
    }

    [HttpGet("vnpay_callback")]
    [ProducesResponseType(typeof(VNPayPaymentReturnResponseModel), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    public async Task<ActionResult<VNPayPaymentReturnResponseModel>> VnPayConfirm(
        [FromQuery] VNPayPaymentReturnCommand notification)
    {
        if (notification == null)
            return BadRequest();
        var result = await mediator.Send(notification);
        return Ok(result);
    }
    [HttpPost("momo_callback")]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    public async Task<ActionResult<CommonResponseModel<object>>> MoMoConfirm(
        [FromBody] MoMoPaymentReturnCommand command)
    {
        await mediator.Send(command);
        return NoContent();
    }
}

