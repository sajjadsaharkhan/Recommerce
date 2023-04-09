using System.Text;

namespace Recommerce.Services.Algorithms.AssociationRules.Helpers;

public static class AssociationRuleHelpers
{
    public static string ListToDisplayString(List<string> list, string separator = ", ")
    {
        if (list.Count == 0)
            return string.Empty;
        var sb = new StringBuilder();
        sb.Append(list[0]);
        for (var i = 1; i < list.Count; i++)
        {
            sb.Append($"{separator}{list[i]}");
        }
         
        return sb.ToString();
    }

    public static string ListToDisplayString(IEnumerable<string> list, string exclude, string separator = ", ")
    {
        var dump = (from item in list where item != exclude select item.ToString()).ToList();
        return ListToDisplayString(dump);
    }

    public static string ToPercentString(this object item)
    {
        return $"{item} %";
    }
}