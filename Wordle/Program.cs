using System.Globalization;
using System.Net.Mime;
using static System.Console;

var input = "stare";

var absent = new HashSet<char>();
absent.Add('s');
absent.Add('t');
absent.Add('r');
absent.Add('e');
absent.Add('m');
absent.Add('l');
var present = new Dictionary<char, List<int>>();
present.Add('a', new List<int> {0, 2});

var correct = new char[5];
correct[1] = 'a';
correct[3] = 'i';
correct[4] = 'c';
Play();

void Play()
{
    var nextWordIsGuessed = false;
    var count = 0;
    var wordle = new DotNETApps.Wordle(absent!, present!, correct!);
    wordle.SetGuessedWords("stare");
    wordle.SetGuessedWords("aulic");
    wordle.SetGuessedWords("malic");
    while (!nextWordIsGuessed && count < 12972)
    {
        nextWordIsGuessed = wordle.TryGetNextWord(out var nextWord);
        WriteLine($"{count++}:{nextWord}");
    }
}