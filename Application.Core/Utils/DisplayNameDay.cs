using Application.Dto.Utils;
using System.ComponentModel.DataAnnotations;

namespace Application.Core.Utils;

public static class DisplayNameDay
{
    public static string GetEnumDisplayName(WeekDay day)
    {
        var displayAttribute = (DisplayAttribute)Attribute.GetCustomAttribute(day.GetType().
            GetField(day.ToString()), typeof(DisplayAttribute));

        return displayAttribute?.Name ?? day.ToString();
    }
}
