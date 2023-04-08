using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using Recommerce.Infrastructure.Pagination.Dto;

namespace Recommerce.Infrastructure.Pagination;

[PublicAPI]
public static class PaginationIQueryableExtension
{
    public static async Task<PaginationResponseDto<TSource>> ToPaginationAsync<TSource>(
        this IQueryable<TSource> source, PaginationRequestDto requestDto, CancellationToken cancellationToken)
    {
        if (source is null)
            throw new ArgumentNullException(nameof(source));
        if (requestDto is null)
            throw new ArgumentNullException(nameof(requestDto));

        var skip = (requestDto.PageNumber - 1) * requestDto.PageSize;
        var dataList = await source.Skip(skip).Take(requestDto.PageSize).ToListAsync(cancellationToken);
        var totalCount = await source.CountAsync(cancellationToken);

        return new PaginationResponseDto<TSource>
        {
            Data = dataList.Any()
                ? dataList
                : new List<TSource>(),
            PageNumber = requestDto.PageNumber,
            PageSize = requestDto.PageSize,
            TotalCount = totalCount
        };
    }

    public static PaginationResponseDto<TSource> ToPagination<TSource>(this IEnumerable<TSource> source,
        PaginationRequestDto requestDto)
    {
        if (source is null)
            throw new ArgumentNullException(nameof(source));
        if (requestDto is null)
            throw new ArgumentNullException(nameof(requestDto));

        var sourceList = source.ToList();

        var skip = (requestDto.PageNumber - 1) * requestDto.PageSize;
        var dataList = sourceList.Skip(skip).Take(requestDto.PageSize).ToList();
        var totalCount = sourceList.Count;

        return new PaginationResponseDto<TSource>
        {
            Data = dataList,
            PageNumber = requestDto.PageNumber,
            PageSize = requestDto.PageSize,
            TotalCount = totalCount
        };
    }
}