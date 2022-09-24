using System;

namespace DoenaSoft.CreateShortcuts.Interfaces.Processors
{
    public interface IArgumentsProcessor : IProcessor
    {
        Boolean AppendArticles { get; }

        String LogFile { get; }

        Boolean DualLog { get; }

        Boolean ShowHelp { get; }
        
        void PrintArguments();
    }
}