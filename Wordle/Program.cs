using static System.Console;

var absent = new HashSet<char>
{
    't', 'e', 'b', 'i', 'f', 'n'
};

var present = new Dictionary<char, List<int>>
{
    {'a', new List<int> {2}},
    {'r', new List<int> {3}}
};

var correct = new char[5];
correct[0] = 's';
correct[1] = 'o';
correct[3] = 'a';
correct[4] = 'r';
Guess();

void Guess()
{
    var nextWordIsGuessed = false;
    var count = 0;
    var wordle = new DotNETApps.Wordle(absent!, present!, correct!);
    wordle.SetGuessedWords("stare");
    wordle.SetGuessedWords("sabir");
    wordle.SetGuessedWords("sofar");
    wordle.SetGuessedWords("sonar");
    while (!nextWordIsGuessed && count < 12972)
    {
        nextWordIsGuessed = wordle.TryGetNextWord(out var nextWord);
        WriteLine($"{count++}:{nextWord}");
    }
}