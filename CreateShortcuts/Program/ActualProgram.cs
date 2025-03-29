using System.Collections.Generic;
using DoenaSoft.CreateShortcuts.Implementation.ObjectStorage;
using DoenaSoft.CreateShortcuts.Interfaces.Processors;

namespace DoenaSoft.CreateShortcuts;

public sealed class ActualProgram : IProgram
{
    private readonly IEnumerable<string> _arguments;

    public ActualProgram(IEnumerable<string> arguments)
    {
        _arguments = arguments;
    }

    public string RootFolderForShortcutFiles
        => Defaults.RootFolderForShortcutFiles;

    public string SeasonFolderPattern
        => Defaults.SeasonFolderPattern;

    public string StaffelFolderPattern
        => Defaults.StaffelFolderPattern;

    public string SeriesNamePattern
        => Defaults.SeriesNamePattern;

    public string ShortcutExtension
        => Defaults.ShortcutExtension;

    public IEnumerable<string> VideoFileFolders
        => Defaults.VideoFileFolders;

    public void Process()
    {
        var of = new ObjectFactory();

        using var os = of.CreateObjectStorage(this, _arguments);

        var ap = os.ArgumentsProcessor;

        ap.Process();

        if (ap.ShowHelp)
        {
            ap.PrintArguments();

            return;
        }

        var wp = os.WarningsProcessor;

        wp.Process();

        if (os.IOServices.Folder.Exists(this.RootFolderForShortcutFiles))
        {
            os.IOServices.Folder.Delete(this.RootFolderForShortcutFiles);
        }

        os.IOServices.Folder.CreateFolder(this.RootFolderForShortcutFiles);

        os.ShortcutFolderProcessor.Process();

        os.VideoFolderProcessor.Process();

        wp.Process();
    }

    public void Dispose()
    {
    }
}