using System;
using System.Collections.Generic;

namespace DoenaSoft.CreateShortcuts.Interfaces.Processors
{
    public interface IProgram : IProcessor, IDisposable
    {
        String RootFolderForShortcutFiles { get; }

        String SeasonFolderPattern { get; }

        String StaffelFolderPattern { get; }

        String SeriesNamePattern { get; }

        String ShortcutExtension { get; }

        IEnumerable<String> VideoFileFolders { get; }
    }
}