using Mapster;
using System.Threading;
using Recommerce.ViewModels;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Recommerce.Data.Entities;
using Recommerce.Services.Products;
using Recommerce.ViewModels.Products;
using Recommerce.Services.Products.Dto;
using Recommerce.Infrastructure.Exceptions;
using Recommerce.Infrastructure.Pagination.Dto;

namespace Recommerce.Controllers;

[ApiController]
[Route("[controller]")]
public class ProductController : ControllerBase
{
    private readonly IProductService _productService;

    public ProductController(IProductService productService)
    {
        _productService = productService;
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateProductInVm productInVm, CancellationToken cancellationToken)
    {
        var validator = new CreateProductInVmValidator();
        var validationResult = await validator.ValidateAsync(productInVm, cancellationToken);
        if (!validationResult.IsValid)
        {
            return BadRequest(validationResult.Errors);
        }

        var productInDto = productInVm.Adapt<CreateProductInDto>();

        var createProductResult = await _productService.CreateAsync(productInDto, cancellationToken);

        return CreatedAtAction(nameof(Create), new { id = createProductResult.Data });
    }

    [HttpPut("{uniqueIdentifier:required}")]
    public async Task<IActionResult> Update(string uniqueIdentifier, UpdateProductInVm productInVm,
        CancellationToken cancellationToken)
    {
        var validator = new UpdateProductInVmValidator();
        var validationResult = await validator.ValidateAsync(productInVm, cancellationToken);
        if (!validationResult.IsValid)
        {
            return BadRequest(validationResult.Errors);
        }

        var updateProductInDto = productInVm.Adapt<UpdateProductInDto>();
        var updateProductResult =
            await _productService.UpdateAsync(uniqueIdentifier, updateProductInDto, cancellationToken);

        if (updateProductResult.Exception is EntityNotFoundException<Product>)
        {
            return NotFound();
        }


        return NoContent();
    }

    [HttpDelete("{uniqueIdentifier:required}")]
    public async Task<ActionResult> Delete(string uniqueIdentifier, CancellationToken cancellationToken)
    {
        var updateProductResult = await _productService.DeleteAsync(uniqueIdentifier, cancellationToken);

        if (updateProductResult.Exception is EntityNotFoundException<Product>)
        {
            return NotFound();
        }

        return NoContent();
    }

    [HttpGet]
    public async Task<IActionResult> GetList([FromQuery] PaginationInVm paginationInVm,
        CancellationToken cancellationToken)
    {
        var paginationInDto = paginationInVm.Adapt<PaginationRequestDto>();
        var paginatedProductListResult = await _productService.GetListAsync(paginationInDto, cancellationToken);

        if (paginatedProductListResult.IsFailed)
            return BadRequest();
        if (paginatedProductListResult.Data is null or { TotalCount: 0 })
            return NoContent();

        return Ok(paginatedProductListResult.Data);
    }

    [HttpGet("{uniqueIdentifier:required}")]
    public async Task<ActionResult<Product>> GetById(string uniqueIdentifier, CancellationToken cancellationToken)
    {
        var productResult = await _productService.GetByIdAsync(uniqueIdentifier, cancellationToken);
        if (productResult.Exception is EntityNotFoundException<Product>)
        {
            return NotFound();
        }

        return Ok(productResult.Data);
    }
}