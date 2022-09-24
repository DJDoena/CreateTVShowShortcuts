using System;
using System.Collections.Generic;
using DoenaSoft.AbstractionLayer.IOServices;
using DoenaSoft.CreateShortcuts.Interfaces.IOServices;
using DoenaSoft.CreateShortcuts.Interfaces.Processors;

namespace DoenaSoft.CreateShortcuts.Interfaces.ObjectStorage
{
    public interface IObjectStorage : IArticleProcessorStorage, ITupleStorage, IDisposable
    {
        IProgram Program { get; }

        IEnumerable<String> Arguments { get; }

        IWarningsProcessor WarningsProcessor { get; }

        IArgumentsProcessor ArgumentsProcessor { get; }

        IProcessor ShortcutFolderProcessor { get; }

        IProcessor VideoFolderProcessor { get; }

        IHelper Helper { get; }

        IShortcutCreator ShortcutCreator { get; }

        IIOServices IOServices { get; }

        ILogger Logger { get; }
    }
}