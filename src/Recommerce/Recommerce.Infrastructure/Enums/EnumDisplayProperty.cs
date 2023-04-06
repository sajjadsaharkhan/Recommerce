using JetBrains.Annotations;

namespace Recommerce.Infrastructure.Enums;

[PublicAPI]
public enum EnumDisplayProperty : byte
{
    Description = 0,
    GroupName = 1,
    Name = 2,
    Prompt = 3,
    ShortName = 4,
    Order = 5
}