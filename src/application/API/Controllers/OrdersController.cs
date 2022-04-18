
namespace API.Controllers;

[Route("api/v{version:apiVersion}/orders")]
[ApiController]
[ApiVersion("1.0")]
[ApiExplorerSettings(GroupName = "v1")]
public class OrdersController : ControllerBase
{
    private readonly ILogger<OrdersController> logger;
    private readonly IMediator mediator;
    private readonly IOrderQueries orderQueries;

    public OrdersController(ILogger<OrdersController> logger, IMediator mediator, IOrderQueries orderQueries)
    {
        this.logger = logger;
        this.mediator = mediator;
        this.orderQueries = orderQueries;
    }
    [HttpPost]
    [ProducesResponseType(typeof(CommonResponseModel<OrderDto>), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    public async Task<ActionResult<CommonResponseModel<OrderDto>>> Create([FromBody] CreateOrderCommand order)
    {
        if (order == null)
            return BadRequest();
        var response = new CommonResponseModel<OrderDto>();
        var result = await mediator.Send(order);
        return Ok(result.Success ? response.SetData(result.Data).SetResult(result.Success, result.Message ?? String.Empty)
                 : response.SetData(null).SetResult(result.Success, result.Message ?? String.Empty)
            );
    }
    [HttpPut]
    [ProducesResponseType(typeof(CommonResponseModel<OrderDto>), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    public async Task<ActionResult<CommonResponseModel<OrderDto>>> Update([FromBody] UpdateOrderCommand order)
    {
        if (order == null)
            return BadRequest();
        var response = new CommonResponseModel<OrderDto>();
        var result = await mediator.Send(order);
        return Ok(result.Success ? response.SetData(result.Data).SetResult(result.Success, result.Message ?? String.Empty)
                 : response.SetData(null).SetResult(result.Success, result.Message ?? String.Empty)
            );
    }
    [HttpDelete]
    [ProducesResponseType(typeof(CommonResponseModel<object>), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    public async Task<ActionResult<CommonCommandResultHasData<object>>> Delete([FromBody] DeleteOrderCommand order)
    {
        if (order == null)
            return BadRequest();
        var response = new CommonResponseModel<bool>();
        var result = await mediator.Send(order);
        return Ok(result.Success ? response.SetData(true).SetResult(result.Success, result.Message ?? String.Empty)
                   : response.SetData(false).SetResult(result.Success, result.Message ?? String.Empty)
              );
    }
    [HttpGet()]
    [ProducesResponseType(typeof(CommonResponseModel<IEnumerable<Order>>), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    public async Task<ActionResult<CommonResponseModel<IEnumerable<Order>>>> GetAll()
    {
        var response = new CommonResponseModel<IEnumerable<Order>>();
        var result =  orderQueries.GetAllAsync();
        return Ok(response.SetResult(true, String.Empty).SetData(result));
    }
    [HttpGet("order/{order_id}")]
    [ProducesResponseType(typeof(CommonResponseModel<Order?>), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    public async Task<ActionResult<CommonResponseModel<Order?>>> GetById(string order_id)
    {
        if (string.IsNullOrEmpty(order_id))
        {
            return BadRequest();
        }
        var response = new CommonResponseModel<Order>();
        var result = await orderQueries.GetByIdAsync(order_id);
        return Ok(response.SetResult(true, String.Empty).SetData(result));
    }
    [HttpGet("service_order/{service_order_id}")]
    [ProducesResponseType(typeof(CommonResponseModel<Order?>), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    public async Task<ActionResult<CommonResponseModel<Order?>>> GetServiceOrderIdById(string service_order_id)
    {
        if (string.IsNullOrEmpty(service_order_id))
        {
            return BadRequest();
        }
        var response = new CommonResponseModel<Order>();
        var result = await orderQueries.GetByServiceOrderIdAsync(service_order_id);
        return Ok(response.SetResult(true, String.Empty).SetData(result));
    }
}

