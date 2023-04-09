using Recommerce.Infrastructure.Pagination.Dto;
using Recommerce.Infrastructure.Rop;
using Recommerce.Services.Products.Dto;
using Scrutor.AspNetCore;

namespace Recommerce.Services.Products;

public interface IProductService : IScopedLifetime
{
    public Task<Result<int>> CreateAsync(CreateProductInDto productInDto, CancellationToken cancellationToken);

    public Task<Result> UpdateAsync(string uniqueIdentifier, UpdateProductInDto productInDto,
        CancellationToken cancellationToken);

    public Task<Result> DeleteAsync(string uniqueIdentifier, CancellationToken cancellationToken);

    public Task<Result<ProductOutDto>> GetByIdAsync(string uniqueIdentifier, CancellationToken cancellationToken);

    public Task<Result<PaginationResponseDto<ProductOutDto>>> GetListAsync(PaginationRequestDto paginationRequestDto,
        CancellationToken cancellationToken);

    public Task<Result<Dictionary<string,int>>> GetProductIdAsync(IEnumerable<string> productIdentifierList,
        CancellationToken cancellationToken);
}