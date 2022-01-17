namespace DotNETApps;

internal class Wordle
{
    private readonly string[] _corpus;
    private readonly HashSet<string> _guessedWords = new();
    private readonly HashSet<char> _absent;
    
    //letters present but not in right position
    private readonly Dictionary<char, List<int>> _present;
    
    //letters present and in right position
    private readonly char[] _correct;
    
    private readonly HashSet<int> _guessedNumbers = new();

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

        _guessedWords.Add(nextGuess);
        
        return !nextGuess.Where((t, i) => 
            !KeepTheLetter(t, i)).Any() && KeepTheWord(nextGuess);
    }

    // Choose the guessed word has both all the previously correctly guessed and present letters 
    bool KeepTheWord(string word)
    {
        // ensure all correct letters are present
        if (_correct.Where((c, i) => c != '\0' && word.IndexOf(c) != i).Any())
            return false;

        // ensure all present letters are present
        return _present.Keys.All(word.Contains);
    }

    bool KeepTheLetter(char letter, int index)
    {
        //if a letter is not present in both present as well as absent then it is kept
        if (!_present!.ContainsKey(letter))
            return !_absent!.Contains(letter);

        //if present ensure, it is present in a different position/index
        return !_present[letter].Contains(index);
    }

    //returns a random number between 0 and corpus.length 
    //which is not in the guessedNumbers
    int GetNextRandomNumber()
    {
        var nxtNumber = new Random().Next(0, 12972);
        if (_guessedNumbers.Contains(nxtNumber))
            return GetNextRandomNumber();
        _guessedNumbers.Add(nxtNumber);
        return nxtNumber;
    }
    
    public void SetGuessedWords(string word) => _guessedWords.Add(word);
}
