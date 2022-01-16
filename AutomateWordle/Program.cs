// See https://aka.ms/new-console-template for more information
using PuppeteerSharp;
using PuppeteerSharp.Input;

Console.WriteLine("Hello, Wordle!");

await new BrowserFetcher().DownloadAsync();
var browser = await Puppeteer.LaunchAsync(new LaunchOptions
{
    Headless = false
});

await using var page = await browser.NewPageAsync();
await page.GoToAsync("https://www.powerlanguage.co.uk/wordle/");
var element = await page.WaitForSelectorAsync("body > game-app");
await element.ClickAsync();

await page.TypeAsync("body > game-app","STARE");
await page.Keyboard.PressAsync(Key.Enter);

var js = await page
    .EvaluateExpressionHandleAsync("document.querySelector(\"body > game-app\")." +
                                   "shadowRoot.querySelector(\"#board > game-row:nth-child(1)\")." +
                                   "shadowRoot.querySelector(\"div > game-tile:nth-child(3)\")." +
                                   "shadowRoot.querySelector(\"div\")")
                                    as ElementHandle;

var properties = await js.GetPropertiesAsync();

var tiles = properties.ToArray();
await Task.Delay(10000);
await page.CloseAsync();


