using System.Collections;

namespace Recommerce.ViewModels;

public record PaginationOutVm<TData>(
    IEnumerable Data,
    int TotalCount,
    int PageSize,
    int PageNumber
);