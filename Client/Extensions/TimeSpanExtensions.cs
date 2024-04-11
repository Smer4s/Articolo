using System.Text;

namespace Client.Extensions;

public static class TimeSpanExtensions
{
    public static string ToTimeString(this TimeSpan value)
    {
        var minutes = (int)Math.Round(value.TotalMinutes);

        if (minutes < 0) return "Только что";

        if (minutes > 60)
        {
            if (minutes > 60 * 24) return ToDateString(minutes);
            return ToHoursString(minutes / 60);
        }
        else return ToMinutesString(minutes);
    }

    private static string ToMinutesString(int minutes)
    {
        return $"{minutes} мин назад";
    }

    private static string ToHoursString(int hours)
    {
        if (hours >= 5 && hours <= 20) return $"{hours} часов назад";
        if (hours % 10 >= 2 && hours % 10 <= 4) return $"{hours} часа назад";
        return $"{hours} час назад";
    }

    private static string ToDateString(int minutes)
    {
        return DateTime.FromBinary(minutes).ToShortDateString();
    }
}
