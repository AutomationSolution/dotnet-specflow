using System.Text.RegularExpressions;

namespace AutomationMobile.Utilities.BrowserStack;

// Whole this class looks like a mess, but haven't found a better solution among existing ones
public static class BrowserStackScriptsFormatter
{
    private static string doubleQuotes = "\"";
    private static string bracketOpen = "{";
    private static string bracketClose = "}";
    private static string tempPlaceholderDoubleQuotes = "__TEMP__";
    private static string tempPlaceholderBracketOpen = "__BRACKETOPEN__";
    private static string tempPlaceholderBracketClose = "__BRACKETCLOSE__";

    private static string pattern = @"{\d+}";
    private static string patternRestore = @"<<\d+>>";

    public static string ReplaceBeforeFormat(string input)
    {
        // Securing our targets to perform formatting
        var result = Regex.Replace(input, pattern, FormattingReplacerBefore);
        // Replacing conflicting symbols before formatting
        result = result
            .Replace(doubleQuotes, tempPlaceholderDoubleQuotes)
            .Replace(bracketOpen, tempPlaceholderBracketOpen)
            .Replace(bracketClose, tempPlaceholderBracketClose);
        // Restoring out targets fo perform formatting
        result = Regex.Replace(result, patternRestore, FormattingReplacerAfter);
        return result;
    }

    public static string ReplaceAfterFormat(string input)
    {
        return input
            .Replace(tempPlaceholderDoubleQuotes, doubleQuotes)
            .Replace(tempPlaceholderBracketOpen, bracketOpen)
            .Replace(tempPlaceholderBracketClose, bracketClose);
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