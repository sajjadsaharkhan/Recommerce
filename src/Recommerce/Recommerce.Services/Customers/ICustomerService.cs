using Recommerce.Infrastructure.Pagination.Dto;
using Recommerce.Infrastructure.Rop;
using Recommerce.Services.Customers.Dto;
using Scrutor.AspNetCore;

namespace Recommerce.Services.Customers;

public interface ICustomerService : IScopedLifetime
{
    public Task<Result<CustomerOutDto>> GetByIdAsync(string uniqueIdentifier, CancellationToken cancellationToken);

    public Task<Result<PaginationResponseDto<CustomerOutDto>>> GetListAsync(PaginationRequestDto paginationRequestDto,
        CancellationToken cancellationToken);

    public Task<Result> UpdateAsync(string uniqueIdentifier, CustomerUpdateInDto customerUpdateInDto,
        CancellationToken cancellationToken);

    public Task<Result<int>> CreateAsync(CustomerCreateInDto customerCreateInDto, CancellationToken cancellationToken);

    public Task<Result> DeleteAsync(string uniqueIdentifier, CancellationToken cancellationToken);

    public Task<Result> LoginAsync(string uniqueIdentifier, CancellationToken cancellationToken);
}