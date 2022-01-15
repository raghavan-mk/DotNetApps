namespace DotNETApps;

public class Wordle
{
    private readonly string[] _corpus;
    private HashSet<string> _guessedWords = new();
    private readonly HashSet<char> _absent = new();
    private readonly Dictionary<char, List<int>> _present = new();
    private readonly char[] _correct = new char[5];
    private HashSet<int> _guessedNumbers = new();

    public Wordle(HashSet<char> absent, Dictionary<char, List<int>> present, char[] correct)
    {
        _corpus = Corpus.GetCorpus();
        _absent = absent;
        _present = present;
        _correct = correct;
    }
    public bool TryGetNextWord(out string nextWord)
    {
        var random = GetNextRandomNumber();
        var nextGuess = _corpus[random];
        nextWord = nextGuess;

        if (_guessedWords.Contains(nextGuess))
            return false;

        for (var i = 0; i < nextGuess.Length; i++)
        {
            if (!KeepTheLetter(nextGuess[i], i))
                return false;
        }

        return KeepTheWord(nextGuess);
    }
    
    public void SetGuessedWords(string guessedWord) => 
        _guessedWords.Add(guessedWord);

    bool KeepTheWord(string word)
    {
        for (var i = 0; i < _correct.Length; i++)
        {
            var c = _correct[i];
            if (c == '\0') continue;
            if (word.IndexOf(c) != i)
                return false;
        }

        return _present.Keys.All(word.Contains);
    }

    bool KeepTheLetter(char letter, int index)
    {
        // if (_correctLetters!.Contains(letter))
        //     return true;

        if (!_present!.ContainsKey(letter))
            return !_absent!.Contains(letter);

        return !_present[letter].Contains(index);
    }

    int GetNextRandomNumber()
    {
        var nxtNumber = new Random().Next(0, 12972);
        if (_guessedNumbers.Contains(nxtNumber))
            return GetNextRandomNumber();
        _guessedNumbers.Add(nxtNumber);
        return nxtNumber;
    }
}

internal static class Corpus
{
    private static readonly string[] Words;
    static Corpus()
    {
        Words = File.ReadAllText("corpus.txt").Split(',')
            .Select(w => w.Replace("\"", "").Trim()).ToArray();
    }
    
    public static string[] GetCorpus() => Words;
}