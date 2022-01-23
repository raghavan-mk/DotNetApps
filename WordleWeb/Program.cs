using PuppeteerSharp;
using PuppeteerSharp.Input;
using Spectre.Console;
using WordleWeb;
using static System.Console;

BrowserFetcher? browserFetcher = null;
Browser? browser = null;
Page? page = null;

try
{
    const string wordleUrl = "https://www.powerlanguage.co.uk/wordle/";
    var headless = true;
    var currentWord = "stare";

    var cmdLineArgs = Environment.GetCommandLineArgs();
    if (cmdLineArgs.Length > 1)
    {
        cmdLineArgs.Skip(1).ToList().ForEach(c =>
        {
            if (c.StartsWith("-h="))
            {
                var t = bool.TryParse(c.Split('=')[1], out headless);
            }

            if (c.StartsWith("-w="))
            {
                if (c.Split('=')[1].Length == 5)
                {
                    currentWord = c.Split('=')[1];
                }
            }
        });
    }

    browserFetcher = new BrowserFetcher();
    browserFetcher.DownloadProgressChanged += (sender, args) =>
    {
        AnsiConsole.Markup(
            $"\r [bold green] Downloading Chromium Driver[/] [bold red]{args.ProgressPercentage}%[/]");
    };

    await browserFetcher.DownloadAsync();
    browser = await Puppeteer.LaunchAsync(new LaunchOptions
    {
        Headless = headless
    });

    Clear();
    var output = new Output();
    output.OutputInAscii("Wordle");

    page = await browser.NewPageAsync();
    await page.GoToAsync(wordleUrl);

    var element = await
        page.EvaluateExpressionHandleAsync(
            "document.querySelector(\"body > game-app\").shadowRoot.querySelector(\"#game > game-modal\")" +
            ".shadowRoot.querySelector(\"div > div > div > game-icon\")" +
            ".shadowRoot.querySelector(\"svg\")") as ElementHandle;

    await Task.Delay(100);
    await element?.ClickAsync()!;

    List<string> colors = new(5);
    var wordle = new Wordle(currentWord);
    var completed = false;
    var attempt = 0;


// number of attempts to find a word - 6 is the max
// also means to increment the row count by 1 to read next word
    for (var i = 1; i < 7; i++)
    {
        await page.TypeAsync("body > game-app", currentWord);
        await page.Keyboard.PressAsync(Key.Enter);
        await Task.Delay(1500); // delay to allow the word to be typed and get the colors of the word

        for (var j = 1; j < 6; j++)
        {
            var rgb = await Helper.GetColor(page, i, j);
            await Task.Delay(100);
            colors.Add(Helper.ParseRgb(rgb));
        }

        output.Addrow(currentWord, colors);
        // dark and non-dark mode   
        if (!colors.All(c => c is "ff538d4e" or "ff6aaa64"))
            GetNextWord();
        else
        {
            completed = true;
            attempt = i;
            break;
        }

        colors.Clear();
    }
//await Task.Delay(1000);

    if (completed)
    {
        output.OutputInAscii($"{Helper.GetAppreciation(attempt - 1)}!");
    }
    else
    {
        AnsiConsole.Write(new Markup($"[bold red]Could not find the word[/]"));
        WriteLine();
        var wordleOfTheDay = await Helper.GetWordleSln(page);
        AnsiConsole.Write(new Markup($"[bold yellow]Solution[/] [bold green]{wordleOfTheDay}[/]"));
    }

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
        while (!wordIsGuessed && count++ < 12792)
        {
            wordIsGuessed = wordle.TryGetNextWord(out currentWord);
            // WriteLine($"{count++}:{currentWord}");
        }
    }
}
catch (Exception e)
{
    AnsiConsole.WriteException(e);
}
finally
{
    page?.Dispose();
    browser?.Dispose();
    browserFetcher?.Dispose();
}

// dark mode colors
// ff538d4e C
// ff3a3a3c A
// ffb59f3b P
// ffb59f3b P
// ff3a3a3c A

// non-dark mode colors
// ff6aaa64 C
// ff787c7e A
// ffc9b458 P
// ffc9b458 P
// ff787c7e A