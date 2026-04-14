using DoenaSoft.AbstractionLayer.IOServices;

namespace DoenaSoft.CreateShortcuts.Interfaces;

public interface IHelper
{
    bool IsSpecialFolder(IFolderInfo di);
}