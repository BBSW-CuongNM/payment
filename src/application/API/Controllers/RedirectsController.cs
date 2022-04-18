using Shared.Models;

namespace API.Controllers;
[Route("api/v{version:apiVersion}/redirect")]
[ApiController]
[ApiVersion("1.0")]
[ApiExplorerSettings(GroupName = "v1")]
public class RedirectsController : ControllerBase
{
    private readonly IMapper mapper;
    private readonly IPaymentQueries paymentQueries;
    private readonly IMediator mediator;

    public RedirectsController(IMapper mapper,
        IPaymentQueries paymentQueries,
        IMediator mediator)
    {
        this.mapper = mapper;
        this.paymentQueries = paymentQueries;
        this.mediator = mediator;
    }

    [HttpGet]
    [Route("vnpay_redirect")]
    [ProducesResponseType((int)HttpStatusCode.Redirect)]
    public async Task<IActionResult> VnPayRedirect([FromQuery] VNPayPaymentRedirectCommand request)
    {
        var resultUrl = string.Empty;
        resultUrl = await mediator.Send(request);
        return Redirect(resultUrl);
    }

    [HttpGet]
    [Route("momo_redirect")]
    [ProducesResponseType((int)HttpStatusCode.Redirect)]
    public async Task<IActionResult> MoMoRedirectAsync([FromQuery] MoMoPaymentRedirectCommand request)
    {
        var resultUrl = string.Empty;
        resultUrl = await mediator.Send(request);
        return Redirect(resultUrl);
    }
}
