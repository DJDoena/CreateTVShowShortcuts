using DoenaSoft.CreateShortcuts.Interfaces.Processors;

namespace DoenaSoft.CreateShortcuts.Interfaces.ObjectStorage
{
    public interface IArticleProcessorStorage
    {
        IArticleProcessor GetArticleProcessor(string seriesName
            , bool articleIsPrefix);
    }
}