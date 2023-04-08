using JetBrains.Annotations;
using Mapster;
using Microsoft.EntityFrameworkCore;
using Recommerce.Data.DbContexts;
using Recommerce.Data.Entities;
using Recommerce.Infrastructure.Exceptions;
using Recommerce.Infrastructure.Pagination;
using Recommerce.Infrastructure.Pagination.Dto;
using Recommerce.Infrastructure.Rop;
using Recommerce.Services.Customers.Dto;

namespace Recommerce.Services.Customers.Implementations;

[UsedImplicitly]
public class CustomerService : ICustomerService
{
    private readonly AppDbContext _dbContext;

    public CustomerService(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Result<CustomerOutDto>> GetByIdAsync(string uniqueIdentifier, CancellationToken cancellationToken)
    {
        var customer = await _dbContext.Customers
            .AsNoTracking()
            .Where(c => c.UniqueIdentifier == uniqueIdentifier)
            .Select(c => c.Adapt<CustomerOutDto>())
            .FirstOrDefaultAsync(cancellationToken);

        if (customer is null or default(CustomerOutDto))
            return new EntityNotFoundException<Customer>(uniqueIdentifier);

        var customerDto = customer.Adapt<CustomerOutDto>();

        return customerDto;
    }

    public async Task<Result<PaginationResponseDto<CustomerOutDto>>> GetListAsync(
        PaginationRequestDto paginationRequestDto, CancellationToken cancellationToken)
    {
        var paginatedCustomerList = await _dbContext.Customers
            .AsNoTracking()
            .OrderByDescending(x => x.Id)
            .Select(x => x.Adapt<CustomerOutDto>())
            .ToPaginationAsync(paginationRequestDto, cancellationToken);

        return paginatedCustomerList;
    }

    public async Task<Result> UpdateAsync(string uniqueIdentifier, CustomerUpdateInDto customerUpdateInDto,
        CancellationToken cancellationToken)
    {
        var customer = await _dbContext.Customers
            .Where(c => c.UniqueIdentifier == uniqueIdentifier)
            .FirstOrDefaultAsync(cancellationToken);

        if (customer is null or default(Customer))
            return new EntityNotFoundException<Customer>(uniqueIdentifier);

        customer.BirthDate = customerUpdateInDto.BirthDate;
        customer.GenderType = customerUpdateInDto.GenderType;
        customer.ShoppingBalance = customerUpdateInDto.ShoppingBalance;

        await _dbContext.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }

    public async Task<Result<int>> CreateAsync(CustomerCreateInDto customerCreateInDto,
        CancellationToken cancellationToken)
    {
        var customer = customerCreateInDto.Adapt<Customer>();

        await _dbContext.Customers.AddAsync(customer, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return customer.Id;
    }

    public async Task<Result> DeleteAsync(string uniqueIdentifier, CancellationToken cancellationToken)
    {
        var customer = await _dbContext.Customers
            .Where(c => c.UniqueIdentifier == uniqueIdentifier)
            .FirstOrDefaultAsync(cancellationToken);

        if (customer is null or default(Customer))
            return new EntityNotFoundException<Customer>(uniqueIdentifier);

        customer.IsDeleted = true;
        await _dbContext.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }

    public async Task<Result> LoginAsync(string uniqueIdentifier, CancellationToken cancellationToken)
    {
        var customer = await _dbContext.Customers
            .Where(c => c.UniqueIdentifier == uniqueIdentifier)
            .FirstOrDefaultAsync(cancellationToken);

        if (customer is null or default(Customer))
            return new EntityNotFoundException<Customer>(uniqueIdentifier);

        customer.LastLoginDate = DateTime.Now;
        await _dbContext.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}