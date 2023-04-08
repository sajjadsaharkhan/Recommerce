using JetBrains.Annotations;

namespace Recommerce.Infrastructure.Pagination.Dto;

[PublicAPI]
public class PaginationRequestDto
{
    public PaginationRequestDto()
    {
        PageSize = 25;
        PageNumber = 1;
    }

    public PaginationRequestDto(int pageSize)
    {
        PageSize = pageSize;
        PageNumber = 1;
    }

    public PaginationRequestDto(int pageSize, int pageNumber)
    {
        PageSize = pageSize;
        PageNumber = pageNumber;
    }

    public int PageSize { get; set; }
    public int PageNumber { get; set; }
}