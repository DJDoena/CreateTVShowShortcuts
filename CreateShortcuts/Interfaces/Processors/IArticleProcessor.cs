using System;

namespace DoenaSoft.CreateShortcuts.Interfaces.Processors
{
    public interface IArticleProcessor : IProcessor
    {
        String SeriesName { get; }
    }
}