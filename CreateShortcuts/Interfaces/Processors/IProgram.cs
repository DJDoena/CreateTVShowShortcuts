using System;
using System.Collections.Generic;

namespace DoenaSoft.CreateShortcuts.Interfaces.Processors;

public interface IProgram : IProcessor, IDisposable
{
    string RootFolderForShortcutFiles { get; }

    string SeasonFolderPattern { get; }

    string StaffelFolderPattern { get; }

    string SeriesNamePattern { get; }

    string ShortcutExtension { get; }

    IEnumerable<string> VideoFileFolders { get; }
}