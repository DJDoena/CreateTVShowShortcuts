using System;

namespace DoenaSoft.CreateShortcuts.Interfaces.ObjectStorage
{
    public interface ITupleStorage
    {
        ITuple GetTuple(String article
            , Boolean articleIsPrefix);
    }
}