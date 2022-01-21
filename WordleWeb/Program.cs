using PuppeteerSharp;
using PuppeteerSharp.Input;
using System.Drawing;
using System.Globalization;
using System.Text.RegularExpressions;
using AutomateWordle;
using DotNETApps;
using static System.Console;

await new BrowserFetcher().DownloadAsync();
var browser = await Puppeteer.LaunchAsync(new LaunchOptions
{
    Headless = false
});

await using var page = await browser.NewPageAsync();
await page.GoToAsync("https://www.powerlanguage.co.uk/wordle/");
var element = await page.WaitForSelectorAsync("body > game-app");
await element.ClickAsync();

var currentWord = "stare";

await page.TypeAsync("body > game-app", currentWord);
await page.Keyboard.PressAsync(Key.Enter);
await Task.Delay(1500);
var row = 1;

List<string> colors = new();
for (var i = 1; i < 6; i++)
{
    var rgb = await Helper.GetColor(page, row, i);
    colors.Add(rgb);
}

await page.CloseAsync();

var absent = new HashSet<char>();
var present = new Dictionary<char, List<int>>();
var correct = new char[5];

for (var i = 0; i < colors.Count; i++)
{
    var c = Helper.EvalColorCode(colors[i]);
    var cl = currentWord[i];
    switch (c)
    {
        case 'c':
            correct[i] = cl;
            break;
        case 'a' when !correct.Contains(cl)
                      && !absent.Contains(cl):
            absent.Add(cl);
            break;
        case 'p':
        {
            if (!present.ContainsKey(cl))
                present.Add(cl, new List<int> {i});
            present[c].Add(i);
            break;
        }
    }
}

var w = new Wordle(correct, absent, present);
w.SetGuessedWords(currentWord);
var wordIsGuessed = false;
var count = 0;
while (wordIsGuessed && count < 12792)
{
    wordIsGuessed = w.TryGetNextWord(out currentWord);
    WriteLine($"{count++}:{currentWord}");
}


// async Task<string> GetText(int row, int cell) =>
//     await page.EvaluateExpressionAsync<string>($"document.querySelector(\"body > game-app\")." +
//                                                $"shadowRoot.querySelector(\"#board > game-row:nth-child({row})\")." +
//                                                $"shadowRoot.querySelector(\"div > game-tile:nth-child({cell})\")." +
//                                                "shadowRoot.querySelector(\"div\").innerText");


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