using Spectre.Console;

namespace WordleWeb;

public class Output
{
    private readonly Table _table;
    private readonly FigletFont _font;

    public Output()
    {
        _table = new Table
        {
            Border = TableBorder.Ascii
        }.HideHeaders();
        _table.AddColumns("", "", "", "", "");
        _font = FigletFont.Load(@"assets/chunky.flf");
    }

    public void Addrow(string word, List<string> color)
    {
        _table.Clear();

        var rows = new List<Markup>();
        for (var i = 0; i < word.Length; i++)
        {
            var c = word[i].ToString().PadBoth(5);
            switch (color[i])
            {
                case "ff3a3a3c" or "ff787c7e":
                    rows.Add(new Markup($"[black on white]{c}[/]"));
                    break;
                case "ff6aaa64" or "ff538d4e":
                    rows.Add(new Markup($"[black on rgb(83,141,78)]{c}[/]"));
                    break;
                case "ffb59f3b" or "ffc9b458":
                    rows.Add(new Markup($"[black on rgb(181,159,59)]{c}[/]"));
                    break;
            }
        }

        _table.AddRow(rows);
        AnsiConsole.Write(_table);
    }

    public void OutputInAscii(string output) =>
        AnsiConsole.Write(
            new FigletText(_font, output)
                .LeftAligned()
                .Color(Color.Green));
}

public static class Extensions
{
    public static string PadBoth(this string str, int length)
    {
        int spaces = length - str.Length;
        int padLeft = spaces / 2 + str.Length;
        return str.PadLeft(padLeft).PadRight(length);
    }

    public static void Clear(this Table table)
    {
        for (var i = 0; i < table.Rows.Count; i++)
        {
            table.Rows.RemoveAt(i);
        }
    }
}