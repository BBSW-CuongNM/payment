namespace API.Controllers;

[Route("api/v{version:apiVersion}/authenticates")]
[ApiController]
[ApiVersion("1.0")]
[ApiExplorerSettings(GroupName = "v1")]
public class AuthenticatesController : ControllerBase
{
    private readonly ILogger<AuthenticatesController> logger;
    private readonly IMediator mediator;

    public AuthenticatesController(ILogger<AuthenticatesController> logger, IMediator mediator)
    {
        this.logger = logger;
        this.mediator = mediator;
    }

    [HttpPost]
    [Route("login")]
    [ProducesResponseType(typeof(CommonResponseModel<UserLoginDataTransferObject>), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    public async Task<ActionResult<CommonResponseModel<UserLoginDataTransferObject>>> LoginAsync([FromBody] LoginCommand login)
    {
        if (login == null)
            return BadRequest();
        var response = new CommonResponseModel<UserLoginDataTransferObject>();
        var result = await mediator.Send(login);
        return Ok(result.Success ? response.SetData(result.Data).SetResult(result.Success, result.Message ?? String.Empty)
                 : response.SetData(null).SetResult(result.Success, result.Message ?? String.Empty));
    }

    [HttpPost]
    [Route("change_password")]
    [ProducesResponseType(typeof(CommonResponseModel<bool>), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    public async Task<ActionResult<CommonResponseModel<bool>>> ChangePasswordAsync([FromBody] ChangePasswordCommand login)
    {
        if (login == null)
            return BadRequest();
        var response = new CommonResponseModel<bool>();
        var result = await mediator.Send(login);

        return Ok(result.Success ? response.SetData(true).SetResult(result.Success, result.Message ?? String.Empty)
                   : response.SetData(false).SetResult(result.Success, result.Message ?? String.Empty)
              );
    }
    [HttpPost]
    [Route("refresh_token")]
    [ProducesResponseType(typeof(CommonResponseModel<UserLoginDataTransferObject>), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    public async Task<ActionResult<CommonResponseModel<UserLoginDataTransferObject>>> GetTokenByRefreshTokenAsync(
        [FromBody] RefreshTokenCommand command)
    {
        if (command == null)
            return BadRequest();
        var response = new CommonResponseModel<UserLoginDataTransferObject>();
        var result = await mediator.Send(command);
        return Ok(result.Success ? response.SetData(result.Data).SetResult(result.Success, result.Message ?? String.Empty)
                        : response.SetData(null).SetResult(result.Success, result.Message ?? String.Empty));
    }
    [HttpPost]
    [Route("logout")]
    [ProducesResponseType(typeof(CommonResponseModel<object>), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    public async Task<ActionResult<CommonResponseModel<object>>> Logout([FromBody] LogoutCommand login)
    {
        if (login == null)
            return BadRequest();
        var result = await mediator.Send(login);
        var response = new CommonResponseModel<bool>();
        return Ok(result.Success ? response.SetData(true).SetResult(result.Success, result.Message ?? String.Empty)
                  : response.SetData(false).SetResult(result.Success, result.Message ?? String.Empty)
             );
    }
}
