using WordleLib;
using static System.Console;

var cmdLineArgs = Environment.GetCommandLineArgs();
_ = cmdLineArgs.Select(c =>
{
    WriteLine(c);
    return c;
});