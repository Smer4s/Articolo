using System.Text;

namespace Client.Extensions;

public static class TimeSpanExtensions
{
	public static string ToTimeIntervalString(this DateTime created)
	{
		var minutes = (int)Math.Round((DateTime.Now - created).TotalMinutes);

		if (minutes <= 5) return "Только что";

		if (minutes >= 60)
		{
			if (minutes >= 60 * 24) return ToDateString(created);

			return ToHoursString(minutes / 60);
		}

		else return ToMinutesString(minutes);
	}

	private static string ToDateString(DateTime created)
	{
		var sb = new StringBuilder();

		sb.Append(created.Day.ToString() + " ");
		sb.Append(created.Month.ToRussianMonthString());
		if (DateTime.Now.Year > created.Year) sb.Append(created.Year);

		return sb.ToString();
	}

	private static string ToRussianMonthString(this int month) =>
		month switch
		{
			1 => "января ",
			2 => "февраля ",
			3 => "марта ",
			4 => "апреля ",
			5 => "мая ",
			6 => "июня ",
			7 => "июля ",
			8 => "августа ",
			9 => "сентября ",
			10 => "октября ",
			11 => "ноября ",
			12 => "декабря ",
			_ => throw new ArgumentOutOfRangeException(nameof(month))
		};

	private static string ToMinutesString(int minutes)
	{
		if (minutes >= 5 && minutes <= 20 || minutes % 10 >= 5 || minutes % 10 == 0) return $"{minutes} минут назад";
		if (minutes % 10 == 1) return $"{minutes} минуту назад";
		if (minutes % 10 >= 2 && minutes % 10 <= 4) return $"{minutes} минуты назад";

		throw new ArgumentOutOfRangeException(nameof(minutes));
	}

	private static string ToHoursString(int hours)
	{
		if (hours >= 5 && hours <= 20) return $"{hours} часов назад";
		if (hours % 10 >= 2 && hours % 10 <= 4) return $"{hours} часа назад";
		return $"{hours} час назад";
	}
}
