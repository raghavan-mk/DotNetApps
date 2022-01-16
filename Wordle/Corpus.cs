namespace DotNETApps;

internal static partial class Corpus
{
    private static readonly string[] Words;

    static Corpus()
    {
        Words = File.ReadAllText("corpus.txt").Split(',')
            .Select(w => w.Replace("\"", "").Trim()).ToArray();
    }

    public static string[] GetCorpus() => Words;
}