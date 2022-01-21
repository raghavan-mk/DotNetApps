using AutomateWordle;
using PuppeteerSharp;
using PuppeteerSharp.Input;
using WordleLib;

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
List<string> colors = new();
var wordle = new Wordle(currentWord);

for (var i = 1; i < 6; i++)
{
    await page.TypeAsync("body > game-app", currentWord);
    await page.Keyboard.PressAsync(Key.Enter);
    await Task.Delay(1500);

    for (var j = 1; j < 6; j++)
    {
        var rgb = await Helper.GetColor(page, i, j);
        colors.Add(Helper.ParseRgb(rgb));
    }

    if (!colors.All(c => c is "ff538d4e" or "ff6aaa64"))
        GetNextWord();
    else
    {
        Console.WriteLine("Completed");
        break;
    }

    colors.Clear();
}
await Task.Delay(1000);
await page.CloseAsync();

void GetNextWord()
{
    for (var i = 0; i < colors.Count; i++)
    {
        var c = Helper.EvalColorCode(colors[i]);
        var cl = currentWord[i];
        switch (c)
        {
            case 'c':
                wordle.Correct[i] = cl;
                break;
            case 'a' when !wordle.Correct.Contains(cl)
                          && !wordle.Absent.Contains(cl):
                wordle.Absent.Add(cl);
                break;
            case 'p':
            {
                if (!wordle.Present.ContainsKey(cl))
                    wordle.Present.Add(cl, new List<int> {i});
                else
                    wordle.Present[cl].Add(i);
                break;
            }
        }
    }
    
    var wordIsGuessed = false;
    var count = 0;
    while (!wordIsGuessed && count < 12792)
    {
        wordIsGuessed = wordle.TryGetNextWord(out currentWord);
        Console.WriteLine($"{count++}:{currentWord}");
    }
}

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