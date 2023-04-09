using Mapster;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Recommerce.Services.Orders;
using Recommerce.ViewModels.Orders;
using Recommerce.Services.Orders.Dto;

namespace Recommerce.Controllers;

[ApiController]
[Route("[controller]")]
public class OrderController : ControllerBase
{
    private readonly IOrderService _orderService;

    public OrderController(IOrderService orderService)
    {
        _orderService = orderService;
    }


    [HttpPost]
    public async Task<IActionResult> Create(CreateOrderInVm orderInVm, CancellationToken cancellationToken)
    {
        var validator = new CreateOrderInVmValidator();
        var validationResult = await validator.ValidateAsync(orderInVm, cancellationToken);
        if (!validationResult.IsValid)
        {
            return BadRequest(validationResult.Errors);
        }

        var orderInDto = orderInVm.Adapt<CreateOrderInDto>();

        var createProductResult = await _orderService.CreateAsync(orderInDto, cancellationToken);

        return createProductResult.IsSuccess
            ? NoContent()
            : BadRequest();
    }
}