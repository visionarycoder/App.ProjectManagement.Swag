using System.Reflection;

namespace Swag.Ifx.Cli.Helpers;

public static class ConsoleHelper
{

    public const int DEFAULT_SEPARATOR_WIDTH = 72;

    public static int SeparatorWidth { get; set; } = DEFAULT_SEPARATOR_WIDTH;
    public static char Separator { get; set; } = '-';

    public static string Title { get; set; } = string.Empty;
    public static string Description { get; set; } = string.Empty;

    public static string GetIndent(int indentDepth = 0)
    {
        return "".PadRight(indentDepth);
    }

    public static void ShowHeader()
    {
        ShowHeader(Title, Description);
    }

    public static void ShowHeader(string title)
    {
        ShowHeader(title, Description);
    }

    public static void ShowHeader(string title, string description)
    {
        Console.Title = title;
        Console.WriteLine();
        PrintSeparator();
        Console.WriteLine();
        Console.WriteLine(title);
        if (!string.IsNullOrWhiteSpace(description))
        {
            Console.WriteLine(description);
        }
        Console.WriteLine();
        PrintSeparator();
        Console.WriteLine();
    }

    public static void ShowFooter()
    {
        Console.WriteLine();
        PrintSeparator();
        Console.WriteLine("Finished.");
        Console.WriteLine();
        PrintSeparator();
        Console.WriteLine();
    }

    public static void ShowExit()
    {
        Console.WriteLine();
        PrintSeparator();
        Console.WriteLine();
        Console.WriteLine("Press any key to exit.");
        Console.WriteLine();
        PrintSeparator();
        Console.WriteLine();
        Console.ReadKey();
    }

    public static void ShowUpdate(string message)
    {
        Console.WriteLine();
        Console.WriteLine(message);
        Console.WriteLine();
    }

    public static void PrintSeparator()
    {
        PrintSeparator(SeparatorWidth, Separator);
    }

    public static void PrintSeparator(int width)
    {
        PrintSeparator(width, Separator);
    }

    public static void PrintSeparator(int width, char paddingChar)
    {
        Console.WriteLine("".PadLeft(width, paddingChar));
    }

    public static void ShowMenu(IEnumerable<KeyValuePair<int, string>> options)
    {
        ShowUpdate("Select an option:");
        var keyValuePairs = options as KeyValuePair<int, string>[] ?? options.ToArray();
        var keyWidth = keyValuePairs.Max(i => i.Key.ToString().Length);
        var valueWidth = keyValuePairs.Max(i => i.Value.Length);

        foreach (var option in keyValuePairs)
        {
            var key = option.Key.ToString("N0").PadLeft(keyWidth);
            var value = option.Value.PadRight(valueWidth);
            Console.WriteLine($"{key}: {value}");
        }
    }

    public static void ShowAsTable(IEnumerable<object> source)
    {
        var src = source.ToList();
        var propertyInfos = source
            .GetType()
            .GetGenericArguments()[0]
            .GetProperties(BindingFlags.Instance & BindingFlags.Public);

        var columnWidths = propertyInfos
            .Select(i => new KeyValuePair<string, int>(i.Name, i.Name.Length))
            .ToDictionary();

        for (var i = 0; i < columnWidths.Keys.Count() ; i++)
        {
            var kvp = columnWidths.ElementAt(i);
            Console.Write($"{kvp.Key} ".PadRight(kvp.Value));
            if (i < columnWidths.Count() - 1)
            {
                Console.Write(" | ");
            }
        }

        foreach (var entry in src)
        {
            foreach (var propertyInfo in propertyInfos)
            {
                Console.Write($"{propertyInfo.GetValue(entry)}");
                if (propertyInfo != propertyInfos.Last())
                {
                    Console.Write(" | ");
                }
            }
            Console.WriteLine();
        }
        Console.WriteLine();

    }

    #region Spinner
    private static readonly string[] spinner = new[] { "|", "/", "-", "\\" };
    private static int spinnerIndex;

    public static void IncrementSpinner()
    {

        if (spinnerIndex > spinner.Length - 1)
        {
            spinnerIndex = 0;
        }
        Console.Write($"{spinner[spinnerIndex++]}");
        Console.CursorLeft -= 1;

    }

    public static void StartSpinner()
    {

        Console.Write(" ");

    }

    public static void StopSpinner()
    {
        Console.WriteLine(" ");
    }
    #endregion Spinner

    #region Highlighting
    private static ConsoleColor originalForegroundColor;
    private static ConsoleColor originalBackgroundColor;

    private static ConsoleColor highlightedForegroundColor;
    private static ConsoleColor highlightedBackgroundColor;

    public static void SetHighlightColors(ConsoleColor foregroundColor, ConsoleColor backgroundColor)
    {
        highlightedForegroundColor = foregroundColor;
        highlightedBackgroundColor = backgroundColor;
    }

    public static void ToggleHighlightOn()
    {
        originalForegroundColor = Console.ForegroundColor;
        originalBackgroundColor = Console.BackgroundColor;

        Console.BackgroundColor = highlightedBackgroundColor;
        Console.ForegroundColor = highlightedForegroundColor;
    }

    public static void ToggleHighlightOff()
    {
        Console.BackgroundColor = originalBackgroundColor;
        Console.ForegroundColor = originalForegroundColor;
    }
    #endregion Highlighting

    #region Inputs
    public static int GetIntegerInput(string errorMessage = "Invalid input.  Please try again.")
    {

        do
        {
            var rawInput = Console.ReadLine() ?? string.Empty;
            var trimmedInput = rawInput.Trim();
            if (int.TryParse(trimmedInput, out var value))
            {
                return value;
            }
            Console.WriteLine(errorMessage);
        } while (true);

    }

    public static string GetStringInput(string errorMessage = "Invalid input.  Please try again.")
    {

        do
        {
            var rawInput = Console.ReadLine();
            var trimmedInput = rawInput?.Trim().ToUpperInvariant();
            if (!string.IsNullOrWhiteSpace(trimmedInput))
            {
                return trimmedInput;
            }
            Console.WriteLine(errorMessage);
        } while (true);

    }

    public static decimal GetDecimalInput(string errorMessage = "Invalid input.  Please try again.")
    {

        do
        {
            var rawInput = Console.ReadLine() ?? string.Empty;
            var trimmedInput = rawInput.Trim();
            if (decimal.TryParse(trimmedInput, out var value))
            {
                return value;
            }
            Console.WriteLine(errorMessage);
        } while (true);

    }

    public static (bool Exit, bool invalidInput) GetYesNoInput()
    {

        var input = GetStringInput().ToUpperInvariant();
        return input switch
        {
            "Y" or "YES" => (true, true),
            "N" or "NO" => (false, true),
            _ => (false, false)
        };

    }
    #endregion Inputs

}