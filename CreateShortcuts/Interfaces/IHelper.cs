using System;
using DoenaSoft.AbstractionLayer.IOServices;

namespace DoenaSoft.CreateShortcuts.Interfaces
{
    public interface IHelper
    {
        Boolean IsSpecialFolder(IFolderInfo di);
    }
}