using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using Recommerce.Services.Baskets;
using Recommerce.Services.Baskets.Dto;
using Recommerce.ViewModels.Baskets;

namespace Recommerce.Controllers;

[ApiController]
[Route("[controller]")]
public class BasketController : ControllerBase
{
    private readonly IBasketService _basketService;

    public BasketController(IBasketService basketService)
    {
        _basketService = basketService;
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateBasketItemInVm createBasketItemInVm,
        CancellationToken cancellationToken)
    {
        var validator = new CreateBasketItemInVmValidator();
        var validationResult = await validator.ValidateAsync(createBasketItemInVm, cancellationToken);
        if (!validationResult.IsValid)
        {
            return BadRequest(validationResult.Errors);
        }

        var createBasketInDto = createBasketItemInVm.Adapt<CreateBasketItemInDto>();

        var createBasketResult = await _basketService.CreateBasketItemAsync(createBasketInDto, cancellationToken);
        if (createBasketResult.IsFailed)
            return BadRequest(createBasketResult.Exception);

        return CreatedAtAction(nameof(Create), new { id = createBasketResult.Data });
    }

    [HttpDelete("{itemUniqueIdentifier:required}")]
    public async Task<IActionResult> Delete(string itemUniqueIdentifier, CancellationToken cancellationToken)
    {
        var deleteBasketResult = await _basketService.DeleteBasketItemAsync(itemUniqueIdentifier, cancellationToken);
        if (deleteBasketResult.IsFailed)
            return BadRequest(deleteBasketResult.Exception);

        return NoContent();
    }
    
    [HttpGet("{customerUniqueIdentifier:required}")]
    public async Task<IActionResult> Get(string customerUniqueIdentifier, CancellationToken cancellationToken)
    {
        var basketListResult = await _basketService.GetCustomerBasketListAsync(customerUniqueIdentifier, cancellationToken);
        if (basketListResult.IsFailed)
            return BadRequest(basketListResult.Exception);

        return basketListResult.Data.Any()
            ? Ok(basketListResult.Data)
            : NoContent();
    }
}