using System.Collections.Generic;
using DoenaSoft.CreateShortcuts.Interfaces;
using DoenaSoft.CreateShortcuts.Interfaces.ObjectStorage;

namespace DoenaSoft.CreateShortcuts.Implementation.ObjectStorage
{
    internal sealed class TupleStorage : SpecialStorage, ITupleStorage
    {
        private Dictionary<string, Dictionary<bool, ITuple>> Tuples { get; set; }

        public TupleStorage(IObjectStorage os, IObjectFactory of) : base(os, of)
        {
        }

        public ITuple GetTuple(string article, bool articleIsPrefix)
        {

            if (this.Tuples == null)
            {
                this.Tuples = new Dictionary<string, Dictionary<bool, ITuple>>();
            }

            if (this.Tuples.TryGetValue(article, out var innerDict) == false)
            {
                innerDict = new Dictionary<bool, ITuple>();
                this.Tuples[article] = innerDict;
            }

            innerDict.TryGetValue(articleIsPrefix, out var tuple);

            if (tuple == null)
            {
                tuple = ObjectFactory.CreateTuple(article, articleIsPrefix);

                innerDict[articleIsPrefix] = tuple;
            }

            return tuple;
        }
    }
}