using System.Threading;
using System.Threading.Tasks;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using Recommerce.Data.Entities;
using Recommerce.Infrastructure.Exceptions;
using Recommerce.Infrastructure.Pagination.Dto;
using Recommerce.Services.Customers;
using Recommerce.Services.Customers.Dto;
using Recommerce.ViewModels;
using Recommerce.ViewModels.Customers;

namespace Recommerce.Controllers;

[ApiController]
[Route("[controller]")]
public class CustomersController : ControllerBase
{
    private readonly ICustomerService _customerService;

    public CustomersController(ICustomerService customerService)
    {
        _customerService = customerService;
    }


    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] PaginationInVm paginationInVm,
        CancellationToken cancellationToken)
    {
        var validator = new PaginationInVmValidator();
        var validationResult = await validator.ValidateAsync(paginationInVm, cancellationToken);
        if (!validationResult.IsValid)
        {
            return BadRequest(validationResult.Errors);
        }

        var paginationRequestDto = paginationInVm.Adapt<PaginationRequestDto>();

        var customerListResult = await _customerService.GetListAsync(paginationRequestDto, cancellationToken);
        if (customerListResult.IsFailed || customerListResult.Data.TotalCount == 0)
            return NoContent();

        var paginatedCustomerList = customerListResult.Data.Adapt<PaginationOutVm<CustomerOutVm>>();

        return Ok(paginatedCustomerList);
    }

    [HttpGet("{uniqueIdentifier:required}")]
    public async Task<IActionResult> GetById(string uniqueIdentifier, CancellationToken cancellationToken)
    {
        var customerResult = await _customerService.GetByIdAsync(uniqueIdentifier, cancellationToken);

        if (customerResult.Exception is EntityNotFoundException<Customer>)
        {
            return NotFound();
        }

        var customerOutVm = customerResult.Data.Adapt<CustomerOutVm>();

        return Ok(customerOutVm);
    }

    [HttpPost]
    public async Task<IActionResult> Create(CustomerCreateInVm customerVm, CancellationToken cancellationToken)
    {
        var validator = new CustomerCreateInValidator();
        var validationResult = await validator.ValidateAsync(customerVm, cancellationToken);
        if (!validationResult.IsValid)
        {
            return BadRequest(validationResult.Errors);
        }

        var customerCreateInDto = customerVm.Adapt<CustomerCreateInDto>();

        var customerId = await _customerService.CreateAsync(customerCreateInDto, cancellationToken);

        return CreatedAtAction(nameof(Create), new { id = customerId.Data });
    }


    [HttpPut("{uniqueIdentifier:required}")]
    public async Task<IActionResult> Update(string uniqueIdentifier, CustomerUpdateInVm customerUpdateInVm,
        CancellationToken cancellationToken)
    {
        var validator = new CustomerUpdateInVmValidator();
        var validationResult = await validator.ValidateAsync(customerUpdateInVm, cancellationToken);

        if (!validationResult.IsValid)
        {
            return BadRequest(validationResult.Errors);
        }

        var customerUpdateInDto = customerUpdateInVm.Adapt<CustomerUpdateInDto>();

        var customerResult =
            await _customerService.UpdateAsync(uniqueIdentifier, customerUpdateInDto, cancellationToken);
        if (customerResult.Exception is EntityNotFoundException<Customer>)
        {
            return NotFound();
        }

        return NoContent();
    }

    [HttpDelete("{uniqueIdentifier:required}")]
    public async Task<IActionResult> Delete(string uniqueIdentifier, CancellationToken cancellationToken)
    {
        var customerDeleteResult = await _customerService.DeleteAsync(uniqueIdentifier, cancellationToken);

        if (customerDeleteResult.IsFailed)
        {
            return NotFound();
        }

        return NoContent();
    }

    [HttpPost("[action]")]
    public async Task<IActionResult> Login(string uniqueIdentifier, CancellationToken cancellationToken)
    {
        var customerLoginResult = await _customerService.LoginAsync(uniqueIdentifier, cancellationToken);

        if (customerLoginResult.IsFailed)
        {
            return NotFound();
        }

        return NoContent();
    }
}