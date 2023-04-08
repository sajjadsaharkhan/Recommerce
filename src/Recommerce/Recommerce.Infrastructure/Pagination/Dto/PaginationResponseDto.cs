using JetBrains.Annotations;

namespace Recommerce.Infrastructure.Pagination.Dto;

[PublicAPI]
public class PaginationResponseDto<TData>
{
    public List<TData> Data { get; set; } = default!;
    public int TotalCount { get; set; }
    public int PageSize { get; set; }
    public int PageNumber { get; set; }

    public static PaginationResponseDto<TData> Empty(int pageSize, int pageNumber)
        => Create(pageSize, pageNumber, 0, new List<TData>());

    public static PaginationResponseDto<TData> Empty(PaginationRequestDto requestDto)
        => Create(requestDto, new List<TData>(), 0);

    public static PaginationResponseDto<TData> Create(PaginationRequestDto requestDto, List<TData> data, int totalCount)
        => new()
        {
            Data = data.Any() ? data : new List<TData>(),
            TotalCount = totalCount,
            PageSize = requestDto.PageSize,
            PageNumber = requestDto.PageNumber
        };

    public static PaginationResponseDto<TData> Create(IEnumerable<TData> data)
    {
        var dataList = data.ToList();
        return new()
        {
            Data = dataList.Any() ? dataList : new List<TData>(),
            TotalCount = dataList.Count,
            PageSize = dataList.Count,
            PageNumber = 1
        };
    }

    public static PaginationResponseDto<TData> Create(int pageSize, int pageNumber, int totalCount, List<TData> data)
        => new()
        {
            Data = data.Any() ? data : new List<TData>(),
            TotalCount = totalCount,
            PageSize = pageSize,
            PageNumber = pageNumber
        };
}