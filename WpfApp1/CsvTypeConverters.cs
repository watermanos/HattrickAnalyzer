using System;
using System.Globalization;
using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.TypeConversion;

namespace HattrickAnalyzer.Core.Models;

public sealed class IntZeroOnInvalidConverter : ITypeConverter
{
    public object ConvertFromString(string? text, IReaderRow row, MemberMapData memberMapData)
    {
        if (string.IsNullOrWhiteSpace(text))
            return 0;

        // Trim and remove common thousands separators that might interfere
        text = text.Trim();
        if (int.TryParse(text, NumberStyles.Integer, CultureInfo.InvariantCulture, out var value))
            return value;

        // Common placeholders to treat as "unknown"
        if (text == "?" || text.Equals("n/a", StringComparison.OrdinalIgnoreCase) || text.Equals("-", StringComparison.OrdinalIgnoreCase))
            return 0;

        // Last resort: try to extract digits
        var digits = System.Text.RegularExpressions.Regex.Replace(text, @"[^\d\-]", "");
        if (int.TryParse(digits, NumberStyles.Integer, CultureInfo.InvariantCulture, out value))
            return value;

        return 0;
    }

    public string ConvertToString(object? value, IWriterRow row, MemberMapData memberMapData)
    {
        return value?.ToString() ?? "0";
    }
}

public sealed class DoubleZeroOnInvalidConverter : ITypeConverter
{
    public object ConvertFromString(string? text, IReaderRow row, MemberMapData memberMapData)
    {
        if (string.IsNullOrWhiteSpace(text))
            return 0.0;

        text = text.Trim();
        if (double.TryParse(text, NumberStyles.Float | NumberStyles.AllowThousands, CultureInfo.InvariantCulture, out var value))
            return value;

        if (text == "?" || text.Equals("n/a", StringComparison.OrdinalIgnoreCase) || text.Equals("-", StringComparison.OrdinalIgnoreCase))
            return 0.0;

        var cleaned = System.Text.RegularExpressions.Regex.Replace(text, @"[^\d\.\-]", "");
        if (double.TryParse(cleaned, NumberStyles.Float | NumberStyles.AllowThousands, CultureInfo.InvariantCulture, out value))
            return value;

        return 0.0;
    }

    public string ConvertToString(object? value, IWriterRow row, MemberMapData memberMapData)
    {
        if (value is double d)
            return d.ToString(CultureInfo.InvariantCulture);
        return "0.0";
    }
}