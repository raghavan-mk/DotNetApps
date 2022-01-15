using static System.Console;

var input = "stare";

var absent = new HashSet<char>();
absent.Add('s');
absent.Add('t');
absent.Add('r');
absent.Add('e');
absent.Add('l');

var present = new Dictionary<char, List<int>>();
present.Add('a', new List<int> {2, 3});
present.Add('i', new List<int> {1});
present.Add('c', new List<int> {2});

var correct = new char[5];
correct[0] = 'p';
var nextWordIsGuessed = false;
var count = 0;
var wordle = new DotNETApps.Wordle(absent, present, correct);
wordle.SetGuessedWords("stare");
wordle.SetGuessedWords("pical");
while (!nextWordIsGuessed && count < 12972)
{
    nextWordIsGuessed = wordle.TryGetNextWord(out var nextWord);
    wordle.SetGuessedWords(nextWord);
    WriteLine($"{count++}:{nextWord}");
}

//
//
// guessedWords.Add("stare");
// guessedWords.Add("anted");
// guessedWords.Add("agley");
//
// absentLetters.Add('s');
// absentLetters.Add('r');
// absentLetters.Add('e');
// absentLetters.Add('n');
// absentLetters.Add('t');
// absentLetters.Add('d');
// absentLetters.Add('g');
// absentLetters.Add('l');

//
// presentLetters.Add('t', new List<int> {1});
// presentLetters.Add('a', new List<int> {2});
// presentLetters.Add('e', new List<int> {4});
//
// correctLetters[0] = 'a';
// correctLetters[1] = 'a';
// correctLetters[2] = 'n';
// correctLetters[3] = 'e';
// correctLetters[4] = 'y';