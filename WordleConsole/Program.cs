using static System.Console;

var absent = new HashSet<char>
{
    't', 'a'
};

var present = new Dictionary<char, List<int>>();


var correct = new char[5];
correct[0] = 's';
correct[3] = 'r';
correct[4] = 'e';
Guess();

void Guess()
{
    var wordIsGuessed = false;
    var count = 0;
    var wordle = new DotNETApps.Wordle(absent!, present!, correct!);
    wordle.SetGuessedWords("stare");
    while (!wordIsGuessed && count < 12972)
    {
        wordIsGuessed = wordle.TryGetNextWord(out var nextWord);
        WriteLine($"{count++}:{nextWord}");
    }
}