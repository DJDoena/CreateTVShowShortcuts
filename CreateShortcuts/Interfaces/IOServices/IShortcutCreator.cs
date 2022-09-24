using System;

namespace DoenaSoft.CreateShortcuts.Interfaces.IOServices
{
    public interface IShortcutCreator
    {
        String Create(String seriesFolderForShortcutFiles
            , String seasonFolder);
    }
}