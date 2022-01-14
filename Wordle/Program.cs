using System.Runtime.CompilerServices;
using System.Threading.Tasks.Sources;
using static System.Console;

var words =
    File.ReadAllText("words.txt").Split(',')
        .Select(w => w.Replace("\"", "").Trim()).ToArray();

var guessedWords = new HashSet<string>();
var absentLetters = new HashSet<char>();
var presentLetters = new Dictionary<char, List<int>>();
var correctLetters = new char[5];
var randomNumberGuessed = new HashSet<int>();

guessedWords.Add("stare");
guessedWords.Add("cagot");
guessedWords.Add("tangi");
guessedWords.Add("tanga");

absentLetters.Add('s');
absentLetters.Add('r');
absentLetters.Add('e');
absentLetters.Add('c');
absentLetters.Add('o');
absentLetters.Add('i');

//
presentLetters.Add('t', new List<int> {1, 4});
presentLetters.Add('a', new List<int> {2, 4});
presentLetters.Add('g', new List<int> {2});
//
correctLetters[0] = 't';
correctLetters[1] = 'a';
correctLetters[2] = 'n';
correctLetters[3] = 'g';
// correctLetters[4] = 'y';

bool tillNextwordIsGuessed = false;
int count = 0;
while (!tillNextwordIsGuessed && count < 12972)
{
    tillNextwordIsGuessed = TryGetNextWord(out var nextWord);
    WriteLine($"{count++}:{nextWord}");
}

bool TryGetNextWord(out string nextWord)
{
    var random = GetNextRandomNumber();
    var nextGuess = words[random];
    nextWord = nextGuess;

    if (guessedWords.Contains(nextGuess))
        return false;

    for (var i = 0; i < nextGuess.Length; i++)
    {
        if (!KeepTheLetter(nextGuess[i], i))
            return false;
    }

    return KeepTheWord(nextGuess);
}

bool KeepTheWord(string word)
{
    for (var i = 0; i < correctLetters.Length; i++)
    {
        var c = correctLetters[i];
        if (c == '\0') continue;
        if (word.IndexOf(c) != i)
            return false;
    }

    return presentLetters.Keys.All(c => word.Contains(c));
}

bool KeepTheLetter(char letter, int index)
{
    // if (correctLetters!.Contains(letter))
    //     return true;

    if (!presentLetters!.ContainsKey(letter)) return !absentLetters!.Contains(letter);

    return !presentLetters[letter].Contains(index);
}


int GetNextRandomNumber()
{
    var nxtNumber = new Random().Next(0, 12972);
    if (randomNumberGuessed.Contains(nxtNumber)) return GetNextRandomNumber();
    randomNumberGuessed.Add(nxtNumber);
    return nxtNumber;
}