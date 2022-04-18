
namespace API.Controllers;

[Route("api/v{version:apiVersion}/partners")]
[ApiController]
[ApiVersion("1.0")]
[ApiExplorerSettings(GroupName = "v1")]
public class PartnersController : ControllerBase
{
    private readonly ILogger<PartnersController> logger;
    private readonly IMediator mediator;
    private readonly IPartnerQueries partnerQueries;

    public PartnersController(ILogger<PartnersController> logger, IMediator mediator
        , IPartnerQueries partnerQueries)
    {
        this.logger = logger;
        this.mediator = mediator;
        this.partnerQueries = partnerQueries;
    }
    [HttpPost]
    [ProducesResponseType(typeof(CommonResponseModel<Partner>), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    public async Task<ActionResult<CommonResponseModel<Partner>>> Create([FromBody] CreatePartnerCommand partner)
    {
        if (partner == null)
            return BadRequest();
        var response = new CommonResponseModel<Partner>();
        var result = await mediator.Send(partner);
        return Ok(result.Success ? response.SetData(result.Data).SetResult(result.Success, result.Message ?? String.Empty)
                 : response.SetData(null).SetResult(result.Success, result.Message ?? String.Empty)
            );
    }
    [HttpPut]
    [ProducesResponseType(typeof(CommonResponseModel<Partner>), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    public async Task<ActionResult<CommonResponseModel<Partner>>> Update([FromBody] UpdatePartnerCommand partner)
    {
        if (partner == null)
            return BadRequest();
        var response = new CommonResponseModel<Partner>();
        var result = await mediator.Send(partner);
        return Ok(result.Success ? response.SetData(result.Data).SetResult(result.Success, result.Message ?? String.Empty)
                 : response.SetData(null).SetResult(result.Success, result.Message ?? String.Empty)
            );
    }
    [HttpDelete]
    [ProducesResponseType(typeof(CommonResponseModel<object>), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    public async Task<ActionResult<CommonResponseModel<object>>> Delete([FromBody] DeletePartnerCommand partner)
    {
        if (partner == null)
            return BadRequest();
        var response = new CommonResponseModel<bool>();
        var result = await mediator.Send(partner);
        return Ok(result.Success ? response.SetData(true).SetResult(result.Success, result.Message ?? String.Empty)
                   : response.SetData(false).SetResult(result.Success, result.Message ?? String.Empty)
              );
    }
    [HttpGet()]
    [ProducesResponseType(typeof(CommonResponseModel<IEnumerable<Partner>>), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    public async Task<ActionResult<CommonResponseModel<IEnumerable<Partner>>>> GetAll()
    {
        var response = new CommonResponseModel<IEnumerable<Partner>>();
        var result = partnerQueries.GetAllAsync();
        return Ok(response.SetResult(true, String.Empty).SetData(result));
    }
    [HttpGet("id")]
    [ProducesResponseType(typeof(CommonResponseModel<Partner?>), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    public async Task<ActionResult<CommonResponseModel<Partner?>>> GetById(string id)
    {
        if (string.IsNullOrEmpty(id))
        {
            return BadRequest();
        }
        var response = new CommonResponseModel<Partner>();
        var result = await partnerQueries.GetByIdAsync(id);
        return Ok(response.SetResult(true, String.Empty).SetData(result));
    }
}
