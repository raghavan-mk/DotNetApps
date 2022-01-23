namespace WordleWeb;

internal static partial class Corpus
{
    private static readonly string[] Words;

    static Corpus()
    {
        Words = File.ReadAllText(@"assets/corpus.txt").Split(',')
            .Select(w => w.Replace("\"", "").Trim()).ToArray();
    }

    public static string[] GetCorpus() => Words;
}