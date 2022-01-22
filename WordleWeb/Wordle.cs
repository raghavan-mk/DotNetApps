namespace WordleWeb;

public class Wordle
{
    private readonly string[] _corpus;
    private readonly HashSet<string> _guessedWords = new();
    public HashSet<char> Absent { get; } = new();

    //letters present but not in right position
    public Dictionary<char, List<int>> Present { get; }= new();

    //letters present and in right position
    public char[] Correct { get; } = new char[5];

    private readonly HashSet<int> _guessedNumbers = new();

    public Wordle(string firstGuess = "")
    {
        _corpus = Corpus.GetCorpus();
        
        if (!string.IsNullOrEmpty(firstGuess))
            _guessedWords.Add(firstGuess);
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
        if (Correct.Where((c, i) => c != '\0' && word.IndexOf(c) != i).Any())
            return false;

        // ensure all present letters are present
        return Present.Keys.All(word.Contains);
    }

    bool KeepTheLetter(char letter, int index)
    {
        //if a letter is not present in both present as well as absent then it is kept
        if (!Present!.ContainsKey(letter))
            return !Absent!.Contains(letter);

        //if present ensure, it is present in a different position/index
        return !Present[letter].Contains(index);
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
}