using System.Drawing;
using System.Text.RegularExpressions;
using PuppeteerSharp;

namespace AutomateWordle;

public static class Helper
{
    public static async Task<string> GetColor(Page page, int row, int cell) =>
        await page.EvaluateExpressionAsync<string>($"window.getComputedStyle(" +
                                                   "document.querySelector(\"body > game-app\")." +
                                                   $"shadowRoot.querySelector(\"#board > game-row:nth-child({row})\")." +
                                                   $"shadowRoot.querySelector(\"div > game-tile:nth-child({cell})\")." +
                                                   "shadowRoot.querySelector(\"div\")).getPropertyValue(\"background-color\")");

    public static string ParseRgb(string color)
    {
        var rgb = Regex.Split(color, @"rgb\((\d{1,3}),\s*(\d{1,3}),\s*(\d{1,3})\)")
            .Where(c => c != "") // for some reason spaces were added too so we need to remove them
            .Select(int.Parse)
            .ToArray();
        return GetColorCode(rgb);
    }

    public static string GetColorCode(IReadOnlyList<int> rgb) => Color.FromArgb(rgb[0], rgb[1], rgb[2]).Name;

    public static char EvalColorCode(string color) => color switch
    {
        // color code shows up differently when run in headless and non-headless mode
        "ff538d4e" or "ff6aaa64" => 'c',
        "ff3a3a3c" or "ff787c7e" => 'a',
        "ffb59f3b" or "ffc9b458" => 'p',
        _ => throw new ArgumentOutOfRangeException(nameof(color), color, null)
    };
}