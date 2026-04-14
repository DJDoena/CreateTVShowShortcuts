namespace DoenaSoft.CreateShortcuts.Interfaces.IOServices;

public interface IShortcutCreator
{
    string Create(string seriesFolderForShortcutFiles
        , string seasonFolder);
}