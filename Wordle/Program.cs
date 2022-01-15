using static System.Console;

var input = "stare";

var absent = new HashSet<char>();
absent.Add('s');
absent.Add('t');
absent.Add('r');
absent.Add('e');
absent.Add('m');
absent.Add('g');
absent.Add('q');
absent.Add('j');
absent.Add('k');
absent.Add('v');
absent.Add('h');
absent.Add('u');
absent.Add('o');

var present = new Dictionary<char, List<int>>();
present.Add('a', new List<int> {2,3,4});
present.Add('c', new List<int> {0,3});
present.Add('p', new List<int> {2});
present.Add('n', new List<int> {4});

var correct = new char[5];
correct[1] = 'a';

var nextWordIsGuessed = false;
var count = 0;
var wordle = new DotNETApps.Wordle(absent, present, correct);

wordle.SetGuessedWords("stare");
wordle.SetGuessedWords("magma");
wordle.SetGuessedWords("qajaq");
wordle.SetGuessedWords("vauch");
wordle.SetGuessedWords("capon");

while (!nextWordIsGuessed && count < 12972)
{
    nextWordIsGuessed = wordle.TryGetNextWord(out var nextWord);
    wordle.SetGuessedWords(nextWord);
    WriteLine($"{count++}:{nextWord}");
}