namespace DoenaSoft.CreateShortcuts.Interfaces.ObjectStorage
{
    public interface ITupleStorage
    {
        ITuple GetTuple(string article
            , bool articleIsPrefix);
    }
}