namespace DoenaSoft.CreateShortcuts.Interfaces.Processors
{
    public interface IArgumentsProcessor : IProcessor
    {
        bool AppendArticles { get; }

        string LogFile { get; }

        bool DualLog { get; }

        bool ShowHelp { get; }

        void PrintArguments();
    }
}