using PuppeteerSharp;
using PuppeteerSharp.Input;
using System.Drawing;
using System.Globalization;
using System.Text.RegularExpressions;
using static System.Console;

await new BrowserFetcher().DownloadAsync();
var browser = await Puppeteer.LaunchAsync(new LaunchOptions
{
    Headless = true
});

await using var page = await browser.NewPageAsync();
await page.GoToAsync("https://www.powerlanguage.co.uk/wordle/");
var element = await page.WaitForSelectorAsync("body > game-app");
await element.ClickAsync();

await page.TypeAsync("body > game-app", "STARE");
await page.Keyboard.PressAsync(Key.Enter);
await Task.Delay(1300);
var row = 1;
List<string> colors = new();
for (var i = 1; i < 6; i++)
{
    var rgb = await GetColor(row, i);
    colors.Add(rgb);
}

await page.CloseAsync();

foreach (var color in colors)
{
    var rgb = ParseRgb(color);
    WriteLine(GetRgbColor(rgb));
}

// async Task<string> GetText(int row, int cell) =>
//     await page.EvaluateExpressionAsync<string>($"document.querySelector(\"body > game-app\")." +
//                                                $"shadowRoot.querySelector(\"#board > game-row:nth-child({row})\")." +
//                                                $"shadowRoot.querySelector(\"div > game-tile:nth-child({cell})\")." +
//                                                "shadowRoot.querySelector(\"div\").innerText");


async Task<string> GetColor(int row, int cell) =>
    await page.EvaluateExpressionAsync<string>($"window.getComputedStyle(" +
                                               "document.querySelector(\"body > game-app\")." +
                                               $"shadowRoot.querySelector(\"#board > game-row:nth-child({row})\")." +
                                               $"shadowRoot.querySelector(\"div > game-tile:nth-child({cell})\")." +
                                               "shadowRoot.querySelector(\"div\")).getPropertyValue(\"background-color\")");


int[] ParseRgb(string color) =>
    Regex.Split(color, @"rgb\((\d{1,3}),\s*(\d{1,3}),\s*(\d{1,3})\)")
        .Where(c => c != "")
        .Select(int.Parse)
        .ToArray();

string GetRgbColor(int[] rgb) => Color.FromArgb(rgb[0], rgb[1], rgb[2]).Name;


// ff538d4e C
// ff3a3a3c A
// ffb59f3b P
// ffb59f3b P
// ff3a3a3c A

// ff6aaa64 C
// ff787c7e A
// ffc9b458 P
// ffc9b458 P
// ff787c7e A
