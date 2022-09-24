using DoenaSoft.CreateShortcuts.Interfaces.Processors;
using System;

namespace DoenaSoft.CreateShortcuts.Interfaces.ObjectStorage
{
    public interface IArticleProcessorStorage
    {
        IArticleProcessor GetArticleProcessor(String seriesName
            , Boolean articleIsPrefix);
    }
}