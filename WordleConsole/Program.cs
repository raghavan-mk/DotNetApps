using static System.Console;

var absent = new HashSet<char>
{
    's', 't', 'a', 'e', 'd', 'u', 'm', 'l' //add to absent if not present in correct
};

var present = new Dictionary<char, List<int>>();
present.Add('r', new List<int> {3});
present.Add('i', new List<int> {3});

var correct = new char[5];
correct[0] = 'p';
correct[1] = 'r';
correct[2] = 'i';
// correct[3] = 'r';
// correct[4] = 'e';
Guess();

void Guess()
{
    var wordIsGuessed = false;
    var count = 0;
    var wordle = new DotNETApps.Wordle(correct!, absent!, present!);
    wordle.SetGuessedWords("stare");
    wordle.SetGuessedWords("druid");
    wordle.SetGuessedWords("primp");
    wordle.SetGuessedWords("prill");
    while (!wordIsGuessed && count < 12972)
    {
        wordIsGuessed = wordle.TryGetNextWord(out var nextWord);
        WriteLine($"{count++}:{nextWord}");
    }
}