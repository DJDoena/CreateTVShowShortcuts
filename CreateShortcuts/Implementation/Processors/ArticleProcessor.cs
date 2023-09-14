using System;
using System.Collections.Generic;
using DoenaSoft.CreateShortcuts.Interfaces;
using DoenaSoft.CreateShortcuts.Interfaces.ObjectStorage;
using DoenaSoft.CreateShortcuts.Interfaces.Processors;

namespace DoenaSoft.CreateShortcuts.Implementation.Processors
{
    internal sealed partial class ArticleProcessor : IArticleProcessor
    {
        private readonly bool _articleIsPrefix;

        private readonly IObjectStorage _objectStorage;

        private bool _wasProcessed;

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

        public ArticleProcessor(string seriesName, bool articleIsPrefix, IObjectStorage os)
        {
            _articleIsPrefix = articleIsPrefix;
            this.SeriesName = seriesName;
            _objectStorage = os;
            _wasProcessed = false;
        }

        public void Process()
        {
            if (_objectStorage.ArgumentsProcessor.AppendArticles && _wasProcessed == false)
            {
                var replaceFunc = this.GetReplaceFunc();

                foreach (var article in Articles)
                {
                    var tuple = _objectStorage.GetTuple(article, _articleIsPrefix);

                    if (replaceFunc(tuple))
                    {
                        break;
                    }
                }

                _wasProcessed = true;
            }
        }

        private Func<ITuple, bool> GetReplaceFunc()
        {
            var replaceFunc = _articleIsPrefix ? (Func<ITuple, bool>)this.ReplacePrefix : this.ReplaceSuffix;

            return replaceFunc;
        }

        private bool ReplacePrefix(ITuple tuple)
        {
            var replaced = false;

            if (this.SeriesName.StartsWith(tuple.Source, StringComparison.InvariantCultureIgnoreCase))
            {
                this.SeriesName = this.SeriesName.Substring(tuple.Source.Length) + tuple.Target;

                replaced = true;
            }

            return replaced;
        }

        private bool ReplaceSuffix(ITuple tuple)
        {
            var replaced = false;

            if (this.SeriesName.EndsWith(tuple.Source, StringComparison.InvariantCultureIgnoreCase))
            {
                this.SeriesName = tuple.Target + this.SeriesName.Substring(0, this.SeriesName.Length - tuple.Source.Length);

                replaced = true;
            }

            return replaced;
        }
    }
}