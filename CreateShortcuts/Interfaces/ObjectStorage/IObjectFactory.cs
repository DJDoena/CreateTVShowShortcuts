using System;
using System.Collections.Generic;
using DoenaSoft.AbstractionLayer.IOServices;
using DoenaSoft.CreateShortcuts.Interfaces.IOServices;
using DoenaSoft.CreateShortcuts.Interfaces.Processors;

namespace DoenaSoft.CreateShortcuts.Interfaces.ObjectStorage
{
    public interface IObjectFactory
    {
        IWarningsProcessor CreateWarningsProcessor(IObjectStorage os);

        IArgumentsProcessor CreateArgumentsProessor(IObjectStorage os);

        IProcessor CreateShortcutFolderProcessor(IObjectStorage os);

        IProcessor CreateVideoFolderProcessor(IObjectStorage os);

        IArticleProcessor CreateArticleProcessor(String seriesName
            , Boolean articleIsPrefix
            , IObjectStorage os);

        IHelper CreateHelper();

        IObjectStorage CreateObjectStorage(IProgram program
            , IEnumerable<String> arguments);

        ITuple CreateTuple(String article
            , Boolean articleIsPrefix);

        IShortcutCreator CreateShortcutCreator(IObjectStorage os);

        IIOServices CreateIOServices(IObjectStorage os);

        IShortcut CreateShortcut(String linkFileName
            , IObjectStorage os);

        ILogger CreateLogger(IObjectStorage os);
    }
}