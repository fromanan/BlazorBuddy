using System.ComponentModel;

namespace Application.Data;

public static class Utils
{
    public static string GetTimeString(DateTime time)
    {
        int count;
        string unit;
        
        TimeSpan difference = DateTime.Now - time;
        if (difference.Days > 365)
        {
            count = difference.Days / 365;
            unit = "year";
        }
        else if (difference.Days > 30)
        {
            count = difference.Days / 30;
            unit = "month";
        }
        else if (difference.Days > 0)
        {
            count = difference.Days;
            unit = "day";
        }
        else if (difference.Hours > 0)
        {
            count = difference.Hours;
            unit = "hour";
        }
        else if (difference.Minutes > 0)
        {
            count = difference.Minutes;
            unit = "minute";
        }
        else
        {
            return difference.Seconds < 10 ? "a few seconds" : "less than a minute";
        }

        return count == 1 ? $"a{(unit == "hour" ? "n" : string.Empty)} {unit}" : Pluralize(count, unit);
    }

    public static string GetVerb(SessionType type)
    {
        return type switch
        {
            SessionType.Current     => "Changed",
            SessionType.Previous    => "Recorded",
            SessionType.Saved       => "Saved",
            SessionType.Updated     => "Updated",
            _                       => throw new InvalidEnumArgumentException($"Invalid session type '{type}'")
        };
    }

    public static string Pluralize(int count, string unit)
    {
        return $"{count} {unit}{(count != 1 ? "s" : string.Empty)}";
    }
}