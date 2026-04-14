using System.Collections.Generic;
using DoenaSoft.CreateShortcuts.Interfaces.ObjectStorage;
using DoenaSoft.CreateShortcuts.Interfaces.Processors;

namespace DoenaSoft.CreateShortcuts.Implementation.ObjectStorage;

internal sealed class ArticleProcessorStorage : SpecialStorage, IArticleProcessorStorage
{
    private Dictionary<string, Dictionary<bool, IArticleProcessor>> ArticleProcessors { get; set; }

    public ArticleProcessorStorage(IObjectStorage os, IObjectFactory of) : base(os, of)
    {
    }

    public IArticleProcessor GetArticleProcessor(string seriesName, bool articleIsPrefix)
    {

        if (this.ArticleProcessors == null)
        {
            this.ArticleProcessors = new Dictionary<string, Dictionary<bool, IArticleProcessor>>();
        }

        if (this.ArticleProcessors.TryGetValue(seriesName, out var innerDict) == false)
        {
            innerDict = new Dictionary<bool, IArticleProcessor>();
            this.ArticleProcessors[seriesName] = innerDict;
        }

        innerDict.TryGetValue(articleIsPrefix, out var processor);

        if (processor == null)
        {
            processor = ObjectFactory.CreateArticleProcessor(seriesName, articleIsPrefix, ObjectStorage);

            innerDict[articleIsPrefix] = processor;
        }

        return processor;
    }
}