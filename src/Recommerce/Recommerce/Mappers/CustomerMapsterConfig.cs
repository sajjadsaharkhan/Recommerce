using System.Collections.Generic;
using JetBrains.Annotations;
using Mapster;
using Recommerce.Infrastructure.Enums;
using Recommerce.Infrastructure.Extensions;
using Recommerce.Infrastructure.Pagination.Dto;
using Recommerce.Services.Customers.Dto;
using Recommerce.ViewModels;
using Recommerce.ViewModels.Customers;

namespace Recommerce.Mappers;

[UsedImplicitly]
public class CustomerMapsterConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<CustomerOutDto, CustomerOutVm>()
            .Map(des => des.UniqueIdentifier, src => src.UniqueIdentifier)
            .Map(des => des.ShoppingBalance, src => src.ShoppingBalance)
            .Map(des => des.LastLoginDate, src => src.LastLoginDate)
            .Map(des => des.RegisterDate, src => src.RegisterDate)
            .Map(des => des.BirthDate, src => src.BirthDate)
            .Map(des => des.GenderType, src => src.GenderType.ToDisplay(EnumDisplayProperty.Name));
        
        config.NewConfig<PaginationResponseDto<CustomerOutDto>, PaginationOutVm<CustomerOutVm>>()
            .Map(des => des.Data, src => src.Data.Adapt<IEnumerable<CustomerOutVm>>())
            .Map(des => des.PageNumber, src => src.PageNumber)
            .Map(des => des.PageSize, src => src.PageSize)
            .Map(des => des.TotalCount, src => src.TotalCount);
    }
}