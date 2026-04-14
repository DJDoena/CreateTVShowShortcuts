using DoenaSoft.CreateShortcuts.Interfaces.ObjectStorage;

namespace DoenaSoft.CreateShortcuts.Implementation.ObjectStorage;

internal abstract class SpecialStorage
{
    protected readonly IObjectStorage ObjectStorage;

    protected readonly IObjectFactory ObjectFactory;

    protected SpecialStorage(IObjectStorage os, IObjectFactory of)
    {
        ObjectStorage = os;
        ObjectFactory = of;
    }
}