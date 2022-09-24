using System;
using System.Collections.Generic;
using DoenaSoft.CreateShortcuts.Interfaces;
using DoenaSoft.CreateShortcuts.Interfaces.ObjectStorage;
using DoenaSoft.CreateShortcuts.Interfaces.Processors;

namespace DoenaSoft.CreateShortcuts.Implementation.Processors
{
    internal sealed partial class ArticleProcessor : IArticleProcessor
    {
        private static IEnumerable<string> Articles
        {
            get
            {
                yield return "The";
                yield return "A";
                yield return "An";
                yield return "Der";
                yield return "Das";
            }
        }

        public string SeriesName { get; private set; }

        private readonly bool ArticleIsPrefix;

        private readonly IObjectStorage ObjectStorage;

        private bool WasProcessed;

        public ArticleProcessor(string seriesName, bool articleIsPrefix, IObjectStorage os)
        {
            ArticleIsPrefix = articleIsPrefix;
            SeriesName = seriesName;
            ObjectStorage = os;
            WasProcessed = false;
        }

        public void Process()
        {
            if (ObjectStorage.ArgumentsProcessor.AppendArticles && WasProcessed == false)
            {
                var replaceFunc = GetReplaceFunc();

                foreach (var article in Articles)
                {
                    var tuple = ObjectStorage.GetTuple(article, ArticleIsPrefix);

                    if (replaceFunc(tuple))
                    {
                        break;
                    }
                }

                WasProcessed = true;
            }
        }

        private Func<ITuple, bool> GetReplaceFunc()
        {
            var replaceFunc = ArticleIsPrefix ? (Func<ITuple, bool>)ReplacePrefix : ReplaceSuffix;

            return replaceFunc;
        }

        private bool ReplacePrefix(ITuple tuple)
        {
            var replaced = false;

            if (SeriesName.StartsWith(tuple.Source, StringComparison.InvariantCultureIgnoreCase))
            {
                SeriesName = SeriesName.Substring(tuple.Source.Length) + tuple.Target;
                replaced = true;
            }

            return replaced;
        }

        private bool ReplaceSuffix(ITuple tuple)
        {
            var replaced = false;

            if (SeriesName.EndsWith(tuple.Source, StringComparison.InvariantCultureIgnoreCase))
            {
                SeriesName = tuple.Target + SeriesName.Substring(0, SeriesName.Length - tuple.Source.Length);
                replaced = true;
            }

            return replaced;
        }
    }
}