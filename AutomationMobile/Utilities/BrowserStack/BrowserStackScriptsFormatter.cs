using System.Text.RegularExpressions;

namespace AutomationMobile.Utilities.BrowserStack;

// Whole this class looks like a mess, but haven't found a better solution among existing ones
public static class BrowserStackScriptsFormatter
{
    private const string DoubleQuotes = "\"";
    private const string BracketOpen = "{";
    private const string BracketClose = "}";
    private const string TempPlaceholderDoubleQuotes = "__TEMP__";
    private const string TempPlaceholderBracketOpen = "__BRACKETOPEN__";
    private const string TempPlaceholderBracketClose = "__BRACKETCLOSE__";

    private const string Pattern = @"{\d+}";
    private const string PatternRestore = @"<<\d+>>";

    public static string ReplaceBeforeFormat(string input)
    {
        // Securing our targets to perform formatting
        var result = Regex.Replace(input, Pattern, FormattingReplacerBefore);
        // Replacing conflicting symbols before formatting
        result = result
            .Replace(DoubleQuotes, TempPlaceholderDoubleQuotes)
            .Replace(BracketOpen, TempPlaceholderBracketOpen)
            .Replace(BracketClose, TempPlaceholderBracketClose);
        // Restoring out targets fo perform formatting
        result = Regex.Replace(result, PatternRestore, FormattingReplacerAfter);
        return result;
    }

    public static string ReplaceAfterFormat(string input)
    {
        return input
            .Replace(TempPlaceholderDoubleQuotes, DoubleQuotes)
            .Replace(TempPlaceholderBracketOpen, BracketOpen)
            .Replace(TempPlaceholderBracketClose, BracketClose);
    }

    private static string FormattingReplacerBefore(Match match)
    {
        return match.Value.Replace("{", "<<").Replace("}", ">>");
    }

    private static string FormattingReplacerAfter(Match match)
    {
        return match.Value.Replace("<<", "{").Replace(">>", "}");
    }
}
